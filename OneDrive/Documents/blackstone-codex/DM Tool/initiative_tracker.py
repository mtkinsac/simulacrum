"""
initiative_tracker.py

This module implements an Initiative Tracker widget using PyQt6.
It displays a list of players with their initiative scores, supports drag-and-drop reordering,
and provides buttons to advance turns and reset rounds.
"""

from PyQt6.QtWidgets import QWidget, QVBoxLayout, QTableWidget, QTableWidgetItem, QPushButton, QHBoxLayout
from PyQt6.QtCore import Qt

class InitiativeTracker(QWidget):
    def __init__(self, parent=None):
        super().__init__(parent)
        self.setup_ui()
    
    def setup_ui(self):
        layout = QVBoxLayout()
        self.setLayout(layout)
        
        # Table to display initiative order
        self.table = QTableWidget(0, 2)
        self.table.setHorizontalHeaderLabels(["Player", "Initiative"])
        self.table.setDragDropMode(QTableWidget.DragDropMode.InternalMove)
        self.table.setSelectionBehavior(QTableWidget.SelectionBehavior.SelectRows)
        layout.addWidget(self.table)
        
        # Buttons for advancing turn and resetting round
        btn_layout = QHBoxLayout()
        self.advance_btn = QPushButton("Advance Turn")
        self.advance_btn.clicked.connect(self.advance_turn)
        btn_layout.addWidget(self.advance_btn)
        
        self.reset_btn = QPushButton("Reset Round")
        self.reset_btn.clicked.connect(self.reset_round)
        btn_layout.addWidget(self.reset_btn)
        
        layout.addLayout(btn_layout)
    
    def load_data(self, players: list):
        """
        Populate the table with players.
        players: List of dicts with keys 'name' and 'initiative'
        """
        self.table.setRowCount(0)
        for player in players:
            self.add_player(player["name"], player.get("initiative", 0))
    
    def add_player(self, name: str, initiative: int):
        row = self.table.rowCount()
        self.table.insertRow(row)
        name_item = QTableWidgetItem(name)
        initiative_item = QTableWidgetItem(str(initiative))
        initiative_item.setTextAlignment(Qt.AlignmentFlag.AlignCenter)
        self.table.setItem(row, 0, name_item)
        self.table.setItem(row, 1, initiative_item)
    
    def advance_turn(self):
        """
        Move the top row to the bottom, simulating turn advancement.
        """
        if self.table.rowCount() == 0:
            return
        row_data = [self.table.item(0, col).text() for col in range(self.table.columnCount())]
        self.table.removeRow(0)
        row = self.table.rowCount()
        self.table.insertRow(row)
        for col, text in enumerate(row_data):
            item = QTableWidgetItem(text)
            if col == 1:
                item.setTextAlignment(Qt.AlignmentFlag.AlignCenter)
            self.table.setItem(row, col, item)
    
    def reset_round(self):
        """
        Re-sort the table based on initiative values (descending).
        """
        rows = []
        for row in range(self.table.rowCount()):
            name = self.table.item(row, 0).text()
            try:
                initiative = int(self.table.item(row, 1).text())
            except ValueError:
                initiative = 0
            rows.append((name, initiative))
        # Sort descending by initiative.
        rows.sort(key=lambda x: x[1], reverse=True)
        self.table.setRowCount(0)
        for name, initiative in rows:
            self.add_player(name, initiative)
