"""
web_dashboard.py

A basic Flask web dashboard for Blackstone Codex.
This dashboard provides a live view of the campaign timeline and session logs,
supporting our Cloud Sync & Web Dashboard goals for Version 2.2.
"""

from flask import Flask, jsonify, render_template, request
import json
import os

app = Flask(__name__)

# File paths (ensure these exist or are created by the DM app)
SESSION_LOG_FILE = "session_log.json"
CAMPAIGN_TIMELINE_FILE = "campaign_timeline.json"  # Future: Auto-built timeline data

def load_json(filepath):
    """Utility to load JSON data from a file; returns empty dict if not found."""
    if os.path.exists(filepath):
        try:
            with open(filepath, "r", encoding="utf-8") as f:
                return json.load(f)
        except Exception as e:
            print(f"Error loading {filepath}: {e}")
    return {}

@app.route("/")
def dashboard():
    """
    Render the main dashboard page.
    This page displays the campaign timeline and session log.
    """
    session_log = load_json(SESSION_LOG_FILE)
    timeline = load_json(CAMPAIGN_TIMELINE_FILE)
    return render_template("dashboard.html", session_log=session_log, timeline=timeline)

@app.route("/api/session-log")
def api_session_log():
    """
    API endpoint to return the session log in JSON format.
    """
    session_log = load_json(SESSION_LOG_FILE)
    return jsonify(session_log)

@app.route("/api/campaign-timeline")
def api_campaign_timeline():
    """
    API endpoint to return the campaign timeline.
    """
    timeline = load_json(CAMPAIGN_TIMELINE_FILE)
    return jsonify(timeline)

@app.route("/api/update-log", methods=["POST"])
def api_update_log():
    """
    API endpoint to update the session log.
    This could be called by the DM app to push new log entries.
    """
    new_entry = request.json
    if new_entry:
        log = load_json(SESSION_LOG_FILE)
        if not isinstance(log, list):
            log = []
        log.append(new_entry)
        try:
            with open(SESSION_LOG_FILE, "w", encoding="utf-8") as f:
                json.dump(log, f, indent=4)
            return jsonify({"status": "success"}), 200
        except Exception as e:
            return jsonify({"status": "error", "message": str(e)}), 500
    return jsonify({"status": "error", "message": "No data provided"}), 400

if __name__ == "__main__":
    # Run the Flask app on port 5000 by default.
    app.run(host="0.0.0.0", port=5000, debug=True)
#     sys.exit(app.exec())
# Note: This web dashboard is a basic implementation and can be extended with
# more features like user authentication, real-time updates using WebSockets,
# and more sophisticated error handling.