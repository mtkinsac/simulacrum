using UnityEngine;
using UnityEngine.UI;

public class SimulatedSummonUI : MonoBehaviour
{
    public CallboxController callboxController;
    private bool showUI = true;

    void OnGUI()
    {
        if (!showUI) return;
        GUILayout.BeginArea(new Rect(10, 10, 300, 400), "Summon UI", GUI.skin.window);

        GUILayout.Label("Destination Selector:");
        if (GUILayout.Button("Travel to WhisperingWoods"))
            callboxController.TravelToDestination("WhisperingWoods");

        GUILayout.Space(10);
        GUILayout.Label("Monitor Controls:");
        if (GUILayout.Button("Summon Desk Monitors"))
            callboxController.SummonMonitors("Desk");
        if (GUILayout.Button("Summon Fireplace Screen"))
            callboxController.SummonMonitors("Fireplace");
        if (GUILayout.Button("Summon Roundtable Monitors"))
            callboxController.SummonMonitors("Roundtable");
        if (GUILayout.Button("Dismiss All Monitors"))
            callboxController.DismissMonitors();

        GUILayout.Space(10);
        GUILayout.Label("Other Controls:");
        if (GUILayout.Button("Toggle UI"))
            showUI = !showUI;

        GUILayout.EndArea();
    }
}