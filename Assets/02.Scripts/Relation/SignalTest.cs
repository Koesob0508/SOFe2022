using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTest : CustomSignal
{
    public override void Init()
    {
        signalTitle = "�׽�Ʈ �ñ׳��Դϴ�.";
        signalExplain = "�׽�Ʈ �����Դϴ�.";
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return true;
    }

    protected override void Apply()
    {
        Debug.Log("�׽�Ʈ �α�");
    }
}
