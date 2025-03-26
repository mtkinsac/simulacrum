using UnityEngine;

public class IntegrationManager : MonoBehaviour
{
    void OnEnable()
    {
        SummonableContentEvents.OnSummonContentRequest += HandleSummonContentRequest;
        EventManager.OnKeyboardToggled += HandleKeyboardToggled;
        EventManager.OnSummonRequest += HandleGeneralSummon;
        EventManager.OnMonitorClosed += HandleMonitorClosed;
        EventManager.OnValeSpeakRequest += HandleValeSpeakRequest;
        EventManager.OnProfileLoaded += HandleProfileLoaded;
        Debug.Log("IntegrationManager: Global events wired.");
    }

    void OnDisable()
    {
        SummonableContentEvents.OnSummonContentRequest -= HandleSummonContentRequest;
        EventManager.OnKeyboardToggled -= HandleKeyboardToggled;
        EventManager.OnSummonRequest -= HandleGeneralSummon;
        EventManager.OnMonitorClosed -= HandleMonitorClosed;
        EventManager.OnValeSpeakRequest -= HandleValeSpeakRequest;
        EventManager.OnProfileLoaded -= HandleProfileLoaded;
        Debug.Log("IntegrationManager: Global event wiring removed.");
    }

    private void HandleSummonContentRequest()
    {
        Debug.Log("IntegrationManager: Handling SummonContentRequest.");
    }

    private void HandleKeyboardToggled(bool isActive)
    {
        Debug.Log("IntegrationManager: Keyboard toggled. Active: " + isActive);
    }

    private void HandleGeneralSummon()
    {
        Debug.Log("IntegrationManager: General summon request received.");
    }

    private void HandleMonitorClosed()
    {
        Debug.Log("IntegrationManager: Monitor closed event received.");
    }

    private void HandleValeSpeakRequest(string message)
    {
        Debug.Log("IntegrationManager: ValeSpeakRequest received with message: " + message);
    }

    private void HandleProfileLoaded()
    {
        Debug.Log("IntegrationManager: Profile loaded, updating system state.");
    }
}
