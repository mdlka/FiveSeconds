using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;

    private float _distance;

    private void Start()
    {
        _distance = Vector2.Distance(_startPoint.position, _endPoint.position);

        StartCoroutine(LerpMoveObject(_startPoint.position, _endPoint.position, _speed));
    }

    private IEnumerator LerpMoveObject(Vector2 startPosition, Vector2 endPosition, float speed)
    {
        float startTime = Time.time;
        float coveredDistance = 0;

        while (coveredDistance < _distance)
        {
            coveredDistance = (Time.time - startTime) * speed;
            transform.position = Vector2.Lerp(startPosition, endPosition, coveredDistance / _distance);

            yield return null;
        }

        StartCoroutine(LerpMoveObject(endPosition, startPosition, speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.transform.SetParent(null);
        }
    }
}
