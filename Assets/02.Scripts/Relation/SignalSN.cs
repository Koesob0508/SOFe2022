using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSN : CustomSignal
{
    protected override void Apply()
    {
        Debug.Log("S�� N �� �̺�Ʈ �߻�");
    }

    protected override bool Condition(LogInfo _logInfo)
    {
        return false;
    }
}
