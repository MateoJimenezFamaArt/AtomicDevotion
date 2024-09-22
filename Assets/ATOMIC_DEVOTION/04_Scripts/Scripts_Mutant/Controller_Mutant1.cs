using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Controller_Mutant1 : MonoBehaviour
{
    [SerializeField] private Rigidbody RB_Mutant1;
    [SerializeField] private float Speed_Mutant1;

    [SerializeField] private float ChaseSpeed_Mutant1 = 2.0f;

    [SerializeField] private GameObject[] PatrolPoints;
    [SerializeField] private Vector3 Target;
    [SerializeField] private int CurrentTarget;
    [SerializeField] private GameObject Edmon_Player;

    private EnemyStates EnemyState = EnemyStates.Patroling;

    private Animation_Mutant1 animation_Mutant1;
    // Start is called before the first frame update
    private enum EnemyStates
    {
        Patroling,
        Chasing
    }


    void Start()
    {
        Speed_Mutant1 = 2.0f;
        RB_Mutant1 = GetComponent<Rigidbody>();
        Edmon_Player = GameObject.FindWithTag("Player");
        animation_Mutant1 = GetComponent<Animation_Mutant1>();

    }

    void FixedUpdate()
    {
        PatrolMutant();
    }


    private void PatrolMutant()
    {
        switch (EnemyState)
        {
            case EnemyStates.Patroling:
            {
               if (CurrentTarget >= PatrolPoints.Length)
                {
                    CurrentTarget = 0;
                }
            
                Target = PatrolPoints[CurrentTarget].transform.position;
                MovementMutant(Target);
                animation_Mutant1.ChangeCurrentStateAccessMethod("Walking");
                
                

                break;
            }

            case EnemyStates.Chasing:
            {
                if (CurrentTarget >= PatrolPoints.Length)
                {
                    CurrentTarget = 0;
                }
                Target = Edmon_Player.transform.position;
                MovementMutant(Target);

                animation_Mutant1.ChangeCurrentStateAccessMethod("Running");

                break;
            }
        }
        
    }

    private void MovementMutant(Vector3 target)
    {
        RB_Mutant1.MovePosition(Vector3.MoveTowards(transform.position, Target, 1 * Speed_Mutant1 * ChaseSpeed_Mutant1 * Time.deltaTime));
                

                if (Vector3.Distance(transform.position, Target) < 3.0f)
                {
                    CurrentTarget++;
                }
    }

    // Metodos de Acceso

    public int GetCurrentTarget()
    {
        return CurrentTarget;
    }

    public string GetCurrentState()
    {
        return EnemyState.ToString();
    }

    public void ChangeCurrentStateAccessMethod(string State)
    {   
        if(System.Enum.TryParse(State, out EnemyStates newState))
        {
            EnemyState = newState;
        }
  
    }



    

}
