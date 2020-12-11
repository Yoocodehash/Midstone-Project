using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //OBJECTS
    public Transform cam;
    CharacterController mover;

    //CMAERA
    Vector3 camF;
    Vector3 camR;

    //INPUT
    Vector2 input;

    //PHYSICS
    Vector3 intent;
    Vector3 velocityZ;
    Vector3 velocity;
    float speed = 5;
    float accel = 11;
    float turnSpeed = 4;
    float turnSpeedLow = 1;
    float turnSpeedHigh = 3;

    //GRAVITY
    float grav = 14f;
    public bool grounded = false;

    void Start()
    {
        mover = GetComponent<CharacterController>();
    }

  void Update()
    {
        DoInput();
       CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();

        mover.Move(velocity * Time.deltaTime);
    }

    void DoInput()
    {

        input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input,1);
    }

    void CalculateCamera()
    {

        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.2f))
        {
            grounded = true;
        }
        else
            grounded = false;
    }

    void DoMove()
    {
     intent = camF*input.y + camR*input.x;

        float tS = velocity.magnitude/5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh,turnSpeedLow, tS);
        if(input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed*Time.deltaTime);
        }

        velocityZ = velocity;
        velocityZ.y = 0;
        velocityZ = Vector3.Lerp(velocityZ, transform.forward* input.magnitude*speed, accel*Time.deltaTime);
        velocity = new Vector3(velocityZ.x, velocity.y, velocityZ.z);

    }

    void DoGravity()
    {
        if (grounded)
            velocity.y = -1f;
        else
        velocity.y -= grav*Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    void DoJump()
    {
        if(grounded)
        {
            if (Input.GetButtonDown("Jump"))
                velocity.y = 10;
        }
    }
}
