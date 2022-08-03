using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class RootNode : Node
    {
        public Node Child;
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            return Child.UpdateNode(owner_comp);
        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.Child = Child.Clone();
            return node; 
        }
    }
}
