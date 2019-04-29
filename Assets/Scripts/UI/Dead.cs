using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dead : MonoBehaviour {
    [SerializeField]
    private Button _menuButton;

	// Use this for initialization
	void Start () {
        _menuButton.onClick.AddListener(OnClickBack);
    }
	
	private void OnClickBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
