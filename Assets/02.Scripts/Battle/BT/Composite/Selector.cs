using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Selector : CompositeNode
    {
        int current = 0;
        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            Node child = Childs[current];
            switch (child.UpdateNode(owner_comp))
            {
                // InProgress 시 Succeeded Or Failed 반환 전까지 Current 유지 및 InProgress반환
                case State.InProgress:
                    return State.InProgress;
                // 자식 Fail 시 다음 자식 실행
                case State.Failed:
                    current++;
                    break;
                // 다음 Update 시 다음 자식 실행.
                case State.Succeeded:
                    return State.Succeeded;
            }

            return current == Childs.Count ? State.Failed : State.InProgress;
        }
    }
}

