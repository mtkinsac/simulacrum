"""
dm_app.py

Main entry point for the Dungeon Master (DM) application.
Integrates GandorPanel, NetworkServer, InitiativeTracker, and SessionManager.
"""

import sys
from PyQt6.QtWidgets import QApplication, QMainWindow, QWidget, QVBoxLayout, QHBoxLayout, QLabel, QTextEdit
from gandor_panel import GandorPanel
from network_server import NetworkServer
from initiative_tracker import InitiativeTracker
from session_manager import SessionManager

class DMApp(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Blackstone Codex – DM App")
        self.resize(1000, 700)
        
        # Initialize SessionManager for log persistence.
        self.session_manager = SessionManager(log_file="session_log.json")
        
        # Start network server.
        self.network_server = NetworkServer()
        self.network_server.set_player_chat_callback(self.on_player_chat)
        self.network_server.start_in_thread()
        
        self.setup_ui()
    
    def setup_ui(self):
        central_widget = QWidget()
        self.setCentralWidget(central_widget)
        main_layout = QVBoxLayout()
        central_widget.setLayout(main_layout)
        
        # Gandor Panel.
        self.gandor_panel = GandorPanel(session_manager=self.session_manager)
        main_layout.addWidget(self.gandor_panel)
        
        # Initiative Tracker.
        self.initiative_tracker = InitiativeTracker()
        dummy_players = [
            {"name": "Aria", "initiative": 15},
            {"name": "Borin", "initiative": 18},
            {"name": "Celeste", "initiative": 12},
            {"name": "Darius", "initiative": 20}
        ]
        self.initiative_tracker.load_data(dummy_players)
        main_layout.addWidget(self.initiative_tracker)
        
        # Session Log.
        log_layout = QHBoxLayout()
        log_label = QLabel("Session Log")
        log_layout.addWidget(log_label)
        self.session_log_view = QTextEdit()
        self.session_log_view.setReadOnly(True)
        self.session_log_view.setPlaceholderText("Session log entries will appear here...")
        log_layout.addWidget(self.session_log_view)
        main_layout.addLayout(log_layout)
        
        self.session_manager.load_session()
        self.update_session_log()

    def update_session_log(self):
        log_text = self.session_manager.get_log()
        self.session_log_view.setPlainText(log_text)

    def on_player_chat(self, payload: dict):
        player_id = payload.get("player_id", "Unknown Player")
        text = payload.get("text", "")
        self.session_manager.add_entry(player_id, text)
        self.update_session_log()
        # Auto-forward to Gandor:
        response = self.gandor_panel.generate_response(text)
        self.session_manager.add_entry("Gandor", response)
        self.update_session_log()

if __name__ == '__main__':
    app = QApplication(sys.argv)
    window = DMApp()
    window.show()
    sys.exit(app.exec())
