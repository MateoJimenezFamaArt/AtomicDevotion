using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Animation_Edmon : MonoBehaviour
{   

    [SerializeField] private Animator Edmon_Animator;
    [SerializeField] private float HorizontalMove;
    [SerializeField] private float VerticalMove;
    [SerializeField] private AnimationsManager animationsManager;

    [SerializeField] AnimatorStates animatorStates = AnimatorStates.Idle;

    private Coroutine InteractingCoroutine;
    private bool CoroutineActive = false;

    private FirstPersonController edmon_Controller;
    


    [SerializeField] private enum AnimatorStates
    {
        Walking,
        Running,
        Idle,
        Interacting
    };

    // Start is called before the first frame update
    void Start()   
    {
        animationsManager = GameObject.FindWithTag("AnimationsManager").GetComponent<AnimationsManager>();
        Edmon_Animator = GetComponent<Animator>();
        edmon_Controller = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {        
        SetAnimationState();
       
    }

    private void SetAnimationState()
    {

        switch(animatorStates)
        {
            case AnimatorStates.Walking:
            {
                animationsManager.ChangeState("Walking", Edmon_Animator);
                break;              
            }
            case AnimatorStates.Running:
            {
                animationsManager.ChangeState("Running", Edmon_Animator);
                break;              
            }

            case AnimatorStates.Idle:
            {
                animationsManager.ChangeState("Idle", Edmon_Animator);
                break;              
            }
               case AnimatorStates.Interacting:
            {
                if(!CoroutineActive)
                {
                    CoroutineActive = true;
                    InteractingCoroutine = StartCoroutine(Interacting());
                    
                }
                
                break;              
            }
        }
        
    }

    public IEnumerator Interacting()
    {
        Debug.Log("Empezamos interaccion");
        edmon_Controller.SetInteract(true); // Desactiva el movimiento
        animationsManager.ChangeState("Interacting", Edmon_Animator);
        //edmon_Controller.interactionZone.enabled = true;
        yield return new WaitForSeconds(2.5f); // Espera a que termine la animación
        edmon_Controller.SetInteract(false); // Vuelve a activar el movimiento
        //edmon_Controller.interactionZone.enabled = false;
        animatorStates = AnimatorStates.Idle; // Cambia el estado a Idle
        CoroutineActive = false;
        StopCoroutine(InteractingCoroutine);
    }


    public void ChangeCurrentStateAccessMethod(string newState)
    {   
        AnimatorStates State;
        if(Enum.TryParse<AnimatorStates>(newState, out State))
        {
            animatorStates = State;
        }
        
    }

    //ACCESS METHODS
    public string GetCurrentState()
    {
        return animatorStates.ToString();
    }


}
