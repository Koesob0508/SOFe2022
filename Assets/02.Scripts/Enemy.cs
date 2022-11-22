using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : Character
{
    public Vector2 Position;
    public uint min_Coin;
    public uint max_Coin;
    public Enemy DeepCopy(Enemy _enemy)
    {
        Enemy result = new Enemy();

        result.GUID = _enemy.GUID;
        result.Name = _enemy.Name;
        result.Type = _enemy.Type;

        result.MaxHP = _enemy.MaxHP;
        result.CurrentHP = _enemy.CurrentHP;
        result.CurHunger = _enemy.CurHunger;
        result.MaxMana = _enemy.MaxMana;
        result.CurrentMana = _enemy.CurrentMana;
        result.MoveSpeed = _enemy.MoveSpeed;
        result.AttackDamage = _enemy.AttackDamage;
        result.AttackRange = _enemy.AttackRange;
        result.AttackSpeed = _enemy.AttackSpeed;
        result.DefensePoint = _enemy.DefensePoint;
        result.Position = _enemy.Position;
        result.min_Coin = _enemy.min_Coin;
        result.max_Coin = _enemy.max_Coin;
        return result;
    }
}
