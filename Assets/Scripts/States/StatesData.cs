using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesData
{
    public event Action< float> DashReloadingStarted;

    public float SlopeAngle;
    public IState PreviousState;

    public Vector3 TargetVelocity;
    public Vector3 WallNormal;

    public bool IsSliding;
    public bool IsGronded;
    public bool IsWalled;

    private float _acceleration;
    private float _drag;
    private float _maxHorizontalVelocity;
    private int _numberOfAvailableJumps;
    private bool _isDashReload;

    public bool IsDashReload => _isDashReload;


    public void SetDashReload(bool isDashReload, float reloadTime)
    {
        _isDashReload = isDashReload;

        if (isDashReload)
            DashReloadingStarted?.Invoke(reloadTime);
    }


    public int NumberOfAvailableJumps
    {
        get => _numberOfAvailableJumps;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(NumberOfAvailableJumps)} less then zero");

            _numberOfAvailableJumps = value;
        }
    }

    public float Acceleration
    {
        get => _acceleration;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _acceleration = value;
        }
    }
    public float Drag
    {
        get => _drag;
        set
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            _drag = value;
        }
    }

    public float MaxHorizontalSpeed
    {
        get => _maxHorizontalVelocity;
        set
        {
            if (value < 0) 
                throw new ArgumentException(nameof(value));

            _maxHorizontalVelocity = value;
        }
    }
}
