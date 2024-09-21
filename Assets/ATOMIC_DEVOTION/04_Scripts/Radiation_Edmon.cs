using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation_Edmon : MonoBehaviour
{
    [SerializeField] private float Edmon_RadiationLevel;
    private Health_Edmon health_Edmon;
    private bool CoroutineActive = false;
    [SerializeField] private Coroutine radiationCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Edmon_RadiationLevel = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator RadiationExposure()
    {
        while(true)
        {
            Edmon_RadiationLevel+=1.0f;
            yield return new WaitForSeconds(1f);
        }
    }

    // Access Methods

    public void RadiationChangeAccess(int i)
    {
        if (i == 1)
        {
            if(!CoroutineActive)
            {
                radiationCoroutine = StartCoroutine(RadiationExposure());
                CoroutineActive = true;
            }
        }
        else if (i == 2)
        {
            if(CoroutineActive)
            {
                StopCoroutine(radiationCoroutine); 
                CoroutineActive = false;
            }
        }
       
    }

    
}
