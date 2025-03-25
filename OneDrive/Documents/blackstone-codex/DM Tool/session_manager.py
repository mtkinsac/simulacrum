"""
session_manager.py

Manages the session log for the DM application.
Supports adding log entries, saving the log to disk, and loading/replaying logs.
"""

import json
import os
from datetime import datetime

class SessionManager:
    def __init__(self, log_file="session_log.json"):
        """
        Initialize the SessionManager.

        Args:
            log_file (str): The filename where the session log will be saved.
        """
        self.log_file = log_file
        self.session_log = []

    def add_entry(self, role: str, message: str):
        """
        Add a log entry with a timestamp and role tag.

        Args:
            role (str): Identifier for the source of the message (e.g., 'DM', 'Gandor', 'Player').
            message (str): The message to log.
        """
        timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        entry = {"timestamp": timestamp, "role": role, "message": message}
        self.session_log.append(entry)

    def get_log(self) -> str:
        """
        Return the entire session log as a formatted string.

        Returns:
            str: A multi-line string of log entries.
        """
        return "\n".join(
            f"[{entry['timestamp']}] ({entry['role']}) {entry['message']}" 
            for entry in self.session_log
        )

    def save_session(self):
        """
        Save the session log to the designated log file in JSON format.
        """
        try:
            with open(self.log_file, "w", encoding="utf-8") as f:
                json.dump(self.session_log, f, indent=4)
        except Exception as e:
            print(f"Error saving session log: {e}")

    def load_session(self):
        """
        Load the session log from the designated log file.
        """
        if os.path.exists(self.log_file):
            try:
                with open(self.log_file, "r", encoding="utf-8") as f:
                    self.session_log = json.load(f)
            except Exception as e:
                print(f"Error loading session log: {e}")

# For standalone testing:
if __name__ == '__main__':
    sm = SessionManager()
    sm.add_entry("DM", "Welcome to Blackstone Codex!")
    sm.add_entry("Gandor", "I am here to guide you.")
    print("Current Session Log:")
    print(sm.get_log())
    sm.save_session()
