using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapExit : MonoBehaviour {
    [SerializeField]
    public string[] _levels;
    private bool _loadingTown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!_loadingTown)
                StartCoroutine(LoadTown());
        }
    }

    IEnumerator LoadTown()
    {
        _loadingTown = true;
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);

        // Deactivate Zombies
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (Zombie zombie in zombies)
        {
            Destroy(zombie);
        }

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
        else
        {
            yield return new WaitForSeconds(3);
        }

        // pick a random level
        System.Random random = new System.Random();
        int index = random.Next(0, _levels.Length);
        string level = _levels[index];
        SceneManager.LoadScene(level);
    }
}
