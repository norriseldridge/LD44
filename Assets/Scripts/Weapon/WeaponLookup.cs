using System.Collections.Generic;
using UnityEngine;

public class WeaponLookup : MonoBehaviour
{
    public static string BaseballBat = "BaseballBat";
    public static string NinjaSword = "NinjaSword";
    public static string Pistol = "Pistol";
    public static string Shotgun = "Shotgun";

    [SerializeField]
    private Weapon _baseballBat;
    [SerializeField]
    private Weapon _ninjaSword;
    [SerializeField]
    private Weapon _pistol;
    [SerializeField]
    private Weapon _shotgun;
    private Dictionary<string, Weapon> _weaponLookup = new Dictionary<string, Weapon>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _weaponLookup.Add(BaseballBat, _baseballBat);
        _weaponLookup.Add(NinjaSword, _ninjaSword);
        _weaponLookup.Add(Pistol, _pistol);
        _weaponLookup.Add(Shotgun, _shotgun);
    }

    public static WeaponLookup GetInstance()
    {
        return FindObjectOfType<WeaponLookup>();
    }

    public Weapon GetWeaponByName(string name)
    {
        return _weaponLookup[name];
    }
}
