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
        Debug.Log("J�� P �� �̺�Ʈ �߻�");
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return false;
    }
}
