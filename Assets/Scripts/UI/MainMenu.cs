using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Image _black;

	// Use this for initialization
	void Start () 
    {
        _playButton.onClick.AddListener(OnClickPlay);

    }

	private void OnClickPlay()
    {
        StartCoroutine(GoToTown());
    }

    private IEnumerator GoToTown()
    {
        _black.enabled = true;
        // Fade music
        GameObject musicPlayer = GameObject.FindWithTag("MusicPlayer");
        if (musicPlayer != null)
        {
            float start = musicPlayer.GetComponent<AudioSource>().volume;
            AudioSource source = musicPlayer.GetComponent<AudioSource>();
            for (float i = 0; i < 30; ++i)
            {
                _black.color = new Color(0, 0, 0, (i / 30.0f));
                source.volume = (1.0f - (i / 30.0f)) * start;
                yield return null;
            }
            _black.color = Color.black;
            source.volume = 0.0f;
            yield return new WaitForSeconds(2);
        }

        SceneManager.LoadScene("Town");
    }
}
