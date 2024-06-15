using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    private float moveInput;
    private float steerInput;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float steerSpeed;
    [SerializeField] private float gravity;
    Vector3 velocity;

    private float rayLength;
    private float tiltIncrement = 0.09f;
    private float zTiltAngle = 45f;
    private float curVelocityOffset;

    [SerializeField] private Rigidbody sphereRb, bikeBodyRb;

    [SerializeField] LayerMask surfaceMask;
    RaycastHit hit;

    private void Awake()
    {
        this.rayLength = sphereRb.GetComponent<SphereCollider>().radius + 0.2f;

        this.sphereRb.transform.parent = null;
        this.bikeBodyRb.transform.parent = null;
    }

    private void Update()
    {
        this.moveInput = Input.GetAxis("Vertical");
        this.steerInput = Input.GetAxis("Horizontal");

        transform.position = this.sphereRb.transform.position;
        //this.bikeBodyRb.MoveRotation(transform.rotation);

        this.velocity = this.bikeBodyRb.transform.InverseTransformDirection(this.bikeBodyRb.velocity);
        this.curVelocityOffset = velocity.z / this.maxSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (this.Grounded())
        {
            this.Acceleration();
            this.Rotation();
        }
        else Gravity();
        this.Tilt();
    }

    void Acceleration()
    {
        sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, transform.forward * this.maxSpeed * this.moveInput, Time.fixedDeltaTime * this.acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, this.steerInput * this.moveInput * this.curVelocityOffset * this.steerSpeed * Time.fixedDeltaTime, 0, Space.World);
    
        // Rotate Handle
    }

    bool Grounded()
    {
        return Physics.Raycast(sphereRb.position, Vector3.down, out hit, this.rayLength, this.surfaceMask);
    }

    void Gravity()
    {
        this.sphereRb.AddForce(this.gravity * Vector3.down, ForceMode.Acceleration);
    }

    void Tilt()
    {
        float xRot = (Quaternion.FromToRotation(this.bikeBodyRb.transform.up, hit.normal) * this.bikeBodyRb.transform.rotation).eulerAngles.x;
        float zRot = 0;
        if(this.curVelocityOffset > 0)
            zRot = -this.zTiltAngle * this.steerInput * this.curVelocityOffset;

        Quaternion targetRot = Quaternion.Slerp(this.bikeBodyRb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), this.tiltIncrement);
        Quaternion newRot = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        this.bikeBodyRb.MoveRotation(newRot);
    }
}
