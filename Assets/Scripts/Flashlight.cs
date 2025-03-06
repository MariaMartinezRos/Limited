using System.Collections;
using UnityEngine;
using TMPro; // Importing TextMeshPro namespace

public class Flashlight : MonoBehaviour
{
    public Light spotlight;

    // SOUND
    public AudioSource loopSound;  // AudioSource for the looping sound
    public AudioClip loopClip;     // Sound to loop when flashlight is on

    // Timer
    private float timer = 300.0f;  // Start with 5 minutes (300 seconds)
    private bool canUseFlashlight = true;

    // UI TextMeshPro Text
    public TextMeshProUGUI timerText; // Reference to the UI TextMeshPro Text component

    // Start is called before the first frame update
    void Start()
    {
        spotlight.enabled = false;  // Start with flashlight off
        loopSound.loop = true;      // Set looping sound
        UpdateTimerText();          // Initially update the timer UI
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseFlashlight)
        {
            if (Input.GetButtonDown("Flashlight"))
            {
                if (spotlight.enabled)
                {
                    TurnOffFlashlight();
                }
                else
                {
                    TurnOnFlashlight();
                }
            }
        }

        // If the flashlight is on, start the countdown
        if (spotlight.enabled)
        {
            timer -= Time.deltaTime;  // Decrease the timer

            // If the timer runs out, turn off the flashlight and prevent further use
            if (timer <= 0)
            {
                timer = 0;
                TurnOffFlashlight();
                canUseFlashlight = false;  // Prevent the flashlight from being turned on again
            }
        }

        // Update the timer text every frame
        UpdateTimerText();
    }

    // Function to turn on the flashlight
    void TurnOnFlashlight()
    {
        spotlight.enabled = true;
        loopSound.clip = loopClip;  // Set the looping sound clip
        loopSound.Play();           // Play the loop sound
    }

    // Function to turn off the flashlight
    void TurnOffFlashlight()
    {
        spotlight.enabled = false;
        loopSound.Stop();           // Stop the loop sound
    }

    // Method to get remaining time
    public float GetRemainingTime()
    {
        return timer;
    }

    // Method to update the timer text UI (formatted as MM:SS)
    void UpdateTimerText()
    {
        // Make sure timerText is not null
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60); // Get the minutes
            int seconds = Mathf.FloorToInt(timer % 60); // Get the seconds

            // Format the time as MM:SS (e.g., 05:00)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}



// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;


// public class Flashlight : MonoBehaviour
// {
//     public Light spotlight;

//     // SOUND
//     public AudioSource loopSound;  // AudioSource for the looping sound
//     public AudioClip loopClip;     // Sound to loop when flashlight is on

//     // Timer
//     private float timer = 60.0f;
//     private bool canUseFlashlight = true;

//     // UI Text
//     public TextMeshProUGUI timerText; // Reference to the UI Text component

//     // Start is called before the first frame update
//     void Start()
//     {
//         spotlight.enabled = false;  // Start with flashlight off
//         loopSound.loop = true;      // Set looping sound
//         UpdateTimerText();          // Initially update the timer UI
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (canUseFlashlight)
//         {
//             if (Input.GetButtonDown("Flashlight"))
//             {
//                 if (spotlight.enabled)
//                 {
//                     TurnOffFlashlight();
//                 }
//                 else
//                 {
//                     TurnOnFlashlight();
//                 }
//             }
//         }

//         // If the flashlight is on, start the countdown
//         if (spotlight.enabled)
//         {
//             timer -= Time.deltaTime;  // Decrease the timer

//             // If the timer runs out, turn off the flashlight and prevent further use
//             if (timer <= 0)
//             {
//                 timer = 0;
//                 TurnOffFlashlight();
//                 canUseFlashlight = false;  // Prevent the flashlight from being turned on again
//             }
//         }

//         // Update the timer text every frame
//         UpdateTimerText();
//     }

//     // Function to turn on the flashlight
//     void TurnOnFlashlight()
//     {
//         spotlight.enabled = true;
//         loopSound.clip = loopClip;  // Set the looping sound clip
//         loopSound.Play();           // Play the loop sound
//     }

//     // Function to turn off the flashlight
//     void TurnOffFlashlight()
//     {
//         spotlight.enabled = false;
//         loopSound.Stop();           // Stop the loop sound
//     }

//     // Method to get remaining time
//     public float GetRemainingTime()
//     {
//         return timer;
//     }

//     // Method to update the timer text UI
//     void UpdateTimerText()
//     {
//         // Make sure timerText is not null
//         if (timerText != null)
//         {
//             timerText.text = "00 : " + Mathf.Ceil(timer).ToString(); // Update the text with the remaining time
//         }
//     }
// }







// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class Flashlight : MonoBehaviour
// {
//     public Light spotlight;

//     //SOUND
//     // public AudioSource clickSound; // AudioSource for the click sound
//     public AudioSource loopSound;  // AudioSource for the looping sound
//     // public AudioClip clickClip;    // Sound for both on and off clicks
//     public AudioClip loopClip;     // Sound to loop when flashlight is on


//     // Timer
//     private float timer = 60.0f;
//     private bool canUseFlashlight = true;
//     public Text timerText;



//     // Start is called before the first frame update
//     void Start()
//     {
//         spotlight.enabled = false;
//         loopSound.loop = true;
//         UpdateTimerText();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (canUseFlashlight)
//         {
//             if(Input.GetButtonDown("Flashlight"))
//             {
//                 if(spotlight.enabled)
//                 {
//                     spotlight.enabled = false;

//                     // clickSound.PlayOneShot(clickClip); // Play the click sound
//                     loopSound.Stop();                  // Stop the loop sound
//                 }
//                 else
//                 {
//                     spotlight.enabled = true;

//                     // clickSound.PlayOneShot(clickClip); // Play the click sound
//                     loopSound.clip = loopClip;         // Set the loop sound clip
//                     loopSound.Play();                  // Start playing the loop sound

//                 }
//             }
//             // If the flashlight is on, start the countdown
//             if (spotlight.enabled)
//             {
//                 timer -= Time.deltaTime;  // Decrease the timer

//                 // If the timer runs out, turn off the flashlight and prevent further use
//                 if (timer <= 0)
//                 {
//                     timer = 0;
//                     TurnOffFlashlight();
//                     canUseFlashlight = false;  // Prevent the flashlight from being turned on again
//                 }
//             }
//             UpdateTimerText();

//         }
//     }
//     public float GetRemainingTime()
//     {
//         return timer;
//     }
//         // Method to update the timer text UI
//     void UpdateTimerText()
//     {
//         // Make sure timerText is not null
//         if (timerText != null)
//         {
//             timerText.text = "Time Remaining: " + Mathf.Ceil(timer).ToString() + "s"; // Update the text with the remaining time
//         }
//     }
// }
