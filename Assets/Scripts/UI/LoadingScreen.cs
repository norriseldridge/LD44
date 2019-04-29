using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    [SerializeField]
    private Image _backgroundImage;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float frames = 60;
        for (float i = 0; i < frames; ++i)
        {
            _backgroundImage.color = new Color(0, 0, 0, i / (frames - 1));
            yield return null;
        }
    }
}
