using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BT
{
    public enum EKey_Query
    {
        Is_Set,
        Is_Not_Set
    }
    public class Blackboard_Conditional : DecoratorNode
    {

        [HideInInspector]public int condition = 0;
        [HideInInspector] public string[] s_condition = new string[] { "Is Set", "Is Not Set" };
        [HideInInspector] public int keyIdx = 0;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate(BehaviorTreeComponent owner_comp)
        {
            var key = bBoard.bb_keys[keyIdx];
            //isset
            if(condition == 0)
            {
                switch (key.Type)
                {
                    case BT_Key.KeyType.E_bool:
                        if ((bool)key.Value)
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }    
                        else return State.Failed;
                    case BT_Key.KeyType.E_int:
                        if ((int)key.Value != 0)
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                        else return State.Failed;
                    case BT_Key.KeyType.E_float:
                        if ((float)key.Value != 0.0f)
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                        else return State.Failed;
                    case BT_Key.KeyType.E_vector2:
                        if ((Vector2)key.Value != Vector2.zero)
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                        else return State.Failed;
                    case BT_Key.KeyType.E_gameobject:
                        if ((GameObject)key.Value != null)
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                        else return State.Failed;
                    default:
                        return State.Succeeded;
                }

            }
            //isnotset
            else
            {
                switch (key.Type)
                {
                    case BT_Key.KeyType.E_bool:
                        if ((bool)key.Value)
                            return State.Failed;
                        else
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                    case BT_Key.KeyType.E_int:
                        if ((int)key.Value != 0)
                            return State.Failed;
                        else
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                    case BT_Key.KeyType.E_float:
                        if ((float)key.Value != 0.0f)
                            return State.Failed;
                        else
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                    case BT_Key.KeyType.E_vector2:
                        if ((Vector2)key.Value != Vector2.zero)
                            return State.Failed;
                        else
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                    case BT_Key.KeyType.E_gameobject:
                        if ((GameObject)key.Value != null)
                            return State.Failed;
                        else
                        {
                            Child.UpdateNode(owner_comp);
                            return State.InProgress;
                        }
                    default:
                        return State.InProgress;
                }
            }
        }
    }

}

