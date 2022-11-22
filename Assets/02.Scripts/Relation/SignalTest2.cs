using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTest2 : CustomSignal
{
    public override void Init()
    {
        signalTitle = "테스트 시그널입니다22";
        signalExplain = "테스트 설명입니다22";
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return true;
    }

    protected override void Apply()
    {
    }
}
