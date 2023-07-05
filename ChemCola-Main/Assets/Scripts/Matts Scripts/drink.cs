using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class drink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    public AudioClip clip;
    public GameObject afterGrabSource;
    public GameObject audioPlaying;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this != null)
        {
            //first stop all audio
            //first test
            GetComponentInParent<AudioSource>().Play();
            audioPlaying = GameObject.Find("aftermoneyGrab");
            audioPlaying.GetComponent<AudioSource>().Stop();
            audioPlaying = GameObject.Find("Dialogue_Speaker");
            audioPlaying.GetComponent<AudioSource>().Stop();
            afterGrabSource = GameObject.Find("aftercolaGrab");
            afterGrabSource.GetComponent<AudioSource>().Play();
            Destroy(gameObject);


            //second test - animation

        }
    }
}
