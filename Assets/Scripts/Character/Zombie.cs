using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour {
    [SerializeField]
    private float _speed;
    private Human _targetHuman;
    private WeaponWielder _weaponWielder;
    private Direction _direction;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField]
    private float _health;
    private bool _isDead;
    [SerializeField]
    private Heart[] _heartSources;
    [SerializeField]
    private GameObject _bloodSplatterSource;

    // Use this for initialization
    void Start () {
        _direction = Direction.Right;
        _weaponWielder = GetComponent<WeaponWielder>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        StartCoroutine(UpdateNearestTarget());
        _isDead = false;

    }
	
	// Update is called once per frame
	void Update () {
        CheckDeath();

        if (_health > 0)
        {
            MoveToTargetHuman();
            AttackTargetHuman();
        }
    }

    private IEnumerator UpdateNearestTarget()
    {
        // Until this zombie is dead!
        while (gameObject && _health > 0.0f)
        {
            // Find the closest human
            Human[] humans = FindObjectsOfType<Human>();
            _targetHuman = null;
            float closestHumanDistance = float.MaxValue;
            foreach (Human human in humans)
            {
                float dist = Vector3.Distance(transform.position, human.transform.position);
                if (dist < closestHumanDistance)
                {
                    closestHumanDistance = dist;
                    _targetHuman = human;
                }
            }

            // wait 2 seconds before we check again
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator FallOver()
    {
        GetComponent<AudioSource>().volume = 0.3f;
        GetComponent<AudioSource>().pitch = 0.85f + Random.value * 0.2f;
        GetComponent<AudioSource>().Play();

        _weaponWielder.UnequipWeapon();
        _animator.Play("DeadZombie");
        float targetAngle = Random.value > 0.5f ? 90 : -90;

        for (int i = 0; i < 50; ++i) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 2, targetAngle), i * Time.deltaTime);
            yield return null;
        }

        // Bloodsplatter
        GameObject splatter = Instantiate(_bloodSplatterSource);
        splatter.transform.position = transform.position + new Vector3(Random.value * 0.1f, Random.value * 0.1f, 0);
        splatter.transform.rotation = Quaternion.Euler(0, 0, Random.value * 360);

        // Spawn a heart or a brain
        int index = (int)(Random.value * (_heartSources.Length));
        Heart heart = Instantiate(_heartSources[index]);
        heart.transform.position = transform.position + new Vector3(Random.value * 0.1f, Random.value * 0.1f, 0);
        heart.transform.rotation = Quaternion.Euler(0, 0, Random.value * 360);

        Destroy(GetComponent<ZDepth>());
        Destroy(GetComponent<BloodSpawner>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        Destroy(this);
    }

    private void TakeProjectileHit(Weapon weapon)
    {
        if (weapon == null)
            return;

        WeaponWielder weaponWielder = GetComponentInParent<WeaponWielder>();
        if (weaponWielder != weapon.GetComponentInParent<WeaponWielder>())
        {
            _health -= weapon.Damage;
        }
    }

    private void CheckDeath()
    {
        if (_health <= 0.0f)
        {
            if (!_isDead)
                StartCoroutine(FallOver());
            _isDead = true;
        }
    }

    private void MoveToTargetHuman()
    {
        if (_targetHuman != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetHuman.transform.position, _speed * Time.deltaTime);

            // Flipped?
            _direction = (transform.position.x > _targetHuman.transform.position.x) ? Direction.Left : Direction.Right;
            _spriteRenderer.flipX = _direction == Direction.Left;
        }

        _weaponWielder.SetDirection(_direction);
    }

    private void AttackTargetHuman()
    {
        if (_targetHuman != null)
        {
            if (Vector3.Distance(_targetHuman.transform.position, transform.position) < 0.2f)
            {
                // Do attack
                _weaponWielder.Attack();
            }
        }
    }
}
