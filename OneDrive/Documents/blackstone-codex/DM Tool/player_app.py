"""
player_app.py

Implements the Player App UI using PyQt6.
Connects to the DM app via the NetworkClient and displays live session updates,
including Gandor chat feed, Initiative order, Active Table details, and Session Log.
Also includes a "Send to Gandor" field for two-way communication.
"""

import sys
import json
import logging
from PyQt6.QtWidgets import (QApplication, QMainWindow, QWidget, QVBoxLayout,
                             QHBoxLayout, QSplitter, QLabel, QTextEdit, QListWidget,
                             QLineEdit, QPushButton)
from PyQt6.QtCore import Qt
from network_client import NetworkClient

logging.basicConfig(level=logging.INFO, format='[%(asctime)s] %(levelname)s: %(message)s')

class PlayerApp(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Blackstone Codex – Player App")
        self.resize(900, 600)
        
        self.token = self.load_token()
        self.net_client = NetworkClient(token=self.token)
        self.net_client.set_callback(self.handle_network_message)
        self.net_client.start()
        
        # Centralized event handler registry.
        self.event_handlers = {
            "gandor_chat": self.handle_gandor_chat,
            "initiative_update": self.handle_initiative_update,
            "table_selected": self.handle_table_selected,
            "session_log_entry": self.handle_session_log_entry,
            "player_chat": self.handle_player_chat
        }
        
        self.setup_ui()
    
    def load_token(self):
        try:
            with open("session_config.json", "r", encoding="utf-8") as f:
                config = json.load(f)
                return config.get("player_token", "default_token")
        except Exception as e:
            logging.error(f"Error loading session_config.json: {e}")
            return "default_token"
    
    def setup_ui(self):
        central_widget = QWidget()
        self.setCentralWidget(central_widget)
        layout = QHBoxLayout()
        central_widget.setLayout(layout)
        
        splitter = QSplitter(Qt.Orientation.Horizontal)
        layout.addWidget(splitter)
        
        # Left Panel: Gandor Chat Feed, Session Log, and Player Chat Input.
        left_panel = QWidget()
        left_layout = QVBoxLayout()
        left_panel.setLayout(left_layout)
        
        self.gandor_chat_label = QLabel("Gandor Chat Feed")
        left_layout.addWidget(self.gandor_chat_label)
        self.gandor_chat_feed = QTextEdit()
        self.gandor_chat_feed.setReadOnly(True)
        left_layout.addWidget(self.gandor_chat_feed)
        
        self.session_log_label = QLabel("Session Log")
        left_layout.addWidget(self.session_log_label)
        self.session_log_view = QTextEdit()
        self.session_log_view.setReadOnly(True)
        left_layout.addWidget(self.session_log_view)
        
        # Player-to-Gandor chat input
        self.player_chat_input = QLineEdit()
        self.player_chat_input.setPlaceholderText("Type message to Gandor...")
        left_layout.addWidget(self.player_chat_input)
        self.player_send_button = QPushButton("Send to Gandor")
        self.player_send_button.clicked.connect(self.send_message_to_gandor)
        left_layout.addWidget(self.player_send_button)
        
        splitter.addWidget(left_panel)
        
        # Right Panel: Initiative Order & Active Table.
        right_panel = QWidget()
        right_layout = QVBoxLayout()
        right_panel.setLayout(right_layout)
        
        self.initiative_label = QLabel("Initiative Order")
        right_layout.addWidget(self.initiative_label)
        self.initiative_list = QListWidget()
        right_layout.addWidget(self.initiative_list)
        
        self.active_table_label = QLabel("Active Table")
        right_layout.addWidget(self.active_table_label)
        self.active_table_info = QTextEdit()
        self.active_table_info.setReadOnly(True)
        right_layout.addWidget(self.active_table_info)
        
        splitter.addWidget(right_panel)
        splitter.setStretchFactor(0, 1)
        splitter.setStretchFactor(1, 1)
    
    def send_message_to_gandor(self):
        msg = self.player_chat_input.text().strip()
        if not msg:
            return
        event_data = {
            "text": msg,
            "player_id": "Player1"  # Replace with actual player ID as needed.
        }
        payload = {
            "event": "player_chat",
            "data": event_data
        }
        self.net_client.send_event(payload)
        self.player_chat_input.clear()
    
    def handle_network_message(self, message):
        try:
            data = json.loads(message)
            event = data.get("event", "")
            payload = data.get("data", {})
            if event in self.event_handlers:
                self.event_handlers[event](payload)
            else:
                logging.warning(f"Unhandled event: {event}")
        except Exception as e:
            logging.error(f"Error handling network message: {e}")
    
    def handle_gandor_chat(self, payload):
        text = payload.get("text", "")
        self.gandor_chat_feed.append(text)
    
    def handle_initiative_update(self, payload):
        self.initiative_list.clear()
        for player in payload.get("players", []):
            self.initiative_list.addItem(player)
    
    def handle_table_selected(self, payload):
        title = payload.get("title", "No active table")
        description = payload.get("description", "")
        latest_roll = payload.get("latest_roll", "")
        info = f"Title: {title}\nDescription: {description}\nLatest Roll: {latest_roll}"
        self.active_table_info.setPlainText(info)
    
    def handle_session_log_entry(self, payload):
        entry = payload.get("entry", "")
        self.session_log_view.append(entry)
    
    def handle_player_chat(self, payload):
        # Optionally display player chat messages in the chat feed.
        text = payload.get("text", "")
        self.gandor_chat_feed.append(f"<b>Player:</b> {text}")

if __name__ == '__main__':
    app = QApplication(sys.argv)
    window = PlayerApp()
    window.show()
    sys.exit(app.exec())
