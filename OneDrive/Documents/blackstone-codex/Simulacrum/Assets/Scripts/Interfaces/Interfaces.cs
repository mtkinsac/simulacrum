public interface ISummonable
{
    void Summon();
    void Dismiss();
}

public interface IMonitorInteractable
{
    void ResizeMonitor(float scaleFactor);
    void CloseMonitor();
}

public interface IAnimatedUI
{
    void PlayAnimation(string animationName);
    void StopAnimation(string animationName);
}

public interface IStatefulObject
{
    string GetState();
    void SetState(string state);
}
