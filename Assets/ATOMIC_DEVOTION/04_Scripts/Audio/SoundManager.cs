using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public class AudioClipInfo
    {
        public string clipName;
        public AudioClip audioClip;
    }

    public List<AudioClipInfo> audioClips = new List<AudioClipInfo>();
    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Populate the dictionary with audio clips
        foreach (var audioClipInfo in audioClips)
        {
            audioClipDictionary[audioClipInfo.clipName] = audioClipInfo.audioClip;
        }
    }

    public void PlaySound(string clipName)
    {
        if (audioClipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }
}
