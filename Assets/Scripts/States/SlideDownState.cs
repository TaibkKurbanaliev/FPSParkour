using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDownState : MovementState
{
    public SlideDownState(Player player, IStateSwitcher stateSwitcher) : base(player, stateSwitcher)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.Rigidbody.velocity = -Player.transform.forward;
    }

    public override void Update()
    {
        base.Update();

        if (!Player.StatesData.IsGronded)
        {
            StateSwitcher.SwitchState<FallingState>();
        }
        else if (Player.StatesData.SlopeAngle == 0f)
        {
            StateSwitcher.SwitchState<IdleState>();
        }
    }

    protected override void Move()
    {
    }
}
