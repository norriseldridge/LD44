using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {
    [SerializeField]
    private float _lifeTime;
    private float _currentLifeTime;

    [SerializeField]
    private float _growSpeed;

    [SerializeField]
    private float _alphaSpeed;

    private SpriteRenderer _spriteRenderer;
    private float _alpha;
    private bool _shouldAnimate = true;
    public void SetShouldAnimate(bool val)
    {
        _shouldAnimate = val;
        _alpha = 1;
        transform.localScale = Vector3.one;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    private void Hide()
    {
        transform.position = new Vector3(100000, 100000, 0);
        _alpha = 0;
    }

    public void Reset()
    {
        _alpha = 2;
        _currentLifeTime = _lifeTime;
        transform.localScale = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, Random.value * 360);
    }

    // Update is called once per frame
    void Update () {
        if (_shouldAnimate)
        {
            _currentLifeTime -= Time.deltaTime;
            if (_currentLifeTime <= 0.0f)
                Hide();

            transform.localScale += Vector3.one * _growSpeed * Time.deltaTime;

            _alpha -= _alphaSpeed * Time.deltaTime;
            _spriteRenderer.color = new Color(1, 1, 1, _alpha);
        }
    }
}
