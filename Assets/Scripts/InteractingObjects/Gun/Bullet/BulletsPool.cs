using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Bullet _template;
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<Bullet> _pool = new List<Bullet>();

    private void Start()
    {
        Initialize(_template);
    }

    private void Initialize(Bullet prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Bullet spawned = Instantiate(prefab, _container);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    public bool TryGetObject(out Bullet result)
    {
        result = _pool.FirstOrDefault(pool => pool.gameObject.activeSelf == false);

        return result != null;
    }
}
