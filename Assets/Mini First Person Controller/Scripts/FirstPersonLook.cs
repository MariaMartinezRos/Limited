using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    // Reference to the Flashlight GameObject
    // public GameObject flashlight;

    // // Reference to the Light component (Spotlight) inside the Flashlight GameObject
    // private Light spotlight;



    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;

        // Ensure the flashlight GameObject is assigned
        // if (flashlight == null)
        // {
        //     Debug.LogError("Flashlight GameObject is not assigned in the Inspector!");
        //     return;
        // }

        // // Get the Light component (Spotlight) from the Flashlight GameObject
        // spotlight = flashlight.GetComponent<Light>();
        // if (spotlight == null)
        // {
        //     Debug.LogError("No Light component found on the Flashlight GameObject!");
        //     return;
        // }

        // // Ensure the flashlight is off at the start
        // spotlight.enabled = false;
        // Debug.Log("Flashlight is initially turned off.");
    }

    void Update()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);



        // Toggle flashlight on/off when "E" is pressed
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     if (spotlight != null)
        //     {
        //         spotlight.enabled = !spotlight.enabled;
        //         Debug.Log("Flashlight toggled: " + (spotlight.enabled ? "ON" : "OFF"));
        //     }
        //     else
        //     {
        //         Debug.LogError("Spotlight component is missing!");
        //     }
        // }

    }
}
