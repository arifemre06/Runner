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
        [SerializeField] private PlayerAnimController PlayerAnimController;
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
            playerCollisionDetector.enemyInFightRange += playerFightController.OnEnemyInFightsRange;
            playerFightController.fightResulted += OnFightResulted;
            playerCollisionDetector.gateHitExit += OnGateHitExit;
        }

        

        private void Start()
        {
            playerModelSwitchController.UpdateModel(PlayerPoint);
            playerPointText.text = PlayerPoint.ToString();
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
                PlayerPoint *= gate.GetEffect();
            }
            else
            {
                PlayerPoint  += gate.GetEffect();
            }
            playerModelSwitchController.UpdateModel(PlayerPoint);
            
            
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