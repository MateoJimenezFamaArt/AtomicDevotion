using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debug_Mutant : MonoBehaviour
{
    [SerializeField] private TMP_Text DebugText;
    [SerializeField] private Movement_Mutant1 movement_Mutant1;
    // Start is called before the first frame update
    void Start()
    {
        movement_Mutant1 = GetComponent<Movement_Mutant1>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDebugValue();
    }

    void GetDebugValue()
    {
        DebugText.text = $"Current_Target:{movement_Mutant1.GetCurrentTarget()}";
    }
}
