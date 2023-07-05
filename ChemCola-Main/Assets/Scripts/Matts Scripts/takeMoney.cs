using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class takeMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject afterGrab;
    public GameObject audioPlaying;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && this != null)
        {
            //first test
            GetComponentInParent<AudioSource>().Play();
            audioPlaying = GameObject.Find("aftercolaGrab");
            audioPlaying.GetComponent<AudioSource>().Stop();
            audioPlaying = GameObject.Find("aftermoneyGrab");
            audioPlaying.GetComponent<AudioSource>().Stop();
            audioPlaying = GameObject.Find("Dialogue_Speaker");
            audioPlaying.GetComponent<AudioSource>().Stop();
            afterGrab = GameObject.Find("aftermoneyGrab");
            afterGrab.GetComponent<AudioSource>().Play();           
            Destroy(gameObject);
            //second test - animation

        }
    }
}
