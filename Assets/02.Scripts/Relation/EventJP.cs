using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventJP : CustomEvent
{
    protected override void Apply()
    {
        Debug.Log("J�� P �� �̺�Ʈ �߻�");
    }

    protected override bool Condition(LogInfo _logInfo)
    {
        return false;
    }
}
