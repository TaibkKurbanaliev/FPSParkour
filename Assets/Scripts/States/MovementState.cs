using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Video;

public abstract class MovementState : IState
{
    protected Player Player;
    protected IStateSwitcher StateSwitcher;

    public MovementState(Player player, IStateSwitcher stateSwitcher)
    {
        Player = player;
        StateSwitcher = stateSwitcher;
    }

    #region State Functions
    public virtual void Enter()
    {
        //Debug.Log($"Enter the {GetType().Name}");
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
        Move();
    }
    
    public virtual void Update()
    {
    }

    #endregion
    protected virtual void Move()
    {
        var moveInput = Player.Input.Player.Move.ReadValue<Vector2>();
        var direction = Player.transform.forward * moveInput.y + Player.transform.right * moveInput.x;
        var horizontalSpeed = Player.Rigidbody.velocity;
        horizontalSpeed.y = 0f;

        horizontalSpeed += Player.StatesData.Acceleration * Time.fixedDeltaTime * direction;
        var currentDrug = Player.StatesData.Drag * Time.fixedDeltaTime * horizontalSpeed.normalized;
        horizontalSpeed = horizontalSpeed.magnitude > Player.StatesData.Drag * Time.fixedDeltaTime 
                          ? horizontalSpeed - currentDrug : Vector3.zero;
        horizontalSpeed = Vector3.ClampMagnitude(horizontalSpeed, Player.StatesData.MaxHorizontalSpeed);

        Player.StatesData.TargetVelocity.x = horizontalSpeed.x;
        Player.StatesData.TargetVelocity.z = horizontalSpeed.z;
        Player.StatesData.TargetVelocity.y = Player.Rigidbody.velocity.y;

        Player.Rigidbody.velocity = Player.StatesData.TargetVelocity;
    }
}
