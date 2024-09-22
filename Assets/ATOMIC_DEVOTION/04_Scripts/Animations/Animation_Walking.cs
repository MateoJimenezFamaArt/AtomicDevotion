using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Walking : MonoBehaviour, I_Animation
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(Animator Animator)
    {
        Animator.SetBool("IsRunning", false);
        Animator.SetBool("IsWalking", true);   
    }
}
