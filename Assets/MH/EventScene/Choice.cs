using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice : MonoBehaviour
{
    [Header("����")]
    public string text;

    [Header("Ÿ�� MBTI")]
    public List<GameManager.MbtiType> TargetMbti;

    public void MakeChoice()
    {
        EventManager choice = GameObject.Find("Event Manager").transform.GetComponent<EventManager>();
        string str = this.transform.name;
        choice.ChoiceMode = Int32.Parse(str.Substring(str.Length - 1));
    }
}
