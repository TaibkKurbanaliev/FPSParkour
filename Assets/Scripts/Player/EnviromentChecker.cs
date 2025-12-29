using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentChecker : MonoBehaviour
{
    [SerializeField] private float _distanceToCheckHead = 1.5f;
    [SerializeField] private float _castRadiusOffset = 0.05f;
    [SerializeField] private Player _player;

    public void GroundCheck()
    {
        Vector3 playerCenter = _player.Rigidbody.position + _player.Collider.center;

        var distance = (_player.Collider.height / 2) - _player.Collider.radius;
        var ray = new Ray(playerCenter, -_player.transform.up); // down direction
        
        if (Physics.SphereCast(ray, _player.Collider.radius - _castRadiusOffset, out var hitInfo, distance + _castRadiusOffset * 2f))
        {
            _player.StatesData.IsGronded = true;
            _player.StatesData.SlopeAngle = Vector3.Angle(_player.transform.up, hitInfo.normal);

            return;
        }

        _player.StatesData.IsGronded = false;
        _player.StatesData.SlopeAngle = 0;
    }

    public bool CheckHead()
    {
        Vector3 playerCenter = _player.Rigidbody.position + _player.Collider.center;

        var distance = (_player.Collider.height / 2) - _player.Collider.radius;
        var ray = new Ray(playerCenter, _player.transform.up);

        return Physics.SphereCast(ray, _player.Collider.radius - _castRadiusOffset, 
                                  out var hitInfo, _distanceToCheckHead);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_player.Config.WallMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            _player.StatesData.IsWalled = true;
            _player.StatesData.WallNormal = collision.contacts[0].normal;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if ((_player.Config.WallMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            _player.StatesData.IsWalled = false;
        }
    }
}
