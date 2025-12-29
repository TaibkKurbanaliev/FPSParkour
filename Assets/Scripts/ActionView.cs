using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionView : MonoBehaviour
{
    [SerializeField] private Image _reloadImage;
    [SerializeField] private Player _player;


    private void OnEnable()
    {
        _player.StatesData.DashReloadingStarted += OnDashReloadStarted;
    }

    private void OnDisable()
    {
        _player.StatesData.DashReloadingStarted -= OnDashReloadStarted;
    }

    private void OnDashReloadStarted(float reloadTime)
    {
        StartReloadingAnim(reloadTime).Forget();
    }

    private async UniTask StartReloadingAnim(float reloadTime)
    {
        var currentTime = 0f;

        while (currentTime < reloadTime)
        {
            await UniTask.Yield(cancellationToken: destroyCancellationToken);
            _reloadImage.fillAmount = 1f - currentTime / reloadTime;
            currentTime += Time.deltaTime;
        }

        _reloadImage.fillAmount = 0f;
    }
}
