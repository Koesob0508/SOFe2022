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
            isFinished = false;
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

            unitComp.skillFinished += FinishSkill;

            //�ҷ��ָ� BT ���߰� �� �½�ũ�� TickTask ����
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
