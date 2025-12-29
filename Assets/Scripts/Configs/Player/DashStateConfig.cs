using System;
using UnityEngine;

[Serializable]
public class DashStateConfig
{
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }

}
