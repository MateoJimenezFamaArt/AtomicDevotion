using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animation_Edmon : MonoBehaviour
{   

    [SerializeField] private Animator Edmon_Animator;
    [SerializeField] private float HorizontalMove;
    [SerializeField] private float VerticalMove;

    [SerializeField] AnimatorStates animatorStates = AnimatorStates.Idle;
    

    [SerializeField] private enum AnimatorStates
    {
        Walking,
        Running,
        Idle
    };

    // Start is called before the first frame update
    void Start()   
    {
        Edmon_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        SetAnimationState();
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        if (HorizontalMove !=0 || VerticalMove != 0)
        {            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ChangeCurrentStateAccessMethod(AnimatorStates.Running);
            }
            else
            {
                ChangeCurrentStateAccessMethod(AnimatorStates.Walking);
            }
        }
        else if (HorizontalMove == 0 || VerticalMove == 0)
        {
            ChangeCurrentStateAccessMethod(AnimatorStates.Idle);
        }
    }

    private void SetAnimationState()
    {

        switch(animatorStates)
        {
            case AnimatorStates.Walking:
            {
                Edmon_Animator.SetBool("IsRunning", false);
                Edmon_Animator.SetBool("IsWalking", true);   
                break;              
            }
            case AnimatorStates.Running:
            {
                Edmon_Animator.SetBool("IsRunning", true);
                Edmon_Animator.SetBool("IsWalking", false);  
                break;              
            }

            case AnimatorStates.Idle:
            {
                Edmon_Animator.SetBool("IsRunning", false);
                Edmon_Animator.SetBool("IsWalking", false);  
                break;              
            }
        }
        
    }

    private void ChangeCurrentStateAccessMethod(AnimatorStates newState)
    {   
        animatorStates = newState;
    }

    //ACCESS METHODS
    public string GetCurrentState()
    {
        return animatorStates.ToString();
    }


}
