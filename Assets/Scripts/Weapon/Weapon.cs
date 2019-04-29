using UnityEngine;

public enum WeaponType
{
    Melee,
    RequiresAmmo
}

public class Weapon : MonoBehaviour {
    [SerializeField]
    private Projectile _projectileSource;
    [SerializeField]
    private float _projectileLifeTime;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private int _damage;
    public int Damage
    {
        get
        {
            return _damage;
        }
    }
    [SerializeField]
    protected WeaponType _weaponType;
    public WeaponType WeaponType
    {
        get
        {
            return _weaponType;
        }
    }
    [SerializeField]
    private AudioClip _soundClip;
    private AudioSource _soundSource;

    public virtual void Use()
    {
        // Spawn a projection
        Projectile projectile = Instantiate(_projectileSource);
        projectile.transform.position = transform.position;
        projectile.lifeTime = _projectileLifeTime;
        projectile.speed = _projectileSpeed;
        projectile.weapon = this;
        projectile.SetTrajectory(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));

        // Play audio
        PlayClip(_soundClip);
    }

    protected void PlayClip(AudioClip clip, bool onlyIfNotPlaying = false)
    {
        if (gameObject.GetComponent<AudioSource>() == null)
            gameObject.AddComponent<AudioSource>();
        _soundSource = gameObject.GetComponent<AudioSource>();

        if (onlyIfNotPlaying)
            if (_soundSource.isPlaying)
                return;

        _soundSource.volume = 0.3f;
        _soundSource.pitch = 1 + (Random.value * 0.25f);
        _soundSource.PlayOneShot(clip);
    }
}
