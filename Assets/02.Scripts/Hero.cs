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
    public bool IsActive = false;

    // public List<Item> Items = new List<Item>();
    public Item[] Items = new Item[3];
    public uint ItemNum = 0;

    public Hero DeepCopy()
    {
        Hero tmp = new Hero();

        tmp.MBTI = MBTI;
        tmp.Cost = Cost;
        tmp.isDead = isDead;
        tmp.IsActive = IsActive;
        tmp.Items = Items;
        tmp.MaxHP = MaxHP;
        tmp.CurrentHP = CurrentHP;
        tmp.CurHunger = CurHunger;
        tmp.MaxMana = MaxMana;
        tmp.CurrentMana = CurrentMana;
        tmp.MoveSpeed = MoveSpeed;
        tmp.AttackDamage = AttackDamage;
        tmp.AttackRange = AttackRange;
        tmp.AttackSpeed = AttackSpeed;
        tmp.DefensePoint = DefensePoint;
        tmp.Type = Type;
        tmp.Name = Name;
        tmp.GUID = GUID;
        return tmp;
    }
}
       