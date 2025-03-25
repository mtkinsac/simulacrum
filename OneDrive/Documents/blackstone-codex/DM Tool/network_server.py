"""
network_server.py

Implements a lightweight WebSocket server using the websockets library.
It listens for incoming player connections, authenticates them via token (from session_config.json),
and broadcasts session events to all connected clients. It also allows a DM app to set
a callback for handling 'player_chat' events directly.
"""

import asyncio
import json
import logging
import threading
import os
import websockets  # pip install websockets

DEFAULT_PORT = 8765
TOKENS_FILE = "session_config.json"

class NetworkServer:
    def __init__(self, port=DEFAULT_PORT):
        self.port = port
        self.clients = set()
        self.loop = None
        self.server = None
        self.running = False
        self.token_set = self.load_tokens()
        self.player_chat_callback = None  # Callback for "player_chat" events
        logging.info(f"NetworkServer initialized on port {self.port}")

    def load_tokens(self):
        if os.path.exists(TOKENS_FILE):
            try:
                with open(TOKENS_FILE, "r", encoding="utf-8") as f:
                    config = json.load(f)
                    return set(config.get("accepted_tokens", []))
            except Exception as e:
                logging.error(f"Error loading tokens: {e}")
        return set()

    async def handler(self, websocket, path):
        """
        Handler for each new WebSocket connection.
        Expects a JSON handshake containing a token.
        """
        try:
            handshake = await websocket.recv()
            data = json.loads(handshake)
            token = data.get("token", "")
            if token not in self.token_set:
                await websocket.send(json.dumps({"error": "Invalid token"}))
                await websocket.close()
                logging.info("Rejected connection due to invalid token.")
                return
            
            self.clients.add(websocket)
            logging.info(f"Client connected: {websocket.remote_address}")

            # Listen for messages from this client.
            async for message in websocket:
                logging.info(f"Received from client: {message}")
                try:
                    msg_data = json.loads(message)
                    event_type = msg_data.get("event", "")
                    payload = msg_data.get("data", {})

                    if event_type == "player_chat":
                        # Invoke callback if set (for DM processing)
                        if self.player_chat_callback:
                            self.player_chat_callback(payload)
                        
                        # Re-broadcast the player's chat message to all clients.
                        broadcast_payload = {
                            "text": f"Player says: {payload.get('text', '')}",
                            "player_id": payload.get("player_id", "Unknown")
                        }
                        await self._broadcast(json.dumps({
                            "event": "player_chat",
                            "data": broadcast_payload
                        }))
                    # Additional event processing can be added here.
                except Exception as e:
                    logging.error(f"Error processing client message: {e}")
        except Exception as e:
            logging.error(f"Error in client handler: {e}")
        finally:
            if websocket in self.clients:
                self.clients.remove(websocket)
                logging.info("Client disconnected.")

    async def start(self):
        # Use a lambda with *args to capture both websocket and path.
        self.server = await websockets.serve(lambda *args: self.handler(*args), "0.0.0.0", self.port)
        self.running = True
        logging.info("Network server started.")
        await self.server.wait_closed()

    def run(self):
        self.loop = asyncio.new_event_loop()
        asyncio.set_event_loop(self.loop)
        try:
            self.loop.run_until_complete(self.start())
        except Exception as e:
            logging.error(f"Network server error: {e}")
        finally:
            self.loop.close()

    def start_in_thread(self):
        thread = threading.Thread(target=self.run, daemon=True)
        thread.start()
        logging.info("Network server thread started.")

    def broadcast(self, event, data):
        message = json.dumps({"event": event, "data": data})
        if self.loop and self.running:
            asyncio.run_coroutine_threadsafe(self._broadcast(message), self.loop)

    async def _broadcast(self, message):
        if self.clients:
            await asyncio.wait([client.send(message) for client in self.clients])
        logging.info(f"Broadcasted message: {message}")

    def set_player_chat_callback(self, callback):
        """Assign a callback function to handle 'player_chat' events."""
        self.player_chat_callback = callback
        logging.info("Player chat callback set.")

if __name__ == '__main__':
    logging.basicConfig(level=logging.INFO, format='[%(asctime)s] %(levelname)s: %(message)s')
    server = NetworkServer(port=8765)
    server.set_player_chat_callback(lambda payload: print(f"Received player chat: {payload}"))
    server.start_in_thread()
    try:
        while True:
            asyncio.sleep(1)
    except KeyboardInterrupt:
        logging.info("Server shutting down.")
