using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    // ���� �����ڸ� ��� �� ������, ������Ƽ�� ����� ������
    public uint GUID;
    public string Name;
    public float MaxHP;
    public float CurrentHP;
    public float Hunger;
    public float MaxMana;
    public float CurrentMana;
    public float MoveSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float DefensePoint;

    public GameObject UnitUIObject;
    public Image healthBar;
    // ���⿡�� �� UI Update?


}
