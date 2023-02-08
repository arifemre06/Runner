using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour
{

    [SerializeField] private int effectType;
    [SerializeField] private float effect; 
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public float GetEffect()
    {
        return effect;
    }
    
    public int GetEffectType()
    {
        return effectType;
    }
    
}
