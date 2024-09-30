using System.Collections.Generic;
using System.Linq; // Import LINQ for efficient UI updates
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Header("Inventory Settings")]
    [SerializeField] private int maxInventorySlots = 4;
    [SerializeField] private List<ItemData> playerInventory;

    [Header("Player Hand")]
    [SerializeField] private Transform handPosition;

    private bool isInventoryOpen = false;

    private void Start()
    {
        playerInventory = new List<ItemData>();  // Initialize player inventory
        inventoryUI.SetActive(false);           // Ensure inventory UI is closed
    }

    private void Update()
    {
        HandleInventoryToggle();
    }

    /// <summary>
    /// Handles opening and closing of the inventory UI, including pausing the game.
    /// </summary>
    private void HandleInventoryToggle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    /// <summary>
    /// Toggles the inventory UI and pauses/unpauses the game.
    /// </summary>
    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Time.timeScale = 0; // Pause the game when the inventory is open
            RefreshInventoryUI();
            EnableMouseControl(); // Enable mouse control when inventory is open
        }
        else
        {
            Time.timeScale = 1; // Unpause the game when the inventory is closed
            DisableMouseControl(); // Disable mouse control when inventory is closed
        }
    }
        /// <summary>
    /// Enables the player's mouse control by unlocking the cursor and making it visible.
    /// </summary>
    private void EnableMouseControl()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    /// <summary>
    /// Disables the player's mouse control by locking the cursor to the center and hiding it.
    /// </summary>
    private void DisableMouseControl()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    /// <summary>
    /// Refreshes the inventory UI using LINQ for more efficient updates.
    /// </summary>
    private void RefreshInventoryUI()
    {
        // Clear existing UI items
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }

        // Efficiently create inventory slots using LINQ
        playerInventory
            .Take(maxInventorySlots) // Limit to the max slots available
            .ToList()
            .ForEach(item =>
            {
                GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryGrid);

                // Set the item name in the UI
                TextMeshProUGUI itemText = newSlot.GetComponentInChildren<TextMeshProUGUI>();
                if (itemText != null)
                {
                    itemText.text = item.itemName;
                }

                // Set the item logo sprite in the UI
                Image itemLogoImage = newSlot.GetComponentInChildren<Image>(); // Assuming Image is a child of newSlot
                 if (itemLogoImage != null)
                {
                itemLogoImage.sprite = item.ItemLogo; // Set the sprite from ItemData
                itemLogoImage.enabled = item.ItemLogo != null; // Enable the image if a logo is assigned
                 }
                else
                {
                Debug.LogError("No Image component found in the inventory slot prefab.");
                }

                // Set up the button click event to instantiate item in hand
                Button slotButton = newSlot.GetComponent<Button>();
                if (slotButton != null)
                {
                    slotButton.onClick.AddListener(() => InstantiateItemInHand(item));
                }
                else
                {
                    Debug.LogError("No Button component found in the inventory slot prefab.");
                }
            });
    }

    /// <summary>
    /// Adds a new item to the player's inventory.
    /// </summary>
    /// <param name="newItem">The item to add.</param>
    public void AddItemToInventory(ItemData newItem)
    {
        if (playerInventory.Count < maxInventorySlots)
        {
            playerInventory.Add(newItem);
            Debug.Log("Added " + newItem.itemName + " to inventory.");
            if (isInventoryOpen)
            {
                RefreshInventoryUI();  // Update the UI only if inventory is open
            }
        }
        else
        {
            Debug.LogWarning("Inventory is full! Cannot add more items.");
        }
    }

    /// <summary>
    /// Instantiates the selected item in the player's hand.
    /// </summary>
    /// <param name="itemData">The item to instantiate.</param>
private void InstantiateItemInHand(ItemData itemData)
{
    // Clear any previously instantiated items from the hand
    ClearHandItems();

    // Check if the itemData has a prefab assigned
    if (itemData != null && itemData.itemPrefab != null)
    {
        GameObject newItem = Instantiate(itemData.itemPrefab, handPosition.position, handPosition.rotation);
        newItem.transform.SetParent(handPosition); // Parent the instantiated item to the hand
        Debug.Log("Instantiated item: " + itemData.itemName + " in hand.");
    }
    else
    {
        Debug.LogWarning("Cannot instantiate item. ItemData is null or prefab is not assigned for " + (itemData != null ? itemData.itemName : "null itemData"));
    }
}

    /// <summary>
    /// Clears any existing items from the player's hand.
    /// </summary>
    private void ClearHandItems()
    {
        foreach (Transform child in handPosition)
        {
            Destroy(child.gameObject);
        }
    }

    public bool IsInventoryOpen()
    {   
        return isInventoryOpen;
    }

}
