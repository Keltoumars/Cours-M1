using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [Tooltip("Vitesse de déplacement"), Range(1, 15)]
    public float linearSpeed = 6;
    [Tooltip("Vitesse de rotation"), Range(1, 5)]
    public float angularSpeed = 1;

    private Transform player;

    public Vector3 dirPlayer;

    public float life = 100;

    public void Start()
    {
        GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        player = goPlayer.transform;
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            dirPlayer = player.position - transform.position;
            dirPlayer = dirPlayer.normalized;

            float angle = Vector3.SignedAngle(dirPlayer,
                transform.forward,
                transform.up);

            if (angle > 4)
                rb.AddTorque(transform.up * -5);
            else
            if (angle < -4)
                rb.AddTorque(transform.up * 5);

            if (Mathf.Abs(angle) < 10 && rb.velocity.magnitude < 3)
            {
                rb.AddForce(transform.forward * 40);
            }

            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetFloat("Speed", rb.velocity.magnitude);
            }
        }

        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + dirPlayer);
    }
}