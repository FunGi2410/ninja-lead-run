using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Vector3> targetPoints;
    private int curPointIndex;
    [SerializeField] private float distance;
    private float disLimit = 1;
    bool isActive = false;
    private Rigidbody mRb;
    private Collider mCollider;

    //VehicleActivator vehicleActivator;

    private void Awake()
    {
        this.mCollider = GetComponent<Collider>();
        this.mRb = GetComponent<Rigidbody>();
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
        targetPoints.Add(target);
        //this.tile = transform.parent.parent.GetComponent<Tile>();

        //this.vehicleActivator = transform.parent.GetComponent<VehicleActivator>();
        //this.vehicleActivator.OnPlayerDetected += ActiveMovement;
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, targetPoints[0], Color.red);
        //if (!isActive) return;
        this.Movement();
    }

    void ActiveMovement()
    {
        this.isActive = true;
    }
    void Movement()
    {
        //if (!this.tile.IsBikeArrive) return;
       /* if (curPointIndex >= targetPoints.Count)
            return;*/
        /*transform.position = Vector3.MoveTowards(transform.position, targetPoints[curPointIndex], Time.deltaTime * this.speed);
        this.mRb.velocity = Vector3.Lerp(this.mRb.velocity, transform.forward * this.speed, Time.fixedDeltaTime);*/
        this.mRb.MovePosition(this.mRb.position + (transform.forward * speed * Time.fixedDeltaTime));

        /*if (Vector3.Distance(targetPoints[curPointIndex], transform.position) < disLimit)
        {
            curPointIndex++;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            Physics.IgnoreCollision(collision.collider, this.mCollider);
        }
    }
}
