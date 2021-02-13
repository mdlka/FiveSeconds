using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenShots;
    [SerializeField] private Transform _shotPoint; 
    [SerializeField] private BulletsPool _pool;
    [SerializeField] private ParticleSystem _shot;

    private float _elapsed;

    private void Update()
    {
        _elapsed += Time.deltaTime;

        if(_elapsed > _secondsBetweenShots)
        {
            _elapsed = 0;

            if (_pool.TryGetObject(out Bullet bullet))
            {
                bullet.transform.position = _shotPoint.position;
                bullet.transform.rotation = transform.rotation;
                bullet.gameObject.SetActive(true);
                _shot.Play();
            }
        }
    }
}
