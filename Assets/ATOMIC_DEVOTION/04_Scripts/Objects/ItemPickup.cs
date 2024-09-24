using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to the inventory manager
    public ItemData itemToAdd; // The item to add to the inventory

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if player collided
        {
            Debug.Log("Chocke con un item verga");
            inventoryManager.AddItemToInventory(itemToAdd); // Add item to inventory
            Destroy(gameObject); // Destroy the pickup after it's collected
        }
    }
}
