using System;
using System.Diagnostics;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerFightController playerFightController;
        [SerializeField] private PlayerCollisionDetector playerCollisionDetector;
        [SerializeField] private PlayerModelSwitchController playerModelSwitchController;
        [SerializeField] private PlayerAnimController playerAnimController;
        [SerializeField] private float playerPoint;
        [SerializeField] private TextMeshPro playerPointText;
        


        public float PlayerPoint
        {
            get => playerPoint;
            set
            {
                playerPoint = value;
                playerPointText.text = value.ToString();
                
            }
            
        }

        private void Awake()
        {
            playerCollisionDetector.enemyDetected += OnEnemyDetected;
            playerCollisionDetector.enemyInFightRange += OnEnemyInFightRange;
            playerCollisionDetector.enemyInFightRange += playerFightController.OnEnemyInFightsRange;
            playerFightController.fightResulted += OnFightResulted;
            playerCollisionDetector.gateHitExit += OnGateHitExit;
            playerCollisionDetector.ObstacleHit += OnObstacleHit;
            
        }

        private void OnEnemyInFightRange(Enemy[] obj)
        {
            playerAnimController.PlayFightAnimation();
            Debug.Log("buraya ne zaman gırıyoz");
        }

        private void OnObstacleHit(GameObject obj)
        {
            Die();
        }


        private void Start()
        {
            playerModelSwitchController.UpdateModel(PlayerPoint);
            
            playerAnimController.UpdateAnimator(playerPoint);
            
            playerPointText.text = PlayerPoint.ToString();
        }

        private void Die()
        {
            playerCollisionDetector.SetCollisionDetectionActive(false);
            playerMovementController.SetIsInputAvailable(false);
            
            playerAnimController.PlayDieAnimation();
            EventManager.RaiseOnGameFailed();
        }

        private void OnFightResulted(bool fightResult)
        {
            if (fightResult)
            {   
                playerModelSwitchController.UpdateModel(PlayerPoint);
                
                playerAnimController.UpdateAnimator(PlayerPoint);
                
                playerMovementController.StartMoving();
                playerMovementController.SetIsInputAvailable(true);
                Debug.Log("fight kazanıldı");
                playerAnimController.PlayRunningAnimation();
                
            }
            else
            {
                Die();
            }
        }

        private void OnGateHitExit(GateController gate)
        {
            if (gate.GetEffectType() == GateController.EffectType.Multiplication)
            {
                PlayerPoint *= gate.GetEffect();
            }
            else
            {
                PlayerPoint  += gate.GetEffect();
            }
            playerModelSwitchController.UpdateModel(PlayerPoint);
            playerAnimController.UpdateAnimator(playerPoint);
            
        }

        private void OnEnemyDetected(Enemy[] enemies)
        {
            playerMovementController.SetIsInputAvailable(false);
            
            playerAnimController.PlayIdleAnimation();
            
            foreach (var enemy in enemies)
            {
                enemy.SetIsMovingToPlayer(true,transform);
            }
        }

        public void StartGame()
        {
            playerMovementController.StartMoving();
            
            playerAnimController.PlayRunningAnimation();
            
            playerCollisionDetector.SetCollisionDetectionActive(true);
        }

       

    }
}