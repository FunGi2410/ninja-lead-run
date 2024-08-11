using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject lineWarning;
    [SerializeField] private float timeToMaxSpeed;
    private Rigidbody mRb;
    private bool isBreak;

    private void Awake()
    {
        this.mRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(MaxAccelerationMove(timeToMaxSpeed));
    }

    /*    private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Move();
        }*/

    private void FixedUpdate()
    {
        this.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile")) return;
        this.isBreak = true;
        this.SetSpeed(0f);
        Destroy(gameObject, 3f);
    }

    void Move()
    {
        if (this.isBreak) return;
        //this.mRb.AddForce(this.speed * Vector3.forward, ForceMode.Impulse);
        this.mRb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * this.speed);
        //this.mRb.velocity = Vector3.Lerp(this.mRb.velocity, transform.forward * this.speed, Time.fixedDeltaTime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    IEnumerator MaxAccelerationMove(float time)
    {
        yield return new WaitForSeconds(time);

        this.SetSpeed(this.maxSpeed);
        //Move();
        StopCoroutine(MaxAccelerationMove(time));
    }
}
