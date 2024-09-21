using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edmon_Controller : MonoBehaviour
{

    [SerializeField] private Rigidbody Edmon_RigidBody;
    [SerializeField] private float Edmon_Speed;
    [SerializeField] float Edmon_SprintSpeed;
    [SerializeField] float Edmon_TurnSpeed;
    [SerializeField] private float HorizontalMove;
    [SerializeField] private float VerticalMove;

    // Start is called before the first frame update
    void Start()   
    {
        Edmon_RigidBody = GetComponent<Rigidbody>();
        Edmon_Speed = 2.0f;
        Edmon_SprintSpeed = 1.0f;
        Edmon_TurnSpeed = 12.0f;
    }

    // Update is called once per frame
    void Update()
    {        
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        if (HorizontalMove !=0 || VerticalMove != 0)
        {            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharacterRunning();
            }
            else
            {
                CharacterWalking();
            }
        }
    }

    void FixedUpdate()
    {   
        Vector3 Movement = new Vector3(HorizontalMove, 0, VerticalMove);
        
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, Movement, Edmon_TurnSpeed * Time.deltaTime, 0f);

        Quaternion Rotation = Quaternion.LookRotation(desiredForward);

        Edmon_RigidBody.MovePosition(transform.position + Movement * Edmon_Speed * Edmon_SprintSpeed * Time.deltaTime);
        Edmon_RigidBody.MoveRotation(Rotation);
    }

    void CharacterWalking()
    {
        Edmon_SprintSpeed = 1.0f;               
    }

    void CharacterRunning()
    {
        Edmon_SprintSpeed = 2.0f;
    }


}
