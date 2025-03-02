using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;
    // Start is called before the first frame update
    void Start()
    {
        spotlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Flashlight"))
        {
            if(spotlight.enabled)
            {
                spotlight.enabled = false;
            }
            else
            {
                spotlight.enabled = true;
            }
        }
    }
}
