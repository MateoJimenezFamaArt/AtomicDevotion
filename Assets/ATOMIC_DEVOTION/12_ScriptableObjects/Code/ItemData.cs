using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName; // Nombre del objeto
    public GameObject itemPrefab; // Prefab del objeto

    public Sprite ItemLogo; // Logo del Objeto
}
