using UnityEngine;

public class BikeController : MonoBehaviour
{
    private float steerInput;

    private string[] dataArray;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float steerSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float gravity;

    private float rayLength;
    private float tiltIncrement = 0.09f;
    private float zTiltAngle = 45f;

    [SerializeField] private Rigidbody sphereRb, bikeBodyRb;

    [SerializeField] LayerMask surfaceMask;
    RaycastHit hit;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float SteerSpeed { get => steerSpeed; set => steerSpeed = value; }

    private void Awake()
    {
        this.rayLength = sphereRb.GetComponent<SphereCollider>().radius + 0.2f;

        this.sphereRb.transform.parent = null;
        this.bikeBodyRb.transform.parent = null;

        ComController.WhenReceiveDataCall += ReceiveData;
    }

    private void Update()
    {
        this.SteerSpeed = Input.GetAxisRaw("Horizontal");
        //this.velocity = Input.GetAxisRaw("Vertical");

        transform.position = this.sphereRb.transform.position;

        /*this.velocity = this.bikeBodyRb.transform.InverseTransformDirection(this.bikeBodyRb.velocity);
        this.curVelocityOffset = velocity.z / this.MaxSpeed;*/
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void ReceiveData(string incomingString)
    {
        this.dataArray = incomingString.Split(',');
        float.TryParse(dataArray[0], out this.steerSpeed);
        float.TryParse(dataArray[1], out this.velocity);
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
        sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, transform.forward * this.MaxSpeed * this.velocity, Time.fixedDeltaTime * this.acceleration);
    }

    void Rotation()
    {
        //transform.Rotate(0, this.steerInput * this.curVelocityOffset * this.steerSpeed * Time.fixedDeltaTime, 0, Space.World);
        /*if (this.steerSpeed > 0)
            sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, Vector3.right * this.SteerSpeed * this.horizontalSpeed, Time.fixedDeltaTime * this.acceleration);
        else if (this.steerSpeed < 0)
            sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, Vector3.left * this.SteerSpeed * this.horizontalSpeed, Time.fixedDeltaTime * this.acceleration);*/

        sphereRb.velocity = Vector3.Lerp(sphereRb.velocity, Vector3.right * this.SteerSpeed * this.horizontalSpeed, Time.fixedDeltaTime * this.acceleration);

        // Rotate Handle
    }

    bool Grounded()
    {
        return Physics.Raycast(sphereRb.position, Vector3.down, out hit, this.rayLength, this.surfaceMask);
    }

    void Gravity()
    {
        this.sphereRb.AddForce(this.gravity * Vector3.down, ForceMode.Acceleration);
        //this.sphereRb.AddForce(this.gravity * Vector3.zero, ForceMode.Acceleration);
    }

    void Tilt()
    {
        float xRot = (Quaternion.FromToRotation(this.bikeBodyRb.transform.up, hit.normal) * this.bikeBodyRb.transform.rotation).eulerAngles.x;
        float zRot = 0;
        /*if (this.curVelocityOffset > 0)
            zRot = -this.zTiltAngle * this.steerSpeed * this.curVelocityOffset;*/
        zRot = -this.zTiltAngle * this.steerSpeed;

        Quaternion targetRot = Quaternion.Slerp(this.bikeBodyRb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), this.tiltIncrement);
        Quaternion newRot = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        this.bikeBodyRb.MoveRotation(newRot);
    }
}
