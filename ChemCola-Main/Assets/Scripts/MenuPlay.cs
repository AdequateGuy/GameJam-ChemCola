using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour
{
    public void PlayGame()
    {
        //start the animation
        GetComponent<Animator>().enabled = true;
        //co-routine - wait 5 seconds to let animation finish, then next scene
        StartCoroutine(LoadScene());
     
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
