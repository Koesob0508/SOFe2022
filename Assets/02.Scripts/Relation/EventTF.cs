using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTF : CustomEvent
{
    protected override void Apply()
    {
        Debug.Log("T와 F 간 이벤트 발생");
    }

    protected override bool Condition(LogInfo _logInfo)
    {
        return false;
    }
}
