using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private Coroutine _timerRoutine;

    public float TimeInMiliseconds {  get; private set; }

    public void StartTimer()
    {
        _timerRoutine = StartCoroutine(CountTime());
    }

    public void StopTimer()
    {
        StopCoroutine(_timerRoutine);
    }

    private IEnumerator CountTime()
    {
        while (true)
        {
            TimeInMiliseconds += Time.deltaTime;
            _timerText.text = FormatTime(TimeInMiliseconds);
            yield return null;
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        string format = @"mm\:ss\:fff";

        // Use ToString with the custom format
        return timeSpan.ToString(format);
    }
}
