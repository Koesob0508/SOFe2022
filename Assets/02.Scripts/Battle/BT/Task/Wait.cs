using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Wait : TaskNode
    {


        [HideInInspector] public int keyIdx = 0;
        [HideInInspector] public string keyName;

        private float WaitTime = 1f;
        private float startTime = 0.0f;
        protected override void OnStart()
        {
            Debug.Log("waitStarted");
            WaitTime = (float)bBoard.bb_keys.Find(n => n.Name == keyName).Value;
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            if (Time.time - startTime > WaitTime)
                return State.Succeeded;
            else
                return State.InProgress;
        }
    }
}
