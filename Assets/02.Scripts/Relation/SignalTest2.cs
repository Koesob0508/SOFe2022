using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTest2 : CustomSignal
{
    public override void Init()
    {
        signalTitle = "�׽�Ʈ �ñ׳��Դϴ�22";
        signalExplain = "�׽�Ʈ �����Դϴ�22";
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return true;
    }

    protected override void Apply()
    {
        Debug.Log("�׽�Ʈ �α�22");
    }
}
