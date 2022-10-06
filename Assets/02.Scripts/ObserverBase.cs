using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverBase : MonoBehaviour
{
    public enum EventType
    {
        R_Enroll,
        R_Dismiss,
        B_Attack,
        B_Hit,
        B_Skill,
        B_Win,
        B_Lose
    }
    /// <summary>
    /// Enroll, Dismiss - Pass the target Hero
    /// Attack, Hit, Skill - Pass the Causer and Target
    /// Win, Lost - Doesnt need to pass anything
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="type"></param>
    public virtual void onNotify(EventType type, Object[] entities = null)
    {
    }
}
