using UnityEngine;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; // Reference to the Light component
    private bool flashlightOn = false; // To track the flashlight's state
    public float flickerInterval = 2.0f; // Time between flickers
    public float flickerDuration = 0.05f; // Duration of each flicker
    private Coroutine flickerCoroutine;

    void Start()
    {
        if (flashlight != null)
        {
            flashlight.enabled = false; // Ensure the flashlight starts off
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Toggle the flashlight with 'L' key
        {
            ToggleFlashlight();
        }
    }


void ToggleFlashlight()
{
    if (flashlight != null)
    {
        flashlightOn = !flashlightOn; // Toggle the state
        flashlight.enabled = flashlightOn;

        if (flashlightOn)
        {
            SoundManager.Instance.PlaySound("FlashlightOn");
            Debug.Log("Flashlight turned ON");
            // Start flickering when the flashlight is on
            flickerCoroutine = StartCoroutine(FlickerLight());
        }
        else
        {
            SoundManager.Instance.PlaySound("FlashlightOff");
            Debug.Log("Flashlight turned OFF");
            // Stop flickering when the flashlight is off
            if (flickerCoroutine != null)
            {
                StopCoroutine(flickerCoroutine);
            }
        }
    }
}


    private IEnumerator FlickerLight()
    {
        while (flashlightOn)
        {
            yield return new WaitForSeconds(Random.Range(flickerInterval - 1.0f, flickerInterval + 1.0f));

            // Flicker the light on and off for a brief duration
            flashlight.enabled = false;
            yield return new WaitForSeconds(flickerDuration);
            flashlight.enabled = true;
        }
    }
}
