using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seller : MonoBehaviour {
    [SerializeField]
    private Transform _target;
    private SpriteRenderer _spriteRenderer;

    private bool _isPlayerInRange = false;
    private bool _isInShop = false;

	// Use this for initialization
	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        // Face the player
        _spriteRenderer.flipX = _target.position.x < transform.position.x;

        // Allow the player to talk to the NPC
        if (_isPlayerInRange && !_isInShop && Input.GetKeyDown(KeyCode.Space))
        {
            // Open the shop
            StartCoroutine(Shop());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            _isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            _isPlayerInRange = false;
    }

    IEnumerator Shop()
    {
        _isInShop = true;

        // Open shop scene additively
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
        yield return null;

        while (GameObject.FindWithTag("Shop") != null)
        {
            yield return null;
        }

        _isInShop = false;
    }
}
