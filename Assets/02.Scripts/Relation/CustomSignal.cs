using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomSignal : MonoBehaviour
{
    public void Judge(BattleLogPanel.Log _log)
    {
        if(Condition(_log))
        {
            Apply();
        }
    }

    protected abstract bool Condition(BattleLogPanel.Log _log);
    protected abstract void Apply();
}
