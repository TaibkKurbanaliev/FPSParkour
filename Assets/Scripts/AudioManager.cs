using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private static AudioSource _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = Instantiate(audioSource);
            DontDestroyOnLoad(_instance);
        }
    }
}
