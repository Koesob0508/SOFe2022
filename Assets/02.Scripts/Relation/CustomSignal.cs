using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomSignal : MonoBehaviour
{
    [SerializeField] protected string signalTitle;
    [SerializeField] protected string signalExplain;
    public abstract void  Init();
    public (string, string) Judge(BattleLogPanel.Log _log)
    {
        if(Condition(_log))
        {
            Apply();

            return (signalTitle, signalExplain);
        }

        return (null, null);
    }

    protected abstract bool Condition(BattleLogPanel.Log _log);
    protected abstract void Apply();
}
