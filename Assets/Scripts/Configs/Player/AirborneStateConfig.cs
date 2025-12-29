using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AirborneStateConfig 
{
    [field: SerializeField, Range(0f,1f)] public float AirFriction { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }

}
