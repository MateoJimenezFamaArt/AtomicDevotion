using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI; // UI del inventario asignable desde el editor
    [SerializeField] private Transform handPosition; // Posición de la mano del jugador donde se instanciarán los objetos
    [SerializeField] private int maxInventorySlots = 4; // Máximo número de espacios en el inventario
    [SerializeField] private GameObject inventorySlotPrefab; // Prefab del espacio del inventario para la UI
    [SerializeField] private Transform inventoryGrid; // Contenedor de la cuadrícula en la UI
    [SerializeField] private List<ItemData> playerInventory; // Lista de objetos del inventario del jugador

    private bool isInventoryOpen = false; // Estado de la UI del inventario

    private void Start()
    {
        // Inicializar la lista de inventario y asegurar que la UI esté cerrada
        playerInventory = new List<ItemData>();
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        // Abrir o cerrar el inventario al presionar la tecla "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory() // Metodo para abrir o cerrar la UI
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.gameObject.SetActive(isInventoryOpen);
        
        if (isInventoryOpen)
        {
            RefreshInventoryUI();
        }
    }

    /// <summary>
    /// Refreshes the inventory UI grid based on the items in the player's inventory.
    /// </summary>
private void RefreshInventoryUI()
{
    Debug.Log("Refreshing Inventory UI...");

    // Clear out existing slots
    foreach (Transform child in inventoryGrid)
    {
        Destroy(child.gameObject);
    }

    // Create new slots for each item in the inventory
    foreach (ItemData item in playerInventory)
    {
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryGrid);
        Debug.Log("Created slot for item: " + item.itemName);

        TextMeshProUGUI itemText = newSlot.GetComponentInChildren<TextMeshProUGUI>();
        if (itemText != null)
        {
            itemText.text = item.itemName;
        }
        else
        {
            Debug.LogError("No TextMeshPro component found in the inventory slot prefab.");
        }

        Button slotButton = newSlot.GetComponent<Button>();
        if (slotButton != null)
        {
            slotButton.onClick.AddListener(() => InstantiateItemInHand(item));
        }
        else
        {
            Debug.LogError("No Button component found in the inventory slot prefab.");
        }
    }
}


    /// <summary>
    /// Adds a new item to the player's inventory.
    /// </summary>
    /// <param name="newItem">The item to add.</param>
    public void AddItemToInventory(ItemData newItem)
    {
        playerInventory.Add(newItem);
        Debug.Log("Added " + newItem.itemName + " to inventory.");

        RefreshInventoryUI(); //Refresh the UI each time a new inventory is added to the player
    }

private void InstantiateItemInHand(ItemData itemData)
    {
        // Clear any previously instantiated items from the hand
        foreach (Transform child in handPosition)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the selected item prefab at the hand position
        if (itemData.itemPrefab != null)
        {
            GameObject newItem = Instantiate(itemData.itemPrefab, handPosition.position, handPosition.rotation);
            newItem.transform.SetParent(handPosition); // Parent the instantiated item to the hand
        }
        else
        {
            Debug.LogWarning("No prefab assigned for " + itemData.itemName);
        }
    }
}
