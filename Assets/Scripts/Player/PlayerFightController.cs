using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFightController : MonoBehaviour
    {
        
        public event Action<bool> fightResulted;
        [SerializeField] private PlayerManager playerManager;
        public void OnEnemyInFightsRange(Enemy[] enemies)
        {
            float enemypower = enemies[0].GetPower();
            if (playerManager.PlayerPoint> enemypower)
            {
                FightWinOperations(enemies[0]);
            }
            else
            {
                FightLostOpperations(enemies[0]);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.SetIsMovingToPlayer(false,transform);
            }
            
        }

        private void FightWinOperations(Enemy enemy)
        {
            playerManager.PlayerPoint -= enemy.GetPower();
            enemy.Die();
            fightResulted?.Invoke(true);
        }

        private void FightLostOpperations(Enemy enemy)
        {
            playerManager.PlayerPoint = 0;
            enemy.SetPower(enemy.GetPower() - playerManager.PlayerPoint);
            fightResulted?.Invoke(false);
        }
    }
}