using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleMagic : MonoBehaviour {
    public float puissance = 30;

    public float lifeTime;

    private float age;

    public void OnCollisionEnter(Collision collision) {
        AIMover other = collision.gameObject.GetComponent<AIMover>();
        if (other != null) {
            other.life -= puissance;
        }

        PlayerMover player = collision.gameObject.GetComponent<PlayerMover>();
        if (player != null) {
            return;
        }

        Destroy(gameObject);
    }

    private void Update() {
        age += Time.deltaTime;
        if (age > lifeTime) {
            Destroy(gameObject);
        }
    }

}