using UnityEngine;

public class FPSTarget : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = 30;
	}
}
