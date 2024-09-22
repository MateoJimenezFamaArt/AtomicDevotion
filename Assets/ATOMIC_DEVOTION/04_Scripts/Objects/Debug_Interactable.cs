using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debug_Interactable : MonoBehaviour
{
    private InteractableObject interactableObject;
    [SerializeField] private TMP_Text DebugText;
    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
       
        DebugText.text = $"Player Interacting: {interactableObject.GetCollisionDebug()}";
    
    }
}
