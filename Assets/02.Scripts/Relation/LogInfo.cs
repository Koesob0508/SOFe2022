using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Verb { Attack, Kill, Buff, Debuff }

/// <summary>
/// Character인지 Unit인지는 회의 때 여쭤보고 결정하기
/// </summary>
public struct LogInfo
{
    private GameObject subjectObject;
    private GameObject objectObject;
    private Character subjective;
    private Character objective;
    private Verb verb;

    public Character Subjective { get => subjective; set => subjective = value; }
    public Character Objective { get => objective; set => objective = value; }
    public GameObject SubjectObject { get => subjectObject; set => subjectObject = value; }
    public GameObject ObjectObject { get => objectObject; set => objectObject = value; }
    public Verb Verb { get => verb; set => verb = value; }
}
