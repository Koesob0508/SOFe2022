using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChioiceController : MonoBehaviour
{
    [Header("질문")]
    public string Ask;

    [Header("선택지의 개수")]
    public uint choiceNum;

    [Header("선택지")]
    public Choice choice1;
    public Choice choice2;
    public Choice choice3;
}
