using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FakeObject : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            StartCoroutine(LerpFade(_spriteRenderer.color.a, 0, _fadeDuration));
        }
    }

    private IEnumerator LerpFade(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0;
        Color spriteColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a);

        while (spriteColor.a > 0)
        {
            elapsedTime += Time.unscaledDeltaTime;

            spriteColor.a = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            _spriteRenderer.color = spriteColor;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
