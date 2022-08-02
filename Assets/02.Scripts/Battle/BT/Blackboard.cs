using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BT_Key
{
    public enum KeyType
    {
        E_bool,
        E_int,
        E_float,
        E_vector2,
        E_gameobject
    }
};
[CreateAssetMenu()]
public class Blackboard : ScriptableObject
{
    public List<BlackboardKeyTypes> bb_keys = new List<BlackboardKeyTypes>();
    
   
    public Blackboard Clone()
    {
        Blackboard bBoard = Instantiate(this);
        bBoard.bb_keys.Clear();
        foreach (var keys in bb_keys)
        {
            var key = keys.Clone();
            key.SetValueAsDefult();
            bBoard.bb_keys.Add(key);
        }
        return bBoard;
    }

    public bool AddKeyValue(string name, BT_Key.KeyType type)
    {
        foreach (var obj in bb_keys)
            if (obj.Name == name)
                return false;

        BlackboardKeyTypes key = CreateInstance<BlackboardKeyTypes>();
        key.Name = name;
        key.Type = type;

        switch (type)
        {
            case BT_Key.KeyType.E_bool:
                {
                    key.Value = false;
                    AssetDatabase.SaveAssets();
                }
                break;
            case BT_Key.KeyType.E_int:
                {
                    key.Value = 0;
                }
                break;
            case BT_Key.KeyType.E_float:
                {
                    key.Value = 0.0f;
                }
                break;
            case BT_Key.KeyType.E_vector2:
                {
                    key.Value = new Vector2(0,0);
                }
                break;
            case BT_Key.KeyType.E_gameobject:
                {
                    key.Value = new GameObject();
                }
                    break;
        }

        bb_keys.Add(key);
        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(key, this);
        }
        AssetDatabase.SaveAssets();
        return true;
    }
    public void DeleteKey(string name)
    {
        var elem = bb_keys.Find(n => n.Name == name);
        bb_keys.Remove(elem);
        DestroyImmediate(elem,true);
        AssetDatabase.SaveAssets();
    }
    public bool GetValueAsBool(string str)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            return (bool)temp.Value;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }

    public void SetValueAsBool(string str, bool val)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            temp.Value = val;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }

    public GameObject GetValueAsGameObject(string str)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            return (GameObject)temp.Value;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }

    public void SetValueAsGameObject(string str, GameObject obj)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            temp.Value = obj;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }

    public Vector2 GetValueAsVector2(string str)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            return (Vector2)temp.Value;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }
    public void SetValueAsVector2(string str, Vector2 val)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            temp.Value = val;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }

    public float GetValueAsFloat(string str)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            return (float)temp.Value;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");

    }
    public void SetValueAsFloat(string str, float val)
    {
         var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            temp.Value = val;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }
    public float GetValueAsInt(string str)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            return (int)temp.Value;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");

    }
    public void SetValueAsInt(string str, int val)
    {
        var temp = bb_keys.Find(n => n.Name == str);
        if (temp)
            temp.Value = val;
        else
            throw new System.Exception("Blackboard_Key doesnt exists");
    }
}
