using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSN : CustomSignal
{
    protected override void Apply()
    {
        Debug.Log("S와 N 간 이벤트 발생");
    }

    protected override bool Condition(LogInfo _logInfo)
    {
        return false;
    }
}
