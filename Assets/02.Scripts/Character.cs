using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Character : GlobalObject
{
    // 접근 한정자를 어떻게 할 것인지, 프로퍼티를 사용할 것인지
    // 0~99 캐릭터 , 100~199 적, 200~299 아이템
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
}
