using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomRank : MonoBehaviour
{
    [SerializeField] protected List<Hero> appliedHero;
    public void Init()
    {
        appliedHero = new List<Hero>();
    }

    public void Judge(int _teamScore)
    {
        if(Condition(_teamScore))
        {
            Apply();
        }
        else
        {
            Clear();
        }
    }

    protected abstract bool Condition(int _teamScore);
    protected abstract void Apply();
    protected abstract void Clear();
}
