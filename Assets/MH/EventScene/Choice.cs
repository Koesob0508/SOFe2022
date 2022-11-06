using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice : MonoBehaviour
{
    [Header("³»¿ë")]
    public string text;

    [Header("Å¸°Ù MBTI")]
    public List<GameManager.MbtiType> TargetMbti;

    public void MakeChoice()
    {
        EventManager choice = GameObject.Find("Event Manager").transform.GetComponent<EventManager>();
        string str = this.transform.name;
        choice.ChoiceMode = Int32.Parse(str.Substring(str.Length - 1));
    }

    public List<GameManager.MbtiType> GetTargetMbti()
    {
        return TargetMbti;
    }
}
