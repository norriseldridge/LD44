using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapLogic : MonoBehaviour {
    [SerializeField]
    private Text _warningText;

    private void Start()
    {
        _warningText.color = Color.clear;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            // Stop map exit message
            StopCoroutine("WarnPlayer");
            _warningText.color = Color.clear;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            // Show map exit message
            StartCoroutine("WarnPlayer");
        }
    }

    IEnumerator WarnPlayer()
    {
        while (true)
        {
            _warningText.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);

            _warningText.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
