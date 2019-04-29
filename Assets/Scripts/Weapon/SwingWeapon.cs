using System.Collections;
using UnityEngine;

public class SwingWeapon : Weapon {
    [SerializeField]
    private Transform _spriteTranform;
    private bool _canSwing;

    [SerializeField]
    private int _swingSpeed;

    [SerializeField]
    private int _swingCount;

    [SerializeField]
    private float _swingRange;

    private Direction _direction;
    private WeaponWielder _wielder;

    private void Start()
    {
        _canSwing = true;
        _direction = Direction.Right;
        _weaponType = WeaponType.Melee;
    }

    private void Update()
    {
        if (_wielder == null)
            _wielder = GetComponentInParent<WeaponWielder>();
        else
        {
            if (_direction != _wielder.Direction)
            {
                _direction = _wielder.Direction;
                transform.Rotate(Vector3.up * 180);
            }
        }
    }

    public override void Use()
    {
        if (_canSwing)
            StartCoroutine(PerformSwing());
    }

    private IEnumerator PerformSwing()
    {
        _canSwing = false;

        // Do the base level "Use"
        base.Use();

        // Do the swing animation here
        Vector3 swingRotation = new Vector3(0, 0, -_swingSpeed);
        for (int i = 0; i < _swingCount; ++i)
        {
            _spriteTranform.localPosition += Vector3.right * _swingRange * Time.deltaTime;
            _spriteTranform.Rotate(swingRotation);
            yield return null;
        }

        // Undo the swing animation here
        swingRotation = new Vector3(0, 0, _swingSpeed / 2);
        for (int i = 0; i < _swingCount * 2; ++i)
        {
            _spriteTranform.localPosition += Vector3.left * _swingRange * 0.5f * Time.deltaTime;
            _spriteTranform.Rotate(swingRotation);
            yield return null;
        }

        _canSwing = true;
    }
}
