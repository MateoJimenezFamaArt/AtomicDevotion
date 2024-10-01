using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Transform LocationForSounds;

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

    private void Start()
    {
    GameManager gameManager = Object.FindFirstObjectByType<GameManager>();
    GameObject player = gameManager.GetPlayerInstance();

    if (player != null)
    {
        // Do something with the player reference
        Debug.Log("Player reference obtained: " + player.name);
    }
    else
    {
        Debug.LogWarning("No player instance found!");
    }
        LocationForSounds = player.transform;

    }


    public void PlaySound(string clipName)
    {
        if (audioClipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip,LocationForSounds.transform.position);
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }
}
