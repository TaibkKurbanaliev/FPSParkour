using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BestTimes", menuName = "Config/BestTimes")]
public class BestTimes : ScriptableObject
{
    [SerializeField] private int _maxTimesCount;
    private List<KeyValuePair<float, string>> _times = new();

    public IEnumerable<KeyValuePair<float, string>> Times => _times;

    public void AddTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        string format = @"mm\:ss\:fff";

        _times.Add(new KeyValuePair<float, string>(time, timeSpan.ToString(format)));
        _times = _times.OrderBy(value => value.Key).ToList();

        if (_times.Count > _maxTimesCount)
            _times.RemoveAt(_maxTimesCount);
    }
}
