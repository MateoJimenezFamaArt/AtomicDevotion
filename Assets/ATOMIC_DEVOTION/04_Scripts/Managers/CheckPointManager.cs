using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints; // Array of checkpoints set in the editor
    [SerializeField] private GameManager gameManager; // Reference to the GameManager
    private Transform currentCheckpoint; // The current respawn point

    [SerializeField] private GameObject player; // The player GameObject to respawn

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = Object.FindFirstObjectByType<GameManager>(); // Automatically assign GameManager if not set
        }
        
        // Get the player from the GameManager
        // Find the player instance in the scene at runtime
        player = GameObject.FindWithTag("Player"); // Ensure your player prefab has the tag "Player"
        if (player == null)
        {
            Debug.LogError("Player instance not found!");
        }

        if (checkpoints.Length > 0)
        {
            currentCheckpoint = checkpoints[0]; // Set the first checkpoint as the default respawn point
            gameManager.SetRespawnPosition(currentCheckpoint.position); // Set respawn in GameManager
        }
    }

    private void Update()
    {
        // Test input for respawning at checkpoints
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RespawnAtCheckpoint(0); // Respawn at checkpoint 1
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RespawnAtCheckpoint(1); // Respawn at checkpoint 2
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RespawnAtCheckpoint(2); // Respawn at checkpoint 3
        }
    }

    /// <summary>
    /// Respawns the player at the specified checkpoint index.
    /// </summary>
    /// <param name="checkpointIndex">Index of the checkpoint in the array.</param>
    public void RespawnAtCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length)
        {
            currentCheckpoint = checkpoints[checkpointIndex];
            gameManager.SetRespawnPosition(currentCheckpoint.position);
            RespawnPlayer(player);
        }
        else
        {
            Debug.LogError("Invalid checkpoint index.");
        }
    }

    /// <summary>
    /// Respawn the player at the current checkpoint.
    /// </summary>
    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = currentCheckpoint.position; // Move player to the last checkpoint
        Debug.Log("Player respawned at: " + currentCheckpoint.position);
    }
}
