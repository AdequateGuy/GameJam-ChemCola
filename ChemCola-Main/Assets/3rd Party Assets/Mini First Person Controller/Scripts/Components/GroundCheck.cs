using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = .15f;
    public Vector2 disFromPlat;
    public bool edgeForgive;

    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;
    /// <summary>
    /// Called when the ground is touched again.
    /// </summary>
    public event System.Action Grounded;

    [SerializeField]
    FirstPersonMovement firstPersonMovement;

    private Rigidbody rb;
    

    const float OriginOffset = .001f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;
    public bool isGroundedNow;
    public bool edg;

    public float slipSpeed;
    public Vector2 m_wallCheck;

    private void Start()
    {
        rb = firstPersonMovement.rb;
    }

    void LateUpdate()
    {
        Vector3 forward = transform.forward * disFromPlat.x;
        Vector3 back = -transform.forward * disFromPlat.x;
        Vector3 right = transform.right * disFromPlat.x;
        Vector3 left = -transform.right * disFromPlat.x;

        Ray frontRay = new Ray(RaycastOrigin, forward);
        Ray backRay = new Ray(RaycastOrigin, back);
        Ray rightRay = new Ray(RaycastOrigin, right);
        Ray leftRay = new Ray(RaycastOrigin, left);

        float range = disFromPlat.x;
        if (Physics.Raycast(backRay,range))
        {
            edgeForgive = true;
        }
        else
        {
            edgeForgive = false;
        }
        // Check if we are grounded now.
        isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

        // Call event if we were in the air and we are now touching the ground.
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;

        if (isGrounded)
        {
            edg = false;
        }
        else
        {
            if (rb.velocity.y < 0) edg = SlipCheckers(); else edg = false;
        }

        
    }

    private bool SlipCheckers()
    {
        Vector3 ray_spawn_pos = transform.position + Vector3.up * m_wallCheck.y;

        Vector3 forward = transform.forward * m_wallCheck.x;
        Vector3 back = -transform.forward * m_wallCheck.x;
        Vector3 right = transform.right * m_wallCheck.x; 
        Vector3 left = -transform.right * m_wallCheck.x;

        Ray front_ray = new Ray(ray_spawn_pos, forward);
        Ray back_ray = new Ray(ray_spawn_pos, back);
        Ray right_ray = new Ray(ray_spawn_pos, right);
        Ray left_ray = new Ray(ray_spawn_pos, left);

        float dis = m_wallCheck.x;

        RaycastHit hit;
        if (Physics.Raycast(front_ray, out hit, dis))
        {
            HitForSlip(transform.forward);
            return true;
        }
        if (Physics.Raycast (back_ray, out hit, dis) || Physics.Raycast (left_ray, out hit, dis) || Physics.Raycast(right_ray, out hit, dis)) 
        {
            HitForSlip(hit.normal);
            return true; 
        }
        return false;
    }

    void HitForSlip(Vector3 slip_direction) 
    {
        rb.AddForce(((slip_direction * slipSpeed) + Vector3.up) * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Vector3 ray_spawn_pos = transform.position + Vector3.up * m_wallCheck.y;

        Vector3 forward = transform.forward * m_wallCheck.x;
        Vector3 back = -transform.forward * m_wallCheck.x;
        Vector3 right = transform.right * m_wallCheck.x;
        Vector3 left = -transform.right * m_wallCheck.x;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray_spawn_pos, forward);
        Gizmos.DrawRay(ray_spawn_pos, back);
        Gizmos.DrawRay(ray_spawn_pos, right);
        Gizmos.DrawRay(ray_spawn_pos, left);

        // Draw a line in the Editor to show whether we are touching the ground.
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
    }
}
