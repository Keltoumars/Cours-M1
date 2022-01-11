using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
    public Transform objectToThrow;
    public Transform playerCam;
    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            Camera cam = transform.GetComponentInChildren<Camera>();
            playerCam = cam.transform;
        }
    }

    private void Update()
    {
        //Sauve la rotation
        Quaternion lastRotation = playerCam.rotation;

        //Baisse / leve la tete
        float rot = Input.GetAxis("Mouse Y") * -10;
        Quaternion q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        //Est ce qu'on a la tete à l'envers ?
        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;
        float regardeDevant = Vector3.Dot(forwardCam, forwardPlayer);
        if (regardeDevant < 0.0f)
            playerCam.rotation = lastRotation;

        //Tourner gauche droite 

        rot = Input.GetAxis("Mouse Y") * 10;
        q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        if(Input.GetButtonDown("Fire1"))

        { Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
            obj.position = playerCam.position + playerCam.forward;
            obj.GetComponent<Rigidbody>().AddForce(playerCam.forward * 40, ForceMode.Impulse);

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Rigidbody rb;
        rb = GetComponent<Rigidbody>();

        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        Vector3 horizontalVelocity = Vector3.zero;
        horizontalVelocity += vert * transform.forward * 10;
        horizontalVelocity += hori * transform.forward * 10;
        rb.velocity = new Vector3(horizontalVelocity.x,
            rb.velocity.y,
            horizontalVelocity.z);

        //Tomber plus vite
        if (rb.velocity.y < 0)
            rb.AddForce(-transform.up * 30);


        //Est ce qu'on touche le sol ?
        isGrounded = false;
        RaycastHit infos;
        bool trouve = Physics.SphereCast(transform.position + transform.up * 0.1f,
            0.05f, -transform.up, out infos, 2);
        if (trouve && infos.distance < 0.05)
            isGrounded = true;



        if (Input.GetButton("Jump"))
        {

            rb.AddForce(transform.up * 20, ForceMode.Impulse);
            isGrounded = false;

        }
        else
        {
            rb.AddForce(transform.up * 30);
            if (rb.velocity.y > 1)
                rb.AddForce(transform.up * 50);
            else
                rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);
        }

    }

}
