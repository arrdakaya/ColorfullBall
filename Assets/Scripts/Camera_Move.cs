using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public int forwardSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Variables.firstTouch == 1)
        {
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        }
        
    }
}
