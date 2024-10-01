using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject playerPrefab; // Prefab del jugador para instanciar
    [SerializeField] private Vector3 respawnPosition; // Posición de respawn del jugador
    [SerializeField] private List<Transform> checkpoints; // Lista de checkpoints en la escena
    [SerializeField] private List<string> achievements; // Logros conseguidos por el jugador
    [SerializeField] private int playerDeaths = 0; // Contador de muertes del jugador
    [SerializeField] private List<string> sceneLog; // Lista de escenas visitadas

    private GameObject playerInstance;
    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); // Kill dupes
        }
    }

    private void Start()
    {
        // Empeiza una lista nueva de escenas recorridas y logros para guardar
        sceneLog = new List<string>();
        achievements = new List<string>();

        
        LogCurrentScene();
    }

    private void LogCurrentScene() // Hace un log de la escena en que estamos
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (!sceneLog.Contains(currentScene))
        {
            sceneLog.Add(currentScene);
        }
    }

    public void AddAchievement(string achievement) // Agrega un achievement a la lista de achievments
    {
        if (!achievements.Contains(achievement))
        {
            achievements.Add(achievement);
        }
    }

    public void SetRespawnPosition(Vector3 position) // Set la position de spawn del Jugador
    {
        respawnPosition = position;
        Debug.Log("Respawn point set to: " + respawnPosition);
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPosition;
    }
public void RespawnPlayer()
{
    // Instantiate player and assign the instance
    playerInstance = Instantiate(playerPrefab, respawnPosition, Quaternion.identity);

    // Find the player's camera
    Transform playerCamera = playerInstance.transform.Find("Camera"); // Adjust the name if needed

    // Now find the Hand object under the Camera
    Transform playerHand = playerCamera?.Find("Hand"); // Adjust the name if needed

    if (playerHand == null)
    {
        Debug.LogWarning("Hand object not found under Camera in the player prefab.");
        return;
    }

    // Get reference to the InventoryManager and assign hand position
    InventoryManager inventoryManager = Object.FindFirstObjectByType<InventoryManager>();
    if (inventoryManager != null)
    {
        inventoryManager.SetHandPosition(playerHand);
        Debug.Log("Hand position assigned to InventoryManager.");
    }
    else
    {
        Debug.LogWarning("InventoryManager not found.");
    }
}


    public void AddCheckpoint(Transform checkpoint) // Agrega un transform a la lista de checkpoints
    {
        if (!checkpoints.Contains(checkpoint))
        {
            checkpoints.Add(checkpoint);
        }
    }

    public void PlayerDied() // Tally cuando el player muere, publico para interactuar con script de vida
    {
        playerDeaths++;
    }

    public int GetPlayerDeaths() // Recoger cuantas veces ha muerto el player
    {
        return playerDeaths;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Se ejecuta al cargar escena para comenzar a logear la escena y spawnear el character
    {
        LogCurrentScene();
        RespawnPlayer();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribirse al evento de carga de escena
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desubscribirse al evento de carga de escena
    }

    public GameObject GetPlayerInstance()
    {
    return playerPrefab; // playerInstance should be the variable holding the instantiated player
    }
}
