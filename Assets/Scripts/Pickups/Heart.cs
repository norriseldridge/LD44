using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    [SerializeField]
    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.BroadcastMessage("PickUp", this, SendMessageOptions.DontRequireReceiver);
    }
}
