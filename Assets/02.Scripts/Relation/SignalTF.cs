using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTF : CustomSignal
{
    public override void Init()
    {
    }

    protected override void Apply()
    {
        Debug.Log("T와 F 간 이벤트 발생");
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return false;
    }
}
