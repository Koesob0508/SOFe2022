using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlackboardKeyTypes : ScriptableObject
{
    public string Name;
    public BT_Key.KeyType Type;
    public object Value;

    public BlackboardKeyTypes Clone()
    {
        return Instantiate(this);
    }
    public void SetValueAsDefult()
    {
        switch (Type)
        {
            case BT_Key.KeyType.E_bool:
                Value = false;
                break;
            case BT_Key.KeyType.E_int:
                Value = 0;
                break;
            case BT_Key.KeyType.E_float:
                Value = 0.0f;
                break;
            case BT_Key.KeyType.E_vector2:
                Value = Vector2.zero;
                break;
            case BT_Key.KeyType.E_gameobject:
                Value = null;
                break;
        }
    }
}
