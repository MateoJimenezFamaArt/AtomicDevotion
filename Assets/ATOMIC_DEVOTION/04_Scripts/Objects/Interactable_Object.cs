using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, I_interactable
{
    private Animation_Edmon animation_Edmon;
    private bool Collision;
    // Start is called before the first frame update
    void Start()
    {
        animation_Edmon = GameObject.FindWithTag("Player").GetComponent<Animation_Edmon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "Player" ))
        {
            if(!Collision)
            {
                Collision = true;

                Interact();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {   
        if (other.CompareTag( "Player" ))
        {
            Collision = false;
        }

    }

    public void Interact()
    {
        animation_Edmon.ChangeCurrentStateAccessMethod("Interacting");
    }

    public bool GetCollisionDebug()
    {
        return Collision;
    }
}
