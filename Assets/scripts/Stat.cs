using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField]private int baseValue;

    public List<int> modifies;

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach(int modifier in modifies)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void AddModifier(int _modifies)
    {
        modifies.Add( _modifies );
    }

    public void RemoveModifier(int _modifies)
    {
        modifies.Remove( _modifies );
    }
}
