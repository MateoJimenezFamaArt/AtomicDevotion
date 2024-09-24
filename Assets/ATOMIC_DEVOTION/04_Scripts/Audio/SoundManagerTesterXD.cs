using UnityEngine;

public class TestAudioPlayer : MonoBehaviour
{
    private void Update()
    {
        // Check for key press 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlaySound("Clip1");
        }
        
        // Check for key press 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlaySound("Clip2");
        }
    }
}
