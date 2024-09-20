using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Movement_Mutant1 : MonoBehaviour
{
    [SerializeField] private Rigidbody RB_Mutant1;
    [SerializeField] private float Speed_Mutant1;

    [SerializeField] private float ChaseSpeed_Mutant1 = 2.0f;

    [SerializeField] private GameObject[] PatrolPoints;
    [SerializeField] private Vector3 Target;
    [SerializeField] private int CurrentTarget;

    // DEBUG TEMP


    // Start is called before the first frame update
    void Start()
    {
        Speed_Mutant1 = 2.0f;
        RB_Mutant1 = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        PatrolMutant();
    }

    void PatrolMutant()
    {
            if (CurrentTarget >= PatrolPoints.Length)
            {
                CurrentTarget = 0;
            }
            
            Target = PatrolPoints[CurrentTarget].transform.position;
            //RB_Mutant1.MoveTowards(transform.position, Target);
            RB_Mutant1.MovePosition(Vector3.MoveTowards(transform.position, Target, 1 * Speed_Mutant1 * ChaseSpeed_Mutant1 * Time.deltaTime));
            

            if (Vector3.Distance(transform.position, Target) < 3.0f)
            {
                CurrentTarget++;
            }
    }

    public int GetCurrentTarget()
    {
        return CurrentTarget;
    }

}
