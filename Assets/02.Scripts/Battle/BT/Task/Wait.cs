using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Wait : TaskNode
    {
        public float WaitTime = 1f;
        private float startTime = 0.0f;
        protected override void OnStart()
        {
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
