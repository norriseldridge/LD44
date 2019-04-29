using System.Collections.Generic;

public class PlayerModel
{
    public static bool HasVisitedShop = false;
    public static bool IsInShop = false;

    private static int CurrentWeaponIndex = 0;
    private static List<string> OwnedWeapons = new List<string>() { };
    public static string GetCurrentWeapon()
    {
        if (OwnedWeapons.Count == 0)
            return null;

        return OwnedWeapons[CurrentWeaponIndex];
    }

    public static string GetNextWeapon()
    {
        if (OwnedWeapons.Count == 0)
            return null;

        int nextIndex = CurrentWeaponIndex + 1;
        if (nextIndex >= OwnedWeapons.Count)
            nextIndex = 0;

        return OwnedWeapons[nextIndex];
    }

    public static string GetPreviousWeapon()
    {
        if (OwnedWeapons.Count == 0)
            return null;

        int previousIndex = CurrentWeaponIndex - 1;
        if (previousIndex < 0)
            previousIndex = OwnedWeapons.Count - 1;

        return OwnedWeapons[previousIndex];
    }

    public static bool HasWeapon(string weapon)
    {
        return OwnedWeapons.Contains(weapon);
    }

    public static void AddWeapon(string weapon)
    {
        if (!OwnedWeapons.Contains(weapon))
        {
            OwnedWeapons.Add(weapon);
        }
    }

    public static int GetWeaponCount()
    {
        return OwnedWeapons.Count;
    }

    public static void CycleWeaponLeft()
    {
        CurrentWeaponIndex -= 1;
        if (CurrentWeaponIndex < 0)
        {
            CurrentWeaponIndex = OwnedWeapons.Count - 1;
        }
    }

    public static void CycleWeaponRight()
    {
        CurrentWeaponIndex += 1;
        if (CurrentWeaponIndex >= OwnedWeapons.Count)
        {
            CurrentWeaponIndex = 0;
        }
    }

    public static float GetHealth()
    {
        if (!UnityEngine.PlayerPrefs.HasKey("PlayerHealth"))
        {
            UnityEngine.PlayerPrefs.SetFloat("PlayerHealth", 15);
            UnityEngine.PlayerPrefs.Save();
        }

        return UnityEngine.PlayerPrefs.GetFloat("PlayerHealth");
    }

    public static void SetHealth(float health)
    {
        UnityEngine.PlayerPrefs.SetFloat("PlayerHealth", health);
        UnityEngine.PlayerPrefs.Save();
    }

    public static int GetAmmo(string key)
    {
        return UnityEngine.PlayerPrefs.GetInt(key);
    }

    public static void AddAmmo(string key, int ammount)
    {
        int cur = GetAmmo(key);
        UnityEngine.PlayerPrefs.SetInt(key, cur + ammount);
        UnityEngine.PlayerPrefs.Save();
    }
}
