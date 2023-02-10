using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float fightRange;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float power;
    [SerializeField] private PlayerModelSwitchController _playerModelSwitchController;
    [SerializeField] private TextMeshPro enemyPointText;
    private Transform _targetTransform;
    private bool _isMovingToPlayer = false;
    private bool notFightYet;

    
    private void Start()
    {
        _playerModelSwitchController.UpdateModel(power);
        enemyPointText.text = power.ToString();

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
        enemyPointText.text = newPower.ToString();
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
