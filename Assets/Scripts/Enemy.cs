using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _targetTransform;
    [SerializeField] private float range;
    [SerializeField] private float fightRange;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float power;
    private bool _isMovingToPlayer = false;
    private bool notFightYet;

    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (_isMovingToPlayer)
        {
            MoveToPlayerByAStep();
        }
    }

    private void MoveToPlayerByAStep()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position,
            enemySpeed * Time.deltaTime);
    }

    

    public float GetPower()
    {
        return power;
    }

    public void SetPower(float newPower)
    {
        power = newPower;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetIsMovingToPlayer(bool isToMoving,Transform targetTransform)
    {
        _isMovingToPlayer =  isToMoving;
        _targetTransform = targetTransform;
    }
}
