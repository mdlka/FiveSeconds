using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _timer.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _timer.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(int time)
    {
        _text.text = time.ToString();
    }
}
