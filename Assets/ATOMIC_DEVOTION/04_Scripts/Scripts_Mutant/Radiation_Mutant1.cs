using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation_Mutant1 : MonoBehaviour
{
    [SerializeField] private float Radiation_Level;
    [SerializeField] private SphereCollider Radiation_Zone;
    [SerializeField] private bool Collision;
    // Start is called before the first frame update
    void Start()
    {
        Radiation_Level = 3.0f;
        Radiation_Zone = GetComponent<SphereCollider>();

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
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player") )
        {
            Collision = false;
        }
    }

    public bool GetCollision()
    {
        return Collision;
    }
    
}
