using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer_Battle : ObserverBase
{
    /// <summary>
    /// On-Off Event Allowed by enroll, dismiss
    /// </summary>
    public enum SynergyEvent
    {
        SendMeHome,
        SilentScream,
        TillPlanZ,
        NoPlan,
    }
    /// <summary>
    /// Single-Shot Event Allowed by attck, hit, skill
    /// </summary>
    public enum BattleEvent
    {
        Teamwork,

    }
    List<byte> heroCount_MBTI = new List<byte>() {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    Dictionary<SynergyEvent, Func<bool>> syn_Checker = new Dictionary<SynergyEvent, Func<bool>>();
    Dictionary<SynergyEvent, bool> syn_State = new Dictionary<SynergyEvent, bool>();

    byte heroTotal = 0;
    byte ExxxCount = 0;
    byte IxxxCount = 0;
    byte xxxPCount = 0;
    byte xxxJCount = 0;

    public void Init()
    {
        syn_Checker.Add(SynergyEvent.SendMeHome, Check_SendMeHome);
        syn_Checker.Add(SynergyEvent.TillPlanZ, Check_TillPlanZ);
        syn_Checker.Add(SynergyEvent.SilentScream, Check_SilentScream);
        syn_Checker.Add(SynergyEvent.NoPlan, Check_NoPlan);
        syn_State.Add(SynergyEvent.SendMeHome, false);
        syn_State.Add(SynergyEvent.TillPlanZ, false);
        syn_State.Add(SynergyEvent.SilentScream, false);
        syn_State.Add(SynergyEvent.NoPlan, false);
    }
    bool Check_SendMeHome()
    {
        return ((heroTotal * ((float)2 / 3)) < ExxxCount) ? true : false;
    }
    bool Check_TillPlanZ()
    {
        return ((heroTotal * ((float)2 / 3)) < xxxJCount) ? true : false;
    }
    bool Check_SilentScream()
    {
        return ((heroTotal * ((float)2 / 3)) < IxxxCount) ? true : false;
    }
    bool Check_NoPlan()
    {
        return ((heroTotal * ((float)2 / 3)) < xxxPCount) ? true : false;
    }
    void CheckSynergy()
    {
        Dictionary<SynergyEvent, bool> updated = new Dictionary<SynergyEvent, bool>();
        foreach(SynergyEvent e in syn_Checker.Keys)
        {
            bool result = syn_Checker[e]();
            if (syn_State[e] != result)
            {
                updated[e] = result;
                syn_State[e] = result;
            }
        }
        GameManager.Battle.UpdateSynergy(updated);
    }
    void Enroll(UnityEngine.Object obj)
    {
        GameObject o = obj as GameObject;
        if (o == null)
            return;
        Hero hData = o.GetComponent<Battle_Heros>().charData as Hero;
        if (hData == null)
            return;

        heroCount_MBTI[(int)hData.MBTI]++;
        heroTotal++;

        switch (hData.MBTI)
        {
            case GameManager.MbtiType.ENFP:
            case GameManager.MbtiType.ENTP:
            case GameManager.MbtiType.ESFP:
            case GameManager.MbtiType.ESTP:
                ExxxCount++; xxxPCount++;
                break;
            case GameManager.MbtiType.ENTJ:
            case GameManager.MbtiType.ENFJ:
            case GameManager.MbtiType.ESTJ:
            case GameManager.MbtiType.ESFJ:
                ExxxCount++; xxxJCount++;
                break;
            case GameManager.MbtiType.INFP:
            case GameManager.MbtiType.INTP:
            case GameManager.MbtiType.ISFP:
            case GameManager.MbtiType.ISTP:
                IxxxCount++; xxxPCount++;
                break;
            case GameManager.MbtiType.INFJ:
            case GameManager.MbtiType.INTJ:
            case GameManager.MbtiType.ISFJ:
            case GameManager.MbtiType.ISTJ:
                IxxxCount++; xxxJCount++;
                break;
        }
        CheckSynergy();
    }
    void Dismiss(UnityEngine.Object obj)
    {
        GameObject o = obj as GameObject;
        if (o == null)
            return;
        Hero hData = o.GetComponent<Battle_Heros>().charData as Hero;
        if (hData == null)
            return;
        heroCount_MBTI[(int)hData.MBTI]--;
        heroTotal--;

        switch (hData.MBTI)
        {
            case GameManager.MbtiType.ENFP:
            case GameManager.MbtiType.ENTP:
            case GameManager.MbtiType.ESFP:
            case GameManager.MbtiType.ESTP:
                ExxxCount--; xxxPCount--;
                break;
            case GameManager.MbtiType.ENTJ:
            case GameManager.MbtiType.ENFJ:
            case GameManager.MbtiType.ESTJ:
            case GameManager.MbtiType.ESFJ:
                ExxxCount--; xxxJCount--;
                break;
            case GameManager.MbtiType.INFP:
            case GameManager.MbtiType.INTP:
            case GameManager.MbtiType.ISFP:
            case GameManager.MbtiType.ISTP:
                IxxxCount--; xxxPCount--;
                break;
            case GameManager.MbtiType.INFJ:
            case GameManager.MbtiType.INTJ:
            case GameManager.MbtiType.ISFJ:
            case GameManager.MbtiType.ISTJ:
                IxxxCount--; xxxJCount--;
                break;
        }
        CheckSynergy();
    }
    public override void onNotify(EventType type, UnityEngine.Object[] entities = null)
    {
        switch (type)
        {
            case EventType.R_Enroll:
                Enroll(entities[0]);
                break;
            case EventType.R_Dismiss:
                Dismiss(entities[0]);
                break;
            case EventType.B_Attack:
                break;
            case EventType.B_Hit:
                break;
            case EventType.B_Skill:
                break;
            case EventType.B_Win:
                break;
            case EventType.B_Lose:
                break;
        }
    }

}

