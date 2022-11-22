using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class IsInAttackRange : ServiceNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            GameObject target = owner_comp.TreeObject.bBoard.GetValueAsGameObject("targetObj");
            if(target)
            {
                float Dist = Vector2.Distance(owner_comp.gameObject.transform.position, target.transform.position);
                if(Dist <= owner_comp.gameObject.GetComponent<Units>().charData.AttackRange)
                {
                    owner_comp.TreeObject.bBoard.SetValueAsBool("CanAttack", true);
                    owner_comp.GetComponent<Units>().StopMovement();
                }
                else
                {
                    owner_comp.TreeObject.bBoard.SetValueAsBool("CanAttack", false);
                }
            }
            return Child.UpdateNode(owner_comp);
        }
    }

}
