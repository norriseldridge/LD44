using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector2 _trajectory;
    public float lifeTime;
    public float speed;
    public Weapon weapon;
	
    public void SetTrajectory(Vector2 trajectory)
    {
        _trajectory = trajectory.normalized;
    }

	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
            Destroy(gameObject);

        transform.Translate(_trajectory * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.BroadcastMessage("TakeProjectileHit", weapon, SendMessageOptions.DontRequireReceiver);
    }
}
