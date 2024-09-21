using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Debug_Edmon : MonoBehaviour
{
    [SerializeField] private TMP_Text DebugText;
    [SerializeField] private Animation_Edmon animation_Edmon;
    
    // Start is called before the first frame update
    void Start()
    {
        animation_Edmon = GetComponent<Animation_Edmon>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
