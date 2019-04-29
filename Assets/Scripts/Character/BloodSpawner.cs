using UnityEngine;

public class BloodSpawner : MonoBehaviour {
    [SerializeField]
    private BloodSplatter _bloodSplatterSorce;
    private void TakeProjectileHit(Weapon weapon)
    {
        if (weapon == null)
            return;

        WeaponWielder weaponWielder = GetComponentInParent<WeaponWielder>();
        if (weaponWielder != weapon.GetComponentInParent<WeaponWielder>())
        {
            SpawnSplatter();
        }
    }

    private BloodSplatter SpawnSplatter()
    {
        BloodSplatter splatter = Instantiate(_bloodSplatterSorce);
        splatter.transform.position = transform.position + new Vector3((1 - Random.value) * 0.1f, (1 - Random.value) * 0.1f, 0);
        splatter.Reset();
        return splatter;
    }
}
