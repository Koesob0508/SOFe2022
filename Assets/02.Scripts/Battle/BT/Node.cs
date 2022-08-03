using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            InProgress,
            Succeeded,
            Failed,
            Aborted
        }

        [HideInInspector]   public State state = State.InProgress;
        // To distinguish whether this node was begun or not 
        [HideInInspector]   public bool started = false;
        [HideInInspector]   public string guid;
        // 에디터를 껏다 켰을때 위치 유지를 위해... ( NodeView는 Node에 따라 에디터 선택 시 생성되므로 여기 저장.. )
        [HideInInspector]   public Vector2 position;
        public Blackboard bBoard;
        public State UpdateNode(BehaviorTreeComponent owner_comp)
        {
            if(!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate(owner_comp);

            if(state == State.Succeeded || state == State.Failed)
            {
                OnStop();
                started = false;
            }
            return state;
        }    
        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate(BehaviorTreeComponent owner_comp);

    }

}

