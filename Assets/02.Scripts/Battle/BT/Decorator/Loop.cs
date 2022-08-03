using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BT
{
    public class Loop : DecoratorNode
    {
        public int NumLoops = 3;
        public bool bInfiniteLoop = false;

        private int LoopCount = 0;
        protected override void OnStart()
        {
            LoopCount = 0;
        }

        protected override void OnStop()
        {
            LoopCount = 0;
            bInfiniteLoop = false;
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            if(bInfiniteLoop)
            {
                Child.UpdateNode(owner_comp);
                return State.InProgress;
            }
            else
            {
                if (LoopCount != NumLoops)
                {
                    Child.UpdateNode(owner_comp);
                    LoopCount++;
                    return State.InProgress;
                }
                else
                    return State.Succeeded;
            }
        }
    }
}
