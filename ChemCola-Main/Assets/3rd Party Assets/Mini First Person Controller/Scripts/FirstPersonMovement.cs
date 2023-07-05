using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;

    public float speed = 5;
    public float maxWalk;
    public float airDecrease;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public float maxVelocity;
    public float airSpeed;
    public KeyCode runningKey = KeyCode.LeftShift;

    public Rigidbody rb;

    public float walkDeaccelerationVolX;
    public float walkDeaccelerationVolZ;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {


        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        /*if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) & groundCheck.isGroundedNow && !groundCheck.isGrounded)
        {
            rb.velocity = new Vector3(Mathf.SmoothDamp(rb.velocity.x, 0f, ref walkDeaccelerationVolX, 1f), rb.velocity.y, Mathf.SmoothDamp(rb.velocity.z, 0f, ref walkDeaccelerationVolZ, 1f));      
        }*/


        // Get targetVelocity from input.
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 xMovement = transform.forward * vertInput;
        Vector3 yMovement = transform.right * horizInput;
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        
        if (IsRunning)
        {
            if ((rb.velocity.x * rb.velocity.x) + (rb.velocity.z * rb.velocity.z) < maxVelocity)
            {
                rb.AddForce(Vector3.ClampMagnitude(xMovement + yMovement, 1.0f) * targetMovingSpeed, ForceMode.Acceleration);
            }
            /*if (!groundCheck.isGrounded)
            {
                rb.AddForce(Vector3.ClampMagnitude(xMovement + yMovement, 1.0f) * airSpeed * airDecrease, ForceMode.Acceleration);
            }*/
        }     
        else
        {
            if ((rb.velocity.x * rb.velocity.x) + (rb.velocity.z * rb.velocity.z) < maxWalk)
            {
                targetVelocity = targetVelocity.normalized * Time.deltaTime * speed;
                rb.AddRelativeForce(targetVelocity);
            }
            /*if (!groundCheck.isGrounded)
            {
                targetVelocity = targetVelocity.normalized * Time.deltaTime * speed * airDecrease;
                rb.AddRelativeForce(targetVelocity);
            }   */                
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(Mathf.SmoothDamp(rb.velocity.x, 0f, ref walkDeaccelerationVolX, 0.05f), rb.velocity.y, Mathf.SmoothDamp(rb.velocity.z, 0f, ref walkDeaccelerationVolZ, 0.05f));
        }


    }
    public void SetMoveSpeed(float newSpeedAdjustment) 
    { 
        runSpeed += newSpeedAdjustment; 
    }
}