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

    [SerializeField] private Rigidbody sphereRb, bikeBodyRb;

    private void Awake()
    {
        this.sphereRb.transform.parent = null;
        this.bikeBodyRb.transform.parent = null;
    }

    private void Update()
    {
        this.moveInput = Input.GetAxis("Vertical");
        this.steerInput = Input.GetAxis("Horizontal");

        transform.position = this.sphereRb.transform.position;
        this.bikeBodyRb.MoveRotation(transform.rotation);
    }

    private void FixedUpdate()
    {
        this.Movement();
        this.Rotation();
    }

    void Movement()
    {
        sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, transform.forward * this.maxSpeed * this.moveInput, Time.fixedDeltaTime * this.acceleration);
    }

    void Rotation()
    {
        transform.Rotate(0, this.steerInput * this.moveInput * this.steerSpeed * Time.fixedDeltaTime, 0, Space.World);
    }
}
