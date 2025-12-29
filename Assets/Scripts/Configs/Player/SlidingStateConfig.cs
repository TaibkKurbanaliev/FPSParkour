using System;
using UnityEngine;

[Serializable]
public class SlidingStateConfig
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float Deceleration { get; private set; }
    [field: SerializeField] public float ColliderSize { get; private set; }
}
