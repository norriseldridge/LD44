using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon {
    [SerializeField]
    private Transform _spriteTranform;
    private bool _canShoot;
    [SerializeField]
    private float _shotDelay;
    [SerializeField]
    private string _ammoKey;
    [SerializeField]
    private SpriteRenderer _muzzleFlash;
    private WeaponWielder _wielder;
    [SerializeField]
    private AudioClip _misFireSound;

    // Use this for initialization
    void Start () {
        _canShoot = true;
        _weaponType = WeaponType.RequiresAmmo;
        _muzzleFlash.color = Color.clear;
    }
	
	// Update is called once per frame
	void Update () {
        if (_wielder == null)
            _wielder = GetComponentInParent<WeaponWielder>();
        else
        {
            // Raycast Mouse Looking
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _spriteTranform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bool flip = angle > 90.0f || angle < -90.0f;
            _spriteTranform.localScale = new Vector3(1, flip ? -1 : 1, 1);
        }
    }

    public int GetAmmo()
    {
        return PlayerModel.GetAmmo(_ammoKey);
    }

    public override void Use()
    {
        if (GetAmmo() <= 0)
        {
            // Play a click / misfire sound
            PlayClip(_misFireSound, true);
            return;
        }

        if (_canShoot)
            StartCoroutine(PerformShoot());
    }

    private IEnumerator PerformShoot()
    {
        _canShoot = false;

        // flash
        StartCoroutine(ShowMuzzleFlash());

        // Do the base level "Use"
        base.Use();

        // decrease ammo
        PlayerModel.ReduceAmmo(_ammoKey);

        // Wait
        yield return new WaitForSeconds(_shotDelay);

        _canShoot = true;
    }

    private IEnumerator ShowMuzzleFlash()
    {
        int frames = 7;
        for (int i = 0; i < frames; ++i)
        {
            _muzzleFlash.color = new Color(1, 1, 1, i / (frames - 1));
            yield return null;
        }

        _muzzleFlash.color = Color.clear;
    }
}
