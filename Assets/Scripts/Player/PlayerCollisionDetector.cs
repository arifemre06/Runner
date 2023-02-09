using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private int detectionEnemyCount;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float enemyDetectionRange;
        [SerializeField] private float enemyFightRange;
        
        public event Action<Enemy[]> enemyDetected;
        public event Action<Enemy[]> enemyInFightRange;
        public event Action<GateController> gateHitExit;
        private bool _isActive;
        private Collider[] _hits;

        private void Awake()
        {
            _hits = new Collider[detectionEnemyCount];
        }

        public void SetCollisionDetectionActive(bool isActive)
        {
            _isActive = isActive;
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;
            
            CheckDetectionRange();
            CheckFightRange();
        }

        private void CheckDetectionRange()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, enemyDetectionRange, _hits, enemyLayer);
            if (hitCount > 0)
            {
                var hitEnemies = new Enemy[hitCount];
                for (int i = 0; i < hitCount; i++)
                {
                    var enemy = _hits[i].GetComponent<Enemy>();
                    hitEnemies[i] = enemy;
                }

                enemyDetected?.Invoke(hitEnemies);
            }
        }

        private void CheckFightRange()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, enemyFightRange, _hits, enemyLayer);
            if (hitCount > 0)
            {
                var hitEnemies = new Enemy[hitCount];
                for (int i = 0; i < hitCount; i++)
                {
                    var enemy = _hits[i].GetComponent<Enemy>();
                    hitEnemies[i] = enemy;
                }

                enemyInFightRange?.Invoke(hitEnemies);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag(Tags.Gate))
            {
                GateController gate = other.GetComponent<GateController>();
                
                gateHitExit?.Invoke(gate);
                
            }
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.transform.CompareTag((Tags.Obstacle)))
            {
                EventManager.RaiseOnGameFailed();
            }
        }
    }
}