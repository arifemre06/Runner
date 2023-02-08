using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class cameratargetcontroller : MonoBehaviour
{   
    

    private CharacterController controller;
    [SerializeField] private SwipeManager swipeManager;
    private Vector3 direction;
    [SerializeField] private float playerPoint;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private int desiredLane;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        desiredLane = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted)
        {
            return;
        }

        direction.z = forwardSpeed;
        
        controller.Move(direction * Time.fixedDeltaTime);
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }


        var pos = transform.position;
        Vector3 targetPosition = pos.z * transform.forward + pos.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }



        if (transform.position == targetPosition)
        {
            return;
        }

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
        // transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);


    }

    
    private void Jump()
    {
        Debug.Log("jump : "+direction.y);
        direction.y = jumpForce;
    }
    
    
}
