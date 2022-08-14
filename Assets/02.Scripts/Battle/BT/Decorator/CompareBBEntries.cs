using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public enum EKey_Comp
    {
        Is_Set,
        Is_Not_Set
    }
    public class CompareBBEntries : DecoratorNode
    {
        [HideInInspector] public int condition = 0;
        [HideInInspector] public string[] s_condition = new string[] { "Is Equal", "Is Not Equal" };
        [HideInInspector] public int keyIdx1 = 0;
        [HideInInspector] public int keyIdx2 = 0;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            // Is Equal
            if (condition == 0)
            {
                var key1 = bBoard.bb_keys[keyIdx1];
                var key2 = bBoard.bb_keys[keyIdx2];
                return key1 == key2 ? State.Succeeded : State.Failed;
            }
            //Is Not Equal
            else
            {
                var key1 = bBoard.bb_keys[keyIdx1];
                var key2 = bBoard.bb_keys[keyIdx2];
                return key1 == key2 ? State.Failed : State.Succeeded;
            }
        }
    }
}



