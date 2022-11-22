using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTest : CustomSignal
{
    public override void Init()
    {
        signalTitle = "테스트 시그널입니다";
        signalExplain = "호감작입니다";
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        return true;
    }

    protected override void Apply()
    {
        Debug.Log("테스트 로그");
    }
}
