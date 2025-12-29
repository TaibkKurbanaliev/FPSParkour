using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public event Action Reached;

    private void OnTriggerEnter(Collider other)
    {
        Reached?.Invoke();
        gameObject.SetActive(false);
    }
}
