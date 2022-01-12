using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {
    public Transform prefabAI;
    public float time = 0;
    public Transform SpawnPoint;

    [Range(0, 15)]
    public float timeMax = 1;

    private float timeToReach;

    // Start is called before the first frame update
    void Start() {
        timeToReach = Random.Range(2f, timeMax);
    }

    Transform SpawnAI() {
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        ai.position = SpawnPoint.position;
        ai.rotation = SpawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai, Vector3 pichenette) {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
        rb.AddForce(pichenette, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update() {
        time = time + Time.deltaTime;
        if (time >= timeMax) {
            time = 0f;
            Transform ai = SpawnAI();
            // AddPichenette(ai, ai.forward * 5);
            timeToReach = Random.Range(2f, timeMax);
        }
    }
}