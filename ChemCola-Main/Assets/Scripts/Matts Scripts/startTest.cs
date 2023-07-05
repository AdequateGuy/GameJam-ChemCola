using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject afterEnter;
    public GameObject audioPlaying;
    public AudioSource audioSource;
    void Start()
    {
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource = GetComponent<AudioSource>();
        audioPlaying = GameObject.Find("aftercolaGrab");
        audioPlaying.GetComponent<AudioSource>().Stop();
        audioPlaying = GameObject.Find("aftermoneyGrab");
        audioPlaying.GetComponent<AudioSource>().Stop();
        audioPlaying = GameObject.Find("Dialogue_Speaker");
        audioPlaying.GetComponent<AudioSource>().Stop();
        //nowplay
        afterEnter = GameObject.Find("Doorway");
        afterEnter.GetComponent<AudioSource>().Play();

        

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
