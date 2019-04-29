using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField]
    private Text _ammoText;
    private Player _player;
    [SerializeField]
    private GameObject _gunSection;

    // Use this for initialization
    void Start () {
        _player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        Weapon currentWeapon = _player.GetComponent<WeaponWielder>().CurrentWeapon;
        _gunSection.SetActive(currentWeapon != null && currentWeapon.WeaponType == WeaponType.RequiresAmmo);
        if (currentWeapon != null)
        {
            if (currentWeapon.WeaponType == WeaponType.RequiresAmmo)
            {
                _ammoText.text = string.Format("X {0}", ((GunWeapon)currentWeapon).GetAmmo());
            }
        }
    }
}
