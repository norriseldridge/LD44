using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepth : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () 
    {
        _spriteRenderer.sortingOrder = (int)(transform.position.y * -1000);
    }
}
