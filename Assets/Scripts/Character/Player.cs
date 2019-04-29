using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed;
    private Vector3 _currentVelocity = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Direction _direction;
    private Human _human;
    private bool _dead;

    [SerializeField]
    private WeaponWielder _wielder;

	// Use this for initialization
	void Start () {
        _dead = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _wielder = GetComponent<WeaponWielder>();
        _direction = Direction.Right;

        // Starting health amount
        _human = GetComponent<Human>();
        _human.SetStartingHealth(15);
        _human.SetHealth(PlayerModel.GetHealth());
    }

    private IEnumerator GoToDead()
    {
        StartCoroutine(FallOver());

        // Fade music
        GameObject musicPlayer = GameObject.FindWithTag("MusicPlayer");
        if (musicPlayer != null)
        {
            float start = musicPlayer.GetComponent<AudioSource>().volume;
            AudioSource source = musicPlayer.GetComponent<AudioSource>();
            for (float i = 0; i < 30; ++i)
            {
                source.volume = (1.0f - (i / 30.0f)) * start;
                yield return null;
            }
            source.volume = 0.0f;
            yield return new WaitForSeconds(2);
        }

        SceneManager.LoadScene("DEad");
    }

    private IEnumerator FallOver()
    {
        _animator.Play("PlayerIdle");
        float targetAngle = Random.value > 0.5f ? 90 : -90;

        for (int i = 0; i < 50; ++i)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 2, targetAngle), i * Time.deltaTime);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
        if (_dead)
            return;

        // Update health
        PlayerModel.SetHealth(_human.GetHeath());

        if (PlayerModel.GetHealth() <= 0)
        {
            StartCoroutine(GoToDead());
            _dead = true;
            return;
        }

        // Make sure that if we have a weapon owned, we have it equipped
        if (_wielder.CurrentWeapon == null)
            _wielder.EquipWeapon(PlayerModel.GetCurrentWeapon());

        // If we are in the shop, do nothing else
        if (PlayerModel.IsInShop)
            return;

        // Handle player movement
        _currentVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _currentVelocity.y = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _currentVelocity.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _currentVelocity.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _currentVelocity.x = 1;
        }

        // Flipped?
        _direction = (Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x) ? Direction.Left : Direction.Right;
        _spriteRenderer.flipX = _direction == Direction.Left;
        _wielder.SetDirection(_direction);

        // Did we move?
        if (_currentVelocity.magnitude > 0)
        {
            transform.Translate(_currentVelocity.normalized * _speed * Time.deltaTime);
            _animator.Play("PlayerWalk");
        }
        else
        {
            _animator.Play("PlayerIdle");
        }

        // Handle attack
        if (Input.GetMouseButton(0))
        {
            _wielder.Attack();
        }

        // Handle switching weapons
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Cycle weapons left
            _wielder.UnequipWeapon();
            PlayerModel.CycleWeaponLeft();
            _wielder.EquipWeapon(PlayerModel.GetCurrentWeapon());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cycle weapons right
            _wielder.UnequipWeapon();
            PlayerModel.CycleWeaponRight();
            _wielder.EquipWeapon(PlayerModel.GetCurrentWeapon());
        }
    }
}
