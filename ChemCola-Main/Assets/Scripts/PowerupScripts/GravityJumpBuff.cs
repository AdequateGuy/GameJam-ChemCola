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
        ActivatePowerup(jump);
        Debug.Log("GravityJumpActivated");
        //wait for some amount of time
        yield return new WaitForSeconds(powerupDuration);
        //deactivate powerup
        DeactivatePowerup(jump);
        Destroy(gameObject);
    }

    private void ActivatePowerup(Jump jump)
    {
        jump.SetGravityJump(Gravity);
    }

    private void DeactivatePowerup(Jump jump)
    {
        jump.SetGravityJump(-Gravity);
    }

    private void ApplyGravity()
    {
      
    }
}
