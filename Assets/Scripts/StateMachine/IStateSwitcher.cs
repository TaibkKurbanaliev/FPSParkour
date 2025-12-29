public interface IStateSwitcher
{
    void SwitchState<T>() where T : IState;
    void SetPreviousState();
}
