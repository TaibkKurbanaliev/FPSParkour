using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Config/Settings")]
public class Settings : ScriptableObject
{
    [field: SerializeField] public MouseSettings MouseSettings { get; private set; }
}
