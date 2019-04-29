using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll(); // clear player prefs
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
	}

}
