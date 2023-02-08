using System;
using TMPro;
using UnityEngine;

public class GateController : MonoBehaviour
{

    [SerializeField] private EffectType effectType;
    [SerializeField] private float effect;
    [SerializeField] private TextMeshPro gateText;

    private void Start()
    {
        if (effectType == EffectType.Multiplication)
        {
            gateText.text = $"x{effect}";
        }
        else if (effectType == EffectType.Addition && effect >= 0 )
        {
            gateText.text = $"+{effect}";
        }
        else if (effectType == EffectType.Addition && effect < 0)
        {
            gateText.text = $"{effect}";
        }
            
    }

    public float GetEffect()
    {
        return effect;
    }
    
    public EffectType GetEffectType()
    {
        return effectType;
    }

    public enum EffectType
    {
        Multiplication,
        Addition,
    }
}
