using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


public class SlowTimeBuff : MonoBehaviour
{
    public float slowMotionTimeScale;

    private float startTimeScale;
    private float startFixedDeltaTime;


    [SerializeField]
    private float powerupDuration = 10f;
    [SerializeField]
    private GameObject artToDisable = null;
    private Collider _collider;
    private bool isActive = false;

    private void Awake()
    {
        Debug.Log("Hit");
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            Debug.Log("Hit");
            startTimeScale = Time.timeScale;
            startFixedDeltaTime = Time.fixedDeltaTime;
            _collider.enabled = false;
            artToDisable.SetActive(false);
            //powerup 
            StartCoroutine(SlowMo());
        }

    }
    public IEnumerator SlowMo()
    {
        Debug.Log("Dead");
        //soft disable
        

        //activate
        PowerUpActivated();
        Debug.Log("SlowMoActivated");

        //wait for some amount of time
        yield return new WaitForSeconds(powerupDuration);

        //deactivate powerup
        PowerUpDeactivated();
        StopSlowMotion();
        Destroy(gameObject);       
    }
    public bool PowerUpActivated()
    {
        isActive = true;
        return isActive;
    }
    public bool PowerUpDeactivated()
    {
        isActive = false;
        return isActive;
    }
   

    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) & isActive)
        {
            StartSlowMotion();
        }

        if (Input.GetKeyDown(KeyCode.E) & isActive)
        {
            StopSlowMotion();
        }
    }

}
