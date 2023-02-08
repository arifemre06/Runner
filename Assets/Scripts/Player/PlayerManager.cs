using System;
using System.Diagnostics;
using Sirenix.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerFightController playerFightController;
        [SerializeField] private PlayerCollisionDetector playerCollisionDetector;
        [SerializeField] private PlayerModelSwitchController playerModelSwitchController;
        [SerializeField] private float playerPoint;
        

        public float PlayerPoint
        {
            get => playerPoint;
            set => playerPoint = value;
        }

        private void Awake()
        {
            playerCollisionDetector.enemyDetected += OnEnemyDetected;
            playerCollisionDetector.enemyInFightRange += playerFightController.OnEnemyInFightsRange;
            playerFightController.fightResulted += OnFightResulted;
            playerCollisionDetector.gateHitExit += OnGateHitExit;
        }

        private void Start()
        {
            playerModelSwitchController.UpdateModel(playerPoint);
        }

        private void Die()
        {   
            gameObject.SetActive(false);
            EventManager.RaiseOnGameFailed();
        }

        private void OnFightResulted(bool fightResult)
        {
            if (fightResult)
            {
                playerMovementController.StartMoving();
                playerMovementController.SetIsInputAvailable(true);
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
                playerPoint *= gate.GetEffect();
            }
            else
            {
                playerPoint  += gate.GetEffect();
            }
            playerModelSwitchController.UpdateModel(playerPoint);
            

            Debug.Log("puan " + playerPoint );
            
        }

        private void OnEnemyDetected(Enemy[] enemies)
        {
            playerMovementController.SetIsInputAvailable(false);
            foreach (var enemy in enemies)
            {
                enemy.SetIsMovingToPlayer(true,transform);
            }
        }

        public void StartGame()
        {
            playerMovementController.StartMoving();
            
            playerCollisionDetector.SetCollisionDetectionActive(true);
        }

    }
}