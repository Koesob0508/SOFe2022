using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class ExecuteSkill : TaskNode
    {
        bool isFinished = false;
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            isFinished = false;
            var unitComp = owner_comp.gameObject.GetComponent<Units>();
            if (unitComp != null)
                unitComp.ExecuteSkill();
            unitComp.skillFinished += FinishSkill;
            owner_comp.TreeObject.bBoard.SetValueAsBool("CanSkill", false);


            //불러주면 BT 멈추고 이 태스크의 TickTask 진행
            owner_comp.TreeObject.TaskInProgress(this);
            return State.InProgress;
        }
        void FinishSkill()
        {
            isFinished = true;
        }
        public override State TickTask(BehaviorTreeComponent owner_comp)
        {
            if (isFinished)
            {
                return State.Succeeded;
            }
            else
                return State.InProgress;
        }
    }

}
