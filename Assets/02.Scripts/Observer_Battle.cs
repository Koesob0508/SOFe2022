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
    Dictionary<SynergyEvent, Action<bool>> synFuncDict = new Dictionary<SynergyEvent, Action<bool>>();
    Dictionary<SynergyEvent, bool> syn_State = new Dictionary<SynergyEvent, bool>();

    byte heroTotal = 0;
    byte ExxxCount = 0;

    List<Hero> ExxxList = new List<Hero>();
    byte IxxxCount = 0;
    List<Hero> IxxxList = new List<Hero>();
    byte xxxPCount = 0;
    List<Hero> xxxPList = new List<Hero>();
    byte xxxJCount = 0;
    List<Hero> xxxJList = new List<Hero>();


    public void Init()
    {
        synFuncDict.Add(SynergyEvent.SendMeHome, Synergy_Func_SendMeHome);
        synFuncDict.Add(SynergyEvent.SilentScream, Synergy_Func_SilentScream);
        synFuncDict.Add(SynergyEvent.TillPlanZ, Synergy_Func_TillPlanZ);
        synFuncDict.Add(SynergyEvent.NoPlan, Synergy_Func_NoPlan);
        syn_Checker.Add(SynergyEvent.SendMeHome, Check_SendMeHome);
        syn_Checker.Add(SynergyEvent.TillPlanZ, Check_TillPlanZ);
        syn_Checker.Add(SynergyEvent.SilentScream, Check_SilentScream);
        syn_Checker.Add(SynergyEvent.NoPlan, Check_NoPlan);
        syn_State.Add(SynergyEvent.SendMeHome, false);
        syn_State.Add(SynergyEvent.TillPlanZ, false);
        syn_State.Add(SynergyEvent.SilentScream, false);
        syn_State.Add(SynergyEvent.NoPlan, false);
    }
    public void UpdateSynergy(Dictionary<SynergyEvent, bool> syn_Update)
    {
        foreach (var e in syn_Update)
        {
            synFuncDict[e.Key](e.Value);

            if (e.Value)
                Debug.Log(e.Key + " is Activated");
            else
                Debug.Log(e.Key + " is Deactivated");
        }
    }
    bool isEnoughHero() { return heroTotal > 2; }
    bool Check_SendMeHome()
    {
        if (!isEnoughHero())
            return false;
        return ((heroTotal * ((float)2 / 3)) < ExxxCount && IxxxCount > 0) ? true : false;
    }
    bool Check_TillPlanZ()
    {
        if (!isEnoughHero())
            return false;
        return ((heroTotal * ((float)2 / 3)) < xxxJCount && xxxPCount > 0) ? true : false;
    }
    bool Check_SilentScream()
    {
        if (!isEnoughHero())
            return false;
        return ((heroTotal * ((float)2 / 3)) < IxxxCount && ExxxCount > 0) ? true : false;
    }
    bool Check_NoPlan()
    {
        if (!isEnoughHero())
            return false;
        return ((heroTotal * ((float)2 / 3)) < xxxPCount && xxxJCount > 0) ? true : false;
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
        UpdateSynergy(updated);
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
                ExxxList.Add(hData); xxxPList.Add(hData);
                break;
            case GameManager.MbtiType.ENTJ:
            case GameManager.MbtiType.ENFJ:
            case GameManager.MbtiType.ESTJ:
            case GameManager.MbtiType.ESFJ:
                ExxxCount++; xxxJCount++;
                ExxxList.Add(hData); xxxJList.Add(hData);
                break;
            case GameManager.MbtiType.INFP:
            case GameManager.MbtiType.INTP:
            case GameManager.MbtiType.ISFP:
            case GameManager.MbtiType.ISTP:
                IxxxCount++; xxxPCount++;
                IxxxList.Add(hData); xxxPList.Add(hData);
                break;
            case GameManager.MbtiType.INFJ:
            case GameManager.MbtiType.INTJ:
            case GameManager.MbtiType.ISFJ:
            case GameManager.MbtiType.ISTJ:
                IxxxCount++; xxxJCount++;
                IxxxList.Add(hData); xxxJList.Add(hData);
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
                ExxxList.Remove(hData); xxxPList.Remove(hData);
                break;
            case GameManager.MbtiType.ENTJ:
            case GameManager.MbtiType.ENFJ:
            case GameManager.MbtiType.ESTJ:
            case GameManager.MbtiType.ESFJ:
                ExxxCount--; xxxJCount--;
                ExxxList.Remove(hData); xxxJList.Remove(hData);
                break;
            case GameManager.MbtiType.INFP:
            case GameManager.MbtiType.INTP:
            case GameManager.MbtiType.ISFP:
            case GameManager.MbtiType.ISTP:
                IxxxCount--; xxxPCount--;
                IxxxList.Remove(hData); xxxPList.Remove(hData); 
                break;
            case GameManager.MbtiType.INFJ:
            case GameManager.MbtiType.INTJ:
            case GameManager.MbtiType.ISFJ:
            case GameManager.MbtiType.ISTJ:
                IxxxCount--; xxxJCount--;
                IxxxList.Remove(hData); xxxJList.Remove(hData);
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
    #region Synergy_Functions
    void Synergy_Func_SendMeHome(bool isActivate)
    {
        SynergyEvent target = SynergyEvent.SendMeHome;
        if (isActivate)
        {
            GameManager.Battle.AddSynergyUIText(target);
            // Activation Code
            foreach (var hdata in IxxxList)
            {
                hdata.CurrentHP *= 0.9f;
            }
        }
        else
        {
            GameManager.Battle.RemoveSynergyUIText(target);
            // Deactivation Code
            foreach (var hdata in IxxxList)
                GameManager.Battle.RestoreHeroData(hdata);
        }
    }
    void Synergy_Func_SilentScream(bool isActivate)
    {
        SynergyEvent target = SynergyEvent.SilentScream;
        if (isActivate)
        {
            GameManager.Battle.AddSynergyUIText(target);
            // Activation Code
            foreach (var hdata in ExxxList)
            {
                hdata.CurrentHP *= 0.9f;
            }
        }
        else
        {
            GameManager.Battle.RemoveSynergyUIText(target);
            // Deactivation Code
            foreach (var hdata in ExxxList)
                GameManager.Battle.RestoreHeroData(hdata);
        }
    }
    void Synergy_Func_TillPlanZ(bool isActivate)
    {
        SynergyEvent target = SynergyEvent.TillPlanZ;
        if (isActivate)
        {
            GameManager.Battle.AddSynergyUIText(target);
            // Activation Code
            foreach (var hdata in xxxPList)
            {
                hdata.CurrentHP *= 0.9f;
            }
        }
        else
        {
            GameManager.Battle.RemoveSynergyUIText(target);
            // Deactivation Code
            foreach (var hdata in xxxPList)
                GameManager.Battle.RestoreHeroData(hdata);
        }
    }
    void Synergy_Func_NoPlan(bool isActivate)
    {
        SynergyEvent target = SynergyEvent.NoPlan;
        if (isActivate)
        {
            GameManager.Battle.AddSynergyUIText(target);
            // Activation Code
            foreach (var hdata in xxxJList)
            {
                hdata.CurrentHP *= 0.9f;
            }
        }
        else
        {
            GameManager.Battle.RemoveSynergyUIText(target);
            // Deactivation Code
            foreach (var hdata in xxxJList)
                GameManager.Battle.RestoreHeroData(hdata);
        }
    }
    #endregion
}

