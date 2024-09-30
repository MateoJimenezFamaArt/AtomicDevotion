using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; // Reference to the Light component
    //public bool isEquipped = false; // Boolean to check if the object is equipped
    private bool flashlightOn = false; // To track the flashlight's state

    void Start()
    {
        if (flashlight != null)
        {
            flashlight.enabled = false; // Make sure the flashlight is off at the start
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Check if equipped and "L" key is pressed
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
        {
            flashlightOn = !flashlightOn; // Toggle the flashlight state
            flashlight.enabled = flashlightOn;
        }
    }
}
