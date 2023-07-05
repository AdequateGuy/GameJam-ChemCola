using UnityEngine;
using System.Collections;

public class SpinY: MonoBehaviour

    //This script is attached to any object you want to spin on the Y axis.
{
	public float speed = 10f;


	void Update ()
	{
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed, Space.Self);
    }
}