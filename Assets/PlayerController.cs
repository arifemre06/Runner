using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private GameObject modelA;
    [SerializeField] private GameObject modelB;
    [SerializeField] private GameObject modelC;
    private GameObject[] EnemiesOnScene;

    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private float playerPoint;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private bool movementAvaible = true;

    private int desiredLane;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (desiredLane == 0)
        {
            desiredLane = 3;
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (!GameManager.isGameStarted)
        {   
            
            return;
        }
        Movement();




    }



    private void Jump()
    {
       
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            GameManager.gameOverBool = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Gate")
        {
            gateController gate = other.GetComponent<gateController>();
            if (gate.GetEffectType() == 1)
            {
                playerPoint *= gate.GetEffect();
            }
            else
            {
                playerPoint += gate.GetEffect();
            }
            Debug.Log("puan " + playerPoint);
            ModelSwitch();
        }
    }
    
    void ModelSwitch()
    {       
         
             if(playerPoint <= 5 && playerPoint != 0)
             {

                 GameObject.Find("playermodel1").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                 GameObject.Find("playermodel2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                 SkinnedMeshRenderer[] skinnedMeshRendererModel3 =
                     GameObject.Find("playermodel3").GetComponentsInChildren<SkinnedMeshRenderer>();
                 foreach (var parts in skinnedMeshRendererModel3)
                 {
                     parts.enabled = false;
                 }
                 
                 
             }
             else if(playerPoint > 5 && playerPoint <= 10)
             {  
                 GameObject.Find("playermodel2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                 GameObject.Find("playermodel1").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                 SkinnedMeshRenderer[] skinnedMeshRendererModel3 =
                     GameObject.Find("playermodel3").GetComponentsInChildren<SkinnedMeshRenderer>();
                 foreach (var parts in skinnedMeshRendererModel3)
                 {
                     parts.enabled = false;
                 }
                 
                 
             }
             else if(playerPoint > 10 && playerPoint < 15)
             {  
                 SkinnedMeshRenderer[] skinnedMeshRendererModel3 =
                     GameObject.Find("playermodel3").GetComponentsInChildren<SkinnedMeshRenderer>();
                 foreach (var parts in skinnedMeshRendererModel3)
                 {
                     parts.enabled = true;
                 }
                 GameObject.Find("playermodel1").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                 GameObject.Find("playermodel2").GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                 
                 
             } 
    }

    void Movement()
    {
        direction.z = forwardSpeed;
        
        controller.Move(direction * Time.fixedDeltaTime);
        if (movementAvaible)
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
    
    public GameObject FindClosestEnemy()
    {
        GameObject[] enemiesOnScreen;
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemiesOnScreen)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }

    public void FightStatusActivate()
    {
        forwardSpeed = 0;
        movementAvaible = false;
    }
    
    public void FightStatusDeactivate()
    {
        forwardSpeed = 2;
        movementAvaible = true;
    }

    public float GetPoint()
    {
        return playerPoint;
    }
    public void SetPoint(float player_point)
    {
        playerPoint = player_point;
        Debug.Log(playerPoint);
        if (playerPoint < 0)
        {
            GameManager.gameOverBool = true;
        }
    }
}
