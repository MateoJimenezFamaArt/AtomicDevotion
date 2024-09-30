using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemPickup : InteractableObject
{
    public InventoryManager inventoryManager; // Reference to the inventory manager
    public ItemData itemToAdd; // The item to add to the inventory
    /*[SerializeField] private bool CanInteract;

    void Start()
    {
        CanInteract = false;
    }

    void Update()
    {
      if (CanInteract && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if player collided
        {
            CanInteract = true;

        }
    }*/

    public override void Interact()
    {
        Debug.Log("Chocke con un item verga");
        inventoryManager.AddItemToInventory(itemToAdd); // Add item to inventory
        Destroy(gameObject); // Destroy the pickup after it's collected
    }
}
