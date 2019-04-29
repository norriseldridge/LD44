using System.Collections;
using UnityEngine;

public class LevelLogic : MonoBehaviour {
    [SerializeField]
    private float _levelDuration;

	// Use this for initialization
	void Start () {
        StartCoroutine(StopSpawners());
	}

    private IEnumerator StopSpawners()
    {
        yield return new WaitForSeconds(_levelDuration);

        ZombieSpawner[] zombieSpawners = FindObjectsOfType<ZombieSpawner>();
        foreach (ZombieSpawner zombieSpawner in zombieSpawners)
        {
            Destroy(zombieSpawner);
        }
    }
}
