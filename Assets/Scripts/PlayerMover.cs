using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
    public Transform objectToThrow;
    public Transform playerCam;

    public float mouseSensivityX;
    public float mouseSensivityY;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start() {
        if (playerCam == null) {
            Camera cam = transform.GetComponentInChildren<Camera>();
            playerCam = cam.transform;
        }
    }

    private void Update() {
        //Sauve la rotation
        Quaternion lastRotation = playerCam.rotation;

        //Baisse / leve la tete
        float rot = Input.GetAxis("Mouse Y") * -mouseSensivityY * Time.deltaTime;
        Quaternion q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        //Est ce qu'on a la tete à l'envers ?
        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;
        float regardeDevant = Vector3.Dot(forwardCam, forwardPlayer);
        if (regardeDevant < 0.0f)
            playerCam.rotation = lastRotation;

        //Tourner gauche droite 
        rot = Input.GetAxis("Mouse X") * mouseSensivityX * Time.deltaTime;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

        if (Input.GetButtonDown("Fire1")) {
            Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
            obj.position = playerCam.position + playerCam.forward;
            obj.GetComponent<Rigidbody>().AddForce(playerCam.forward * 40, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();

        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        Vector3 horizontalVelocity = Vector3.zero;
        horizontalVelocity += hori * transform.right * 10f;
        horizontalVelocity += vert * transform.forward * 10f;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);

        //Tomber plus vite
        if (rb.velocity.y < 0)
            rb.AddForce(-transform.up * 30f);

        //Est ce qu'on touche le sol ?
        isGrounded = false;
        RaycastHit infos;
        bool hit = Physics.SphereCast(transform.position + transform.up * 0.3f,
            0.05f, -transform.up, out infos, 2f);
        if (hit && infos.distance < 0.3f) {
            isGrounded = true;
        }

        if (isGrounded && Input.GetButton("Jump")) {
            rb.AddForce(transform.up * 6f, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}