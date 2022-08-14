using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(BT.CompareBBEntries))]
public class Compare_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BT.CompareBBEntries script = (BT.CompareBBEntries)target;

        GUIContent condition_Label = new GUIContent("Operator");
        script.condition = EditorGUILayout.Popup(condition_Label, script.condition, script.s_condition);


        GUIContent keyLabel = new GUIContent("Key1");
        GUIContent keyLabel2 = new GUIContent("Key2");
        List<string> keyarr = new List<string>();
        foreach (var key in script.bBoard.bb_keys)
        {
            keyarr.Add(key.Name);
        }
        script.keyIdx1 = EditorGUILayout.Popup(keyLabel, script.keyIdx1, keyarr.ToArray());
        script.keyIdx2 = EditorGUILayout.Popup(keyLabel2, script.keyIdx2, keyarr.ToArray());
    }
}
