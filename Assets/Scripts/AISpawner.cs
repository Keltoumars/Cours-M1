using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public Transform prefabAI;
    public float time = 0;
    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }
    Transform SpawnAI()
    {
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        ai.position = transform.position;
        ai.rotation = transform.rotation;
        return ai;
    }
    void AddPichenette(Transform ai, Vector3 pichenette)

    {

        Rigidbody rb = ai.GetComponent<Rigidbody>();
        rb.AddForce(pichenette, ForceMode.Impulse);
    }
    [Range(0, 15)]
    public float timeMax = 1;
    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if (time >= timeMax)
        {
            SpawnAI();
            time = 0;
            Transform ai = SpawnAI();
            AddPichenette(ai, ai.forward * 5);

        }

    }
}