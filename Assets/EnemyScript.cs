using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float range;
    [SerializeField] private float fightRange;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float power;
    private bool notFightYet;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
        
        if (distanceToPlayer < range && distanceToPlayer > fightRange)
        {
            notFightYet = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                enemySpeed * Time.fixedDeltaTime);
            
            playerController.FightStatusActivate();
        }
        else if (distanceToPlayer < fightRange && notFightYet)
        {
            float powerAfterFight = power - playerController.GetPoint();
            playerController.SetPoint(playerController.GetPoint() - power);
            
            if (powerAfterFight < 0)
            {
                Destroy(gameObject);
                playerController.FightStatusDeactivate();
                
            }

            notFightYet = false;


        }
    }
}
