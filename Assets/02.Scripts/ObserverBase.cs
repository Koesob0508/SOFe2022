using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverBase : MonoBehaviour
{
    public enum EventType
    {
        R_EnrollINFP,
        R_EnrollENFP,
        R_EnrollINFJ,
        R_EnrollENFJ,
        R_EnrollINTJ,
        R_EnrollENTJ,
        R_EnrollINTP,
        R_EnrollENTP,
        R_EnrollISFP,
        R_EnrollESFP,
        R_EnrollISTP,
        R_EnrollESTP,
        R_EnrollISFJ,
        R_EnrollESFJ,
        R_EnrollISTJ,
        R_EnrollESTJ,
        R_DismissINFP,
        R_DismissENFP,
        R_DismissINFJ,
        R_DismissENFJ,
        R_DismissINTJ,
        R_DismissENTJ,
        R_DismissINTP,
        R_DismissENTP,
        R_DismissISFP,
        R_DismissESFP,
        R_DismissISTP,
        R_DismissESTP,
        R_DismissISFJ,
        R_DismissESFJ,
        R_DismissISTJ,
        R_DismissESTJ,
        B_Attack,
        B_Hit,
    }
    public virtual void onNotify(List<GlobalObject> causers, EventType type)
    {

    }
    protected virtual void UnLock()
    {

    }
}
