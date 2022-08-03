using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BT
{
    public class DebugLogNode : TaskNode
    {
        public string message;
        protected override void OnStart()
        {
            Debug.Log($"OnStart{message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop{message}");

        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            Debug.Log($"OnUpdate{message}");
            return State.Succeeded;
        }
    }
}

