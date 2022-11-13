using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Character : GlobalObject
{
    // ���� �����ڸ� ��� �� ������, ������Ƽ�� ����� ������
    // 0~99 ĳ���� , 100~199 ��, 200~299 ������
    public float MaxHP;
    public float CurrentHP;
    public float CurHunger;
    public float MaxMana;
    public float CurrentMana;
    public float MoveSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float DefensePoint;

    public Character DeepCopy()
    {
        Character tmp = new Character();

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
