using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    [Header("����")]
    public string text;

    [Header("Ÿ�� MBTI")]
    public uint num;

    public List<GameManager.MbtiType> TargetMbti;
}
