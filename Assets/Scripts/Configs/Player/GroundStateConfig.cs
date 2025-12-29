using System;
using UnityEngine;

[Serializable]
public class GroundStateConfig 
{
    [field: SerializeField] public float WalkSpeed { get; private set; }
    [field: SerializeField] public float WalkAcceleration { get; private set; }
    [field: SerializeField] public float RunSpeed { get; private set; }
    [field: SerializeField] public float RunAcceleration { get; private set; }
    [field: SerializeField] public float CrouchSpeed { get; private set; }
    [field: SerializeField] public float CrouchAcceleration { get; private set; }
    [field: SerializeField] public float CrouchHeight { get; private set; }
    [field: SerializeField] public float Drag { get; private set; }
    [field: SerializeField] public float MaxSlopeAngle { get; private set; }
    [field: SerializeField] public int NumberOfJumps { get; private set; }
}
