using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalJP : CustomSignal
{
    public override void Init()
    {
    }

    protected override void Apply()
    {
        Debug.Log("J와 P 간 이벤트 발생");
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return false;
    }
}
