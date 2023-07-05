using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityJumpBuff : MonoBehaviour
{
    [SerializeField]
    private float Gravity = 20;
    [SerializeField]
    private float powerupDuration = 10;
    [SerializeField]
    private GameObject artToDisable = null;
    private Collider _collider;

    public float jumpMass;
    public float regularMass;
    public float jumpDrag;
    public float regularDrag;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Jump jump = other.gameObject.GetComponent<Jump>();
        if (jump != null)
        {
            //powerup 
            StartCoroutine(GravityJump(jump));
        }

    }
    public IEnumerator GravityJump(Jump jump)
    {
        //soft disable
        _collider.enabled = false;
        artToDisable.SetActive(false);
        
        //activate 
        var jumpRigidbody =jump.GetComponent<Rigidbody>();      
        
        //set jump mass and drag for duration of powerup
        if (jumpRigidbody != null)
        {
            ApplyGravity(jumpRigidbody, this.jumpMass, this.jumpDrag);
        }
        ActivatePowerup(jump);
        Debug.Log("GravityJumpActivated");
        
        //wait for some amount of time
        yield return new WaitForSeconds(powerupDuration);
        
        //deactivate powerup
        DeactivatePowerup(jump);
        if (jumpRigidbody != null)
        {
            ApplyGravity(jumpRigidbody, this.regularMass, this.regularDrag);
        }
        _collider.enabled = true;
        artToDisable.SetActive(true);
    }

    private void ActivatePowerup(Jump jump)
    {
        jump.SetGravityJump(Gravity);
    }

    private void DeactivatePowerup(Jump jump)
    {
        jump.SetGravityJump(-Gravity);
    }

    //Applies new values for mass and drag to the player rigidbody
    private void ApplyGravity(Rigidbody rb, float jumpMass, float jumpDrag)
    {
        rb.mass = jumpMass;
        rb.drag = jumpDrag;
    }
}
