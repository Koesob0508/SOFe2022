using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public bool IsActive;
    public GameManager.MbtiType MBTI;
    public uint Cost = 1000;

    public List<Item> Items = new List<Item>();
}
