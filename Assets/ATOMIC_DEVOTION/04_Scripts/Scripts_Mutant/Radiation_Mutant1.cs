using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation_Mutant1 : MonoBehaviour
{
    [SerializeField] private float Radiation_Level;
    [SerializeField] private SphereCollider Radiation_Zone;
    [SerializeField] private bool Collision;
    private Controller_Mutant1 controller_Mutant1;
    private Radiation_Edmon radiation_Edmon;
    // Start is called before the first frame update
    void Start()
    {
        Radiation_Level = 3.0f;
        Radiation_Zone = GetComponent<SphereCollider>();
        controller_Mutant1 = GetComponent<Controller_Mutant1>();
        radiation_Edmon = GameObject.FindWithTag("Player").GetComponent<Radiation_Edmon>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            Collision = true;
            controller_Mutant1.ChangeCurrentStateAccessMethod("Chasing");
            radiation_Edmon.RadiationChangeAccess(1);  
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player") )
        {
            radiation_Edmon.RadiationChangeAccess(2);
            Collision = false;
            controller_Mutant1.ChangeCurrentStateAccessMethod("Patroling");
        }
    }

    public bool GetCollision()
    {
        return Collision;
    }
    
}
