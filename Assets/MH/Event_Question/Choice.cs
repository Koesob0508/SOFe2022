using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    [Header("³»¿ë")]
    public string text;

    [Header("Å¸°Ù MBTI")]
    public uint num;

    public List<GameManager.MbtiType> TargetMbti;
}
