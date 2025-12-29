using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("HeadBob")]
    [SerializeField, Range(0, 0.1f)] private float _amplitude;
    [SerializeField, Range(0, 30f)] private float _frequency;
    [SerializeField, Range(1, 10f)] private float _smoothness;

    private Vector2Int _y—onstraint = new Vector2Int(-88,88);

    private float _pitch;

    private PlayerInput _input;
    private Player _player;
    private MouseSettings _setting;
    private Vector3 _headBobValue;
    private Vector3 _initialPos;
    private float _currentFrequency;

    public Vector2Int Y—onstraint
    {
        get => _y—onstraint;
        set
        {
            if ((value.x > 89 || value.x < -89 || value.y > 89 || value.y < -89) && value.x <= value.y)
                throw new ArgumentOutOfRangeException(nameof(value));

            _y—onstraint = value;
        }
    }

    public void Init(PlayerInput input, Player player, MouseSettings setting)
    {
        _input = input;
        _player = player;
        _setting = setting;
        _initialPos = transform.localPosition;
    }

    private void LateUpdate()
    {
        var lookInput = _input.Player.Look.ReadValue<Vector2>();
        _player.transform.Rotate(_player.transform.up, lookInput.x * _setting.Sensetive * Time.unscaledDeltaTime);

        transform.localPosition = _initialPos + _player.Collider.center * 2f;

        _pitch -= lookInput.y * _setting.Sensetive * Time.unscaledDeltaTime;
        _pitch = Mathf.Clamp(_pitch, _y—onstraint.x, _y—onstraint.y);
        transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);

        ApplyHeadBob();
    }

    private Vector3 FootStep()
    {
        _currentFrequency = _frequency * _player.StatesData.MaxHorizontalSpeed;
        _headBobValue = Vector3.zero;
        _headBobValue.y = Mathf.Sin((_currentFrequency) * Time.time) * _amplitude;
        _headBobValue.x = Mathf.Cos((_currentFrequency / 2) * Time.time) * _amplitude * 2;
        return _headBobValue;
    }

    private void ApplyHeadBob()
    {
        if (_player.StatesData.IsSliding || (!_player.StatesData.IsWalled && !_player.StatesData.IsGronded))
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, 
                                          transform.position + FootStep(), 
                                          _player.Rigidbody.velocity.magnitude * _smoothness);
    }

    public void FastRotate(Quaternion rotation)
    {

    }
}
