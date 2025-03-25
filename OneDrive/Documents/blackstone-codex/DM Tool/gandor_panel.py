"""
gandor_panel.py

Uses a centralized flavor generator, persists conversation memory,
and logs entries to the session log with a "Gandor" or "DM" role tag.
"""

import json
import os
from PyQt6.QtWidgets import QWidget, QVBoxLayout, QTextEdit, QLineEdit, QPushButton
from gandor_flavor import get_flavor

class GandorPanel(QWidget):
    def __init__(self, session_manager=None, personality="Wise", parent=None):
        super().__init__(parent)
        self.session_manager = session_manager
        self.personality = personality
        self.session_context = []
        self.memory_file = "gandor_memory.json"
        self.setup_ui()
    
    def setup_ui(self):
        layout = QVBoxLayout()
        self.setLayout(layout)
        
        self.chat_log = QTextEdit()
        self.chat_log.setReadOnly(True)
        self.chat_log.setPlaceholderText("Gandor's responses will appear here...")
        layout.addWidget(self.chat_log)
        
        self.input_field = QLineEdit()
        self.input_field.setPlaceholderText("Enter your message to Gandor...")
        layout.addWidget(self.input_field)
        
        self.send_button = QPushButton("Send")
        self.send_button.clicked.connect(self.send_message)
        layout.addWidget(self.send_button)
    
    def send_message(self):
        message = self.input_field.text().strip()
        if not message:
            return
        
        # Log the DM's message with role "DM"
        if self.session_manager:
            self.session_manager.add_entry("DM", message)
        
        self.chat_log.append(f"<b>DM:</b> {message}")
        self.session_context.append(message)
        self.input_field.clear()
        
        response = self.generate_response(message)
        
        # Log Gandor's reply with role "Gandor"
        if self.session_manager:
            self.session_manager.add_entry("Gandor", response)
        
        self.chat_log.append(f"<b>Gandor:</b> {response}")
        
        self.save_conversation_memory(message, response)
    
    def generate_response(self, message: str) -> str:
        lower_msg = message.lower()
        if "roll initiative" in lower_msg:
            base_response = "I see you wish to roll initiative. Let fate decide!"
        elif "help" in lower_msg:
            base_response = "I am here to guide you through shadows and light."
        else:
            base_response = f"I heard you say: '{message}'"
        
        if self.session_context.count(message) > 1:
            base_response += " You've asked that before..."
        
        flavor = get_flavor(self.personality)
        return f"{base_response} {flavor}"
    
    def save_conversation_memory(self, dm_message: str, gandor_response: str):
        # same as before, no changes
        ...
