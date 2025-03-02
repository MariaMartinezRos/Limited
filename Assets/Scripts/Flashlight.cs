using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light spotlight;


    //SOUND
    // public AudioSource clickSound; // AudioSource for the click sound
    public AudioSource loopSound;  // AudioSource for the looping sound
    // public AudioClip clickClip;    // Sound for both on and off clicks
    public AudioClip loopClip;     // Sound to loop when flashlight is on



    // Start is called before the first frame update
    void Start()
    {
        spotlight.enabled = false;
        loopSound.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Flashlight"))
        {
            if(spotlight.enabled)
            {
                spotlight.enabled = false;

                // clickSound.PlayOneShot(clickClip); // Play the click sound
                loopSound.Stop();                  // Stop the loop sound
            }
            else
            {
                spotlight.enabled = true;

                // clickSound.PlayOneShot(clickClip); // Play the click sound
                loopSound.clip = loopClip;         // Set the loop sound clip
                loopSound.Play();                  // Start playing the loop sound

            }
        }
    }
}
