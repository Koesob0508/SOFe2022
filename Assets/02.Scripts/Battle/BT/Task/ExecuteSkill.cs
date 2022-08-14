using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class ExecuteSkill : TaskNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            var unitComp = owner_comp.gameObject.GetComponent<Units>();
            if (unitComp != null)
                unitComp.ExecuteSkill();
            owner_comp.TreeObject.bBoard.SetValueAsBool("CanSkill", false);
            return State.Succeeded;
        }
    }

}
