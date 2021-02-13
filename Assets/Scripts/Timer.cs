using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _remainingTime;

    public event UnityAction<int> TimeChanged;
    public event UnityAction TimeOver;

    private void Start()
    {
        TimeChanged?.Invoke(_remainingTime);

        StartCoroutine(DecreaseTime());
    }

    private IEnumerator DecreaseTime()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (_remainingTime > 0)
        {
            yield return waitForSeconds;

            _remainingTime -= 1;
            TimeChanged?.Invoke(_remainingTime);
        }

        TimeOver?.Invoke();
    }
}
