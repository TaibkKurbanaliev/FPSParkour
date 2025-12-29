using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Settings _settings;
    [SerializeField] private CameraController _camera;

    private PlayerInput _inputActions;

    private void Awake()
    {
        _inputActions = new();
        _inputActions.Enable();
        _player.Init(_inputActions);
        _camera.Init(_inputActions, _player, _settings.MouseSettings);
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
