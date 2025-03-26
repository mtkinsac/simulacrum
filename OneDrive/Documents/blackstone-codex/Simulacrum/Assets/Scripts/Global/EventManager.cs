using System;
using UnityEngine;

public static class EventManager
{
    public static event Action OnSummonRequest;
    public static event Action OnMonitorClosed;
    public static event Action<string> OnValeSpeakRequest;
    public static event Action<bool> OnKeyboardToggled;
    public static event Action OnProfileLoaded;

    public static void SummonRequest()
    {
        OnSummonRequest?.Invoke();
        Debug.Log("EventManager: SummonRequest event triggered.");
    }

    public static void MonitorClosed()
    {
        OnMonitorClosed?.Invoke();
        Debug.Log("EventManager: MonitorClosed event triggered.");
    }

    public static void ValeSpeakRequest(string message)
    {
        OnValeSpeakRequest?.Invoke(message);
        Debug.Log("EventManager: ValeSpeakRequest event triggered with message: " + message);
    }

    public static void KeyboardToggled(bool isActive)
    {
        OnKeyboardToggled?.Invoke(isActive);
        Debug.Log("EventManager: KeyboardToggled event triggered with state: " + isActive);
    }

    public static void ProfileLoaded()
    {
        OnProfileLoaded?.Invoke();
        Debug.Log("EventManager: ProfileLoaded event triggered.");
    }
}
