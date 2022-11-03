using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Hero, Enemy }
public enum Verb { Attack, Kill, Buff, Debuff }

/// <summary>
/// Character인지 Unit인지는 회의 때 여쭤보고 결정하기
/// </summary>
public struct LogInfo
{
    private UnitType subjective;
    private UnitType objective;
    private Verb verb;

    public UnitType Subjective { get => subjective; set => subjective = value; }
    public UnitType Objective { get => objective; set => objective = value; }
    public Verb Verb { get => verb; set => verb = value; }
}
