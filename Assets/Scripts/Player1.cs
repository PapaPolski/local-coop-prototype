using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour {

    public float moveSpeed;
    public bool canFire;

    private BowController bow;
    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

   

    void Start() {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        bow = GetComponent<BowController>();
        canFire = true;
    }

    void Update() {

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
}
