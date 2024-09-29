using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Handler : MonoBehaviour
{
    [SerializeField]private bool Interacting; /*el valor de esta variable provendra de la clase FirstPersonController, que gestiona el estado 
    de interacción del jugador y se asigna a través del método SetInteract() en la clase Animation_Edmon.*/

   [SerializeField] private bool CanInteract;
    [SerializeField]private FirstPersonController edmon_Controller;
    [SerializeField]private Animation_Edmon animation_Edmon;
    [SerializeField]private InteractableObject interactableObject;

    void Start()
    {
        Interacting = false;
        edmon_Controller = GetComponent<FirstPersonController>();
        animation_Edmon = GetComponent<Animation_Edmon>();
    }

    void Update()
    {
        Interacting = edmon_Controller.GetInteract(); // Aqui se actualiza el estado de Interacting desde el controlador.

        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (!Interacting && CanInteract)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("Empezamos interaccion");
                interactableObject.Interact(); 
                animation_Edmon.ChangeCurrentStateAccessMethod("Interacting");   
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {              
            CanInteract = true;
            interactableObject = other.GetComponent<InteractableObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Interactable"))
        {
            CanInteract = false;
        } 
    }
}
