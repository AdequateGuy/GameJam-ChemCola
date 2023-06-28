using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRoom : MonoBehaviour
{

    public float rotateSpeed = 20f;

    public Vector3 rotateVector = new Vector3(20f, 0f,0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector *Time.deltaTime * rotateSpeed);
    }
}
