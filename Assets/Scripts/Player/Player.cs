using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;

    [field: SerializeField] public PlayerConfig Config;
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Camera Camera { get; private set; }
    [field: SerializeField] public EnviromentChecker Checker { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public PlayerInput Input { get; private set; }
    public StatesData StatesData { get; private set; }

    public void Init(PlayerInput input)
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        Input = input;

        StatesData = new StatesData();
        _stateMachine = new StateMachine();

        _stateMachine.AddState(new IdleState(this, _stateMachine, Config.GroundStateConfig));
        _stateMachine.AddState(new WalkingState(this, _stateMachine, Config.GroundStateConfig));
        _stateMachine.AddState(new RunningState(this, _stateMachine, Config.GroundStateConfig));
        _stateMachine.AddState(new CrouchingState(this, _stateMachine, Config.GroundStateConfig));
        _stateMachine.AddState(new SlidingState(this, _stateMachine, Config.GroundStateConfig, Config.SlidingStateConfig));

        _stateMachine.AddState(new DashState(this, _stateMachine, Config.DashStateConfig));
        _stateMachine.AddState(new AirDashState(this, _stateMachine, Config.DashStateConfig, Config.AirborneStateConfig));

        _stateMachine.AddState(new WallRunningState(this, _stateMachine, Config.WallRunningConfig));
        _stateMachine.AddState(new SlideDownState(this, _stateMachine));

        _stateMachine.AddState(new JumpingState(this, _stateMachine, Config.AirborneStateConfig));
        _stateMachine.AddState(new FallingState(this, _stateMachine, Config.AirborneStateConfig));

        _stateMachine.SwitchState<IdleState>();
    }

    private void Update()
    {
        _stateMachine.Update();

        if (transform.position.y < Config.MinYPosToDeath)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate()
    {
        Checker.GroundCheck();
        _stateMachine.FixedUpdate();
    }
}
