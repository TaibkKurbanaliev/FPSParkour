using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MinYPosToDeath { get; private set; }
    [field: SerializeField] public LayerMask WallMask { get; private set; }
    [field: SerializeField] public GroundStateConfig GroundStateConfig {  get; private set; }
    [field: SerializeField] public AirborneStateConfig AirborneStateConfig { get; private set; }
    [field: SerializeField] public SlidingStateConfig SlidingStateConfig { get; private set; }
    [field: SerializeField] public DashStateConfig DashStateConfig { get; private set; }
    [field: SerializeField] public WallRunningConfig WallRunningConfig { get; private set; }
}
