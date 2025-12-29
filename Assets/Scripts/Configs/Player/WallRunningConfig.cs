using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallRunningConfig
{
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int NumberOfJumps { get; private set; }
}
