using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class AttackTarget : TaskNode
    {
        protected override void OnStart()
        {
            Debug.Log("Start attack");
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            Blackboard bb = owner_comp.TreeObject.bBoard;
            GameObject targetObj = bb.GetValueAsGameObject("targetObj");
            if(targetObj.GetComponent<BehaviorTreeComponent>().TreeObject.bBoard.GetValueAsBool("IsDead"))
            {
                bb.SetValueAsGameObject("targetObj", null);
                return State.Failed;
            }
            float dmg = bb.GetValueAsFloat("Damage");

            owner_comp.gameObject.GetComponent<Units>().PlayAttackAnimation();
            GameManager.Battle.GenerateHit(owner_comp.gameObject, targetObj.gameObject, dmg);
            return State.Succeeded;
        }
    }

}
