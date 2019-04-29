using UnityEngine;

public class WeaponWielder : MonoBehaviour {
    [SerializeField]
    private Weapon _currentWeapon;
    public Weapon CurrentWeapon
    {
        get
        {
            return _currentWeapon;
        }
    }

    public void EquipWeapon(string weaponName)
    {
        if (weaponName == null)
            return;

        _currentWeapon = Instantiate(WeaponLookup.GetInstance().GetWeaponByName(weaponName), transform);
    }

    public void UnequipWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.transform.SetParent(null);
            Destroy(_currentWeapon.gameObject);
        }
    }

    private Direction _direction;
    public Direction Direction
    {
        get
        {
            return _direction;
        }
    }

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

	public void Attack()
    {
        if (_currentWeapon != null)
            _currentWeapon.Use();
    }
}
