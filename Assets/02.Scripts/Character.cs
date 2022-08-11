using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    // ���� �����ڸ� ��� �� ������, ������Ƽ�� ����� ������
    // 0~99 ĳ���� , 100~199 ��, 200~299 ������
    public uint GUID;
    public string Name;
    public float MaxHP;
    public float CurrentHP =100;
    public float CurHunger =100;
    public float MaxMana;
    public float CurrentMana;
    public float MoveSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float DefensePoint;
}
