using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class DashState : MovementState
{
    private DashStateConfig _config;

    public DashState(Player player, IStateSwitcher stateSwitcher, DashStateConfig config) : base(player, stateSwitcher)
    {
        _config = config;
    }

    public override void Enter()
    {
        base.Enter();

        Player.StatesData.SetDashReload(true, _config.ReloadTime + _config.Duration);
        Dash().Forget();
    }
    
    protected async UniTask Dash()
    {
        Vector3 direction = new Vector3(Player.transform.forward.x, 0f, Player.transform.forward.z);
 
        Player.StatesData.MaxHorizontalSpeed = _config.Acceleration;
        Player.StatesData.Acceleration = _config.Acceleration;
        Player.StatesData.TargetVelocity = direction.normalized * Player.StatesData.Acceleration;
        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;

        await UniTask.WaitForSeconds(_config.Duration);

        StateSwitcher.SetPreviousState();
        Reload().Forget();
    }

    private async UniTask Reload()
    {
        await UniTask.WaitForSeconds(_config.ReloadTime);

        Player.StatesData.SetDashReload(false, _config.ReloadTime);
    }
}
