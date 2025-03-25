"""
network_client.py

This module implements a WebSocket client that connects to the DM app's WebSocket server.
It performs a secure token-based handshake and receives live session events.
It also provides a helper method to send arbitrary event payloads.
"""

import asyncio
import json
import logging
import threading
import websockets  # pip install websockets

class NetworkClient:
    def __init__(self, uri="ws://localhost:8765", token=""):
        self.uri = uri
        self.token = token
        self.loop = None
        self.connected = False
        self.callback = None  # Callback to handle received messages.
        self.websocket = None
        logging.info(f"NetworkClient initialized with URI {self.uri} and token {self.token}")

    def set_callback(self, callback):
        """Set the callback function to handle incoming messages."""
        self.callback = callback

    async def send_event_async(self, payload: dict):
        """Asynchronously send an event payload to the server."""
        if self.websocket and self.connected:
            await self.websocket.send(json.dumps(payload))

    def send_event(self, payload: dict):
        """Thread-safe wrapper to send an event."""
        if not self.loop or not self.connected:
            return
        asyncio.run_coroutine_threadsafe(self.send_event_async(payload), self.loop)

    async def connect(self):
        try:
            async with websockets.connect(self.uri) as websocket:
                self.websocket = websocket
                handshake = json.dumps({"token": self.token})
                await websocket.send(handshake)
                self.connected = True
                logging.info("Connected to DM server via WebSocket.")
                async for message in websocket:
                    logging.info(f"Received message: {message}")
                    if self.callback:
                        self.callback(message)
        except Exception as e:
            logging.error(f"NetworkClient error: {e}")
            self.connected = False

    def run(self):
        self.loop = asyncio.new_event_loop()
        asyncio.set_event_loop(self.loop)
        self.loop.run_until_complete(self.connect())

    def start(self):
        thread = threading.Thread(target=self.run, daemon=True)
        thread.start()
