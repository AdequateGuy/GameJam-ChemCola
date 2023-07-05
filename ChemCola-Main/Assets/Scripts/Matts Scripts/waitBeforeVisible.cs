using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitBeforeVisible : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int _numberOfSeconds = 21;
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(ExampleCoroutine());

        

    }

    // Update is called once per frame

    
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 2 seconds.

        yield return new WaitForSeconds(_numberOfSeconds);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;


        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    void Update()
    {
        
    }
}
