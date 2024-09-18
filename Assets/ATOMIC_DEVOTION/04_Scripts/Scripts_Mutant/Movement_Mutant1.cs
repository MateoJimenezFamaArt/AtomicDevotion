using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Mutant1 : MonoBehaviour
{
    [SerializeField] private Rigidbody RB_Mutant1;
    [SerializeField] private float Speed_Mutant1;

    [SerializeField] private static float ChaseSpeed_Mutant1 = 2.0f;



    // Start is called before the first frame update
    void Start()
    {
        RB_Mutant1 = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PatrolMutant();
    }

    void PatrolMutant()
    {
       
    }
}
