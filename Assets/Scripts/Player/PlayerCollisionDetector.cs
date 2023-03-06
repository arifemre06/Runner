using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private int detectionEnemyCount;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float enemyDetectionRange;
        [SerializeField] private float enemyFightRange;
        private bool _isEnemyInDetectionRange;
        private bool _isEnemyInFightRange;
        
        
        public event Action<Enemy[]> enemyDetected;
        public event Action<Enemy[]> enemyInFightRange;
        public event Action<GateController> gateHitExit;
        public event Action<GameObject> ObstacleHit;
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
            if (hitCount > 0 && !_isEnemyInDetectionRange)
            {   
                
                var hitEnemies = new Enemy[hitCount];
                for (int i = 0; i < hitCount; i++)
                {
                    var enemy = _hits[i].GetComponent<Enemy>();
                    hitEnemies[i] = enemy;
                }
                
                _isEnemyInDetectionRange = true;
                Debug.Log("noluyo");
                enemyDetected?.Invoke(hitEnemies);
            }
            else if(hitCount == 0)
            {
                _isEnemyInDetectionRange = false;
            }
        }

        private void CheckFightRange()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, enemyFightRange, _hits, enemyLayer);
            if (hitCount > 0 && !_isEnemyInFightRange)
            {
                var hitEnemies = new Enemy[hitCount];
                for (int i = 0; i < hitCount; i++)
                {
                    var enemy = _hits[i].GetComponent<Enemy>();
                    hitEnemies[i] = enemy;
                }

                _isEnemyInFightRange = true;
                enemyInFightRange?.Invoke(hitEnemies);
            }
            else if(hitCount == 0)
            {
                _isEnemyInFightRange = false;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag(Tags.Gate))
            {
                GateController gate = other.GetComponent<GateController>();
                
                gateHitExit?.Invoke(gate);
            }
            else if (other.transform.CompareTag(Tags.FinishTriggerObject))
            {
                Debug.Log("level finished");
                EventManager.RaiseArrivedToFinish();
            }
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!_isActive)
            {
                return;
            }

            if (hit.transform.CompareTag(Tags.Obstacle))
            {   
                ObstacleHit?.Invoke(hit.gameObject);
                
            }
        }
    }
}