using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomEvent : MonoBehaviour
{
    public void Judge(LogInfo _logInfo)
    {
        if(Condition(_logInfo))
        {
            Apply();
        }
    }

    protected abstract bool Condition(LogInfo _logInfo);
    protected abstract void Apply();
}
