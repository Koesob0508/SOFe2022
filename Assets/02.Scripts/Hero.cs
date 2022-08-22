using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public bool IsActive;
    public GameManager.MbtiType MBTI;

    public List<Item> Items = new List<Item>();
}
