using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SuperSpeedBuff : MonoBehaviour
{
    [SerializeField]
    private float speedIncreaseAmount = 1000;
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
        FirstPersonMovement firstPersonMovement = other.gameObject.GetComponent<FirstPersonMovement>();
        if(firstPersonMovement != null)
        {
            //powerup 
            StartCoroutine(SuperSpeed(firstPersonMovement));
        }

       }
    public IEnumerator SuperSpeed(FirstPersonMovement firstPersonMovement)
    {
        //soft disable
        _collider.enabled = false;
        artToDisable.SetActive(false);
        //activate
        ActivatePowerup(firstPersonMovement);
        Debug.Log("SuperSpeedActivated");
        //wait for some amount of time
        yield return new WaitForSeconds(powerupDuration);
        //deactivate powerup
        DeactivatePowerup(firstPersonMovement);
        _collider.enabled = true;
        artToDisable.SetActive(true);
    }

    private void ActivatePowerup(FirstPersonMovement firstPersonMovement)
    {
        firstPersonMovement.SetMoveSpeed(speedIncreaseAmount);
    }

    private void DeactivatePowerup(FirstPersonMovement firstPersonMovement)
    {
        firstPersonMovement.SetMoveSpeed(-speedIncreaseAmount);
    }

    
}
