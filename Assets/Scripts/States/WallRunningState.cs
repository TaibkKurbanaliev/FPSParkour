using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

enum WallSide { Left, Right }

public class WallRunningState : MovementState
{

    private CancellationTokenSource _cts;
    private WallRunningConfig _config;
    private WallSide _side;
    private Vector3 _wallNormal;
    private Vector3 _velocity;

    public WallRunningState(Player player, IStateSwitcher stateSwitcher, WallRunningConfig config) 
        : base(player, stateSwitcher)
    {
        _config = config;
        Player.StatesData.NumberOfAvailableJumps = _config.NumberOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        Player.Input.Player.Jump.started += OnJumpStarted;
        _wallNormal = Player.StatesData.WallNormal;
        InitWallRun();
    }

    private void InitWallRun()
    {
        _side = _wallNormal.magnitude > 0 ? WallSide.Right : WallSide.Left;

        Vector3 wallTangent = Vector3.Cross(Vector3.up, _wallNormal).normalized;
        var horizontalVelocity = Player.Rigidbody.velocity;
        horizontalVelocity.y = 0f;

        float dir = Mathf.Sign(Vector3.Dot(wallTangent, Player.transform.forward));
        wallTangent *= dir;
        Debug.Log(wallTangent);

        Player.StatesData.TargetVelocity = wallTangent * _config.Speed;

        _cts = new CancellationTokenSource();
        WaitRun(_cts.Token).Forget();
    }

    public override void Exit()
    {
        base.Exit();
        Player.StatesData.NumberOfAvailableJumps = _config.NumberOfJumps;
        Player.Input.Player.Jump.started -= OnJumpStarted;
        Player.StatesData.IsWalled = false;
        _cts.Cancel();
    }

    public override void FixedUpdate()
    {
        if (Player.StatesData.IsWalled == false)
        {
            StateSwitcher.SwitchState<FallingState>();
            return;
        }

        base.FixedUpdate(); 
    }

    protected override void Move()
    {
        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;
    }

    private async UniTask WaitRun(CancellationToken token = default)
    {
        await UniTask.WaitForSeconds(_config.Duration, cancellationToken: token);

        Player.Rigidbody.AddForce(-_wallNormal * 2f, ForceMode.VelocityChange);
        Player.StatesData.IsWalled = false;
        StateSwitcher.SwitchState<FallingState>();
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        var angle = Player.transform.forward;
        angle.y = 0f;
        var wallCross = Vector3.Dot(_wallNormal, angle);

        if (wallCross > 0.5f)
            Player.StatesData.TargetVelocity = angle * _config.Speed;
        else
            Player.StatesData.TargetVelocity = _wallNormal * _config.Speed;

        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;
        StateSwitcher.SwitchState<JumpingState>();
    }
}
