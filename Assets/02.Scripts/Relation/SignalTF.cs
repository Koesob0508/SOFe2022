using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTF : CustomSignal
{
    protected override void Apply()
    {
        Debug.Log("T�� F �� �̺�Ʈ �߻�");
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return false;
    }
}
