using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
    [SerializeField]
    private Zombie _zombieSource;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}

    IEnumerator SpawnEnemy()
    {
        while (this)
        {
            yield return new WaitForSeconds(Random.value * 5);
            Zombie zombie = Instantiate(_zombieSource);
            zombie.transform.position = transform.position;
        }
    }
}
