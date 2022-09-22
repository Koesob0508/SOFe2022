using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hero : Character
{
    public GameManager.MbtiType MBTI;
    public uint Cost = 1000;
    public bool isDead = false;
    public bool IsActive;

    public List<Item> Items = new List<Item>();
}
