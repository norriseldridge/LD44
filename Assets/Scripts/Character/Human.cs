using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {
    [SerializeField]
    private float _health;
    private float _maxHealth;

    public void SetStartingHealth(float health)
    {
        _health = health;
        _maxHealth = health;
        _healthText.text = "X " + _health;
    }

    public void SetHealth(float health)
    {
        _health = health;
        _healthText.text = "X " + _health;
    }

    public float GetHeath()
    {
        return _health;
    }

    [SerializeField]
    private Text _healthText;

    private void Start()
    {
        _maxHealth = _health;
        _healthText.text = "X " + _health;
    }

    private void TakeProjectileHit(Weapon weapon)
    {
        if (weapon == null)
            return;

        WeaponWielder weaponWielder = GetComponentInParent<WeaponWielder>();
        if (weaponWielder != weapon.GetComponentInParent<WeaponWielder>())
        {
            RemoveHeath(weapon.Damage);
        }
    }

    public void RemoveHeath(float amount)
    {
        _health -= amount;
        _healthText.text = "X " + _health;
    }

    private void PickUp(Heart heart)
    {
        _health += heart.Health;
        Destroy(heart.gameObject);
        _healthText.text = "X " + _health;
    }
}
