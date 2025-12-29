using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Checkpoint _start;
    [SerializeField] private Checkpoint _finish;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _prefab;
    [SerializeField] private BestTimes _times;
    [SerializeField] private Button _reloadButton;


    private List<TMP_Text> _timesInContainer = new();

    private void Start()
    {
        AddNewTimes();
    }

    private void OnEnable()
    {
        _start.Reached += OnStartReached;
        _finish.Reached += OnFinishReached;
        _reloadButton.onClick.AddListener(OnReloadButtonClick);
    }

    private void OnDisable()
    {
        _start.Reached -= OnStartReached;
        _finish.Reached -= OnFinishReached;
        _reloadButton.onClick.RemoveListener(OnReloadButtonClick);
    }

    private void OnFinishReached()
    {
        _timer.StopTimer();
        _times.AddTime(_timer.TimeInMiliseconds);

        foreach (var time in _timesInContainer)
            Destroy(time.gameObject);

        _timesInContainer.Clear();
        AddNewTimes();
    }

    private void AddNewTimes()
    {
        foreach (var time in _times.Times)
        {
            var newTimeText = Instantiate(_prefab, _container);
            newTimeText.text = time.Value;
            _timesInContainer.Add(newTimeText);
        }
    }

    private void OnStartReached()
    {
        _timer.StartTimer();
    }

    private void OnReloadButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
