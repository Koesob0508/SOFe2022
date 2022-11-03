using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Verb { Attack, Kill, Buff, Debuff }

/// <summary>
/// Character���� Unit������ ȸ�� �� ���庸�� �����ϱ�
/// </summary>
public struct LogInfo
{
    private Character subjective;
    private Character objective;
    private Verb verb;

    public Character Subjective { get => subjective; set => subjective = value; }
    public Character Objective { get => objective; set => objective = value; }
    public Verb Verb { get => verb; set => verb = value; }
}
