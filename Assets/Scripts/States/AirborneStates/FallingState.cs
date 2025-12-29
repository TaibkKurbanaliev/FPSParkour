public class FallingState : AirborneState
{
    public FallingState(Player player, IStateSwitcher stateSwitcher, AirborneStateConfig config) 
        : base(player, stateSwitcher, config)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }
}
