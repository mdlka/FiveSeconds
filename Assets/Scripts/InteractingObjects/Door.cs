using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private float _closedOffsetY;
    [SerializeField] private float _closingDuration;

    private Vector2 _basePosition;

    private void OnEnable()
    {
        _basePosition = transform.position;
        transform.position += new Vector3(0, _closedOffsetY);

        _timer.TimeOver += OnTimeOver;
    }

    private void OnDisable()
    {
        _timer.TimeOver -= OnTimeOver;
    }

    private void OnTimeOver()
    {
        StartCoroutine(LerpDoorClosing(transform.position, _basePosition, _closingDuration));
    }

    private IEnumerator LerpDoorClosing(Vector2 startPosition, Vector2 endPosition, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);

            yield return null;
        }
    }
}
