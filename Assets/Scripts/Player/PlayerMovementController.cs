using DefaultNamespace;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private CharacterController controller;
    private Vector3 direction;
    private bool isInputAvailable = true;
    private bool canMove;
    private int desiredLane;
    private float currentForwardSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (desiredLane == 0)
        {
            desiredLane = 3;
        }
        currentForwardSpeed = forwardSpeed;
    }

    private void Update()
    {
        if (!canMove)
            return;

        Movement();
    }


    public void StartMoving()
    {
        canMove = true;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }


    private void Movement()
    {
        direction.z = currentForwardSpeed;

        controller.Move(direction * Time.fixedDeltaTime);
        if (isInputAvailable)
        {
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
                if (desiredLane == 5)
                {
                    desiredLane = 4;
                }
            }

            if (SwipeManager.swipeLeft)
            {
                desiredLane--;
                if (desiredLane == 1)
                {
                    desiredLane = 2;
                }
            }


            var pos = transform.position;
            Vector3 targetPosition = pos.z * transform.forward + pos.y * transform.up;

            if (desiredLane == 2)
            {
                targetPosition += Vector3.left * laneDistance;
            }
            else if (desiredLane == 4)
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
        }
    }
    

    public void SetIsInputAvailable(bool isAvailable)
    {
        if (isAvailable)
        {
            currentForwardSpeed = forwardSpeed;
            isInputAvailable = true;
        }
        else
        {
            currentForwardSpeed = 0;
            isInputAvailable = false;
        }
    }
}