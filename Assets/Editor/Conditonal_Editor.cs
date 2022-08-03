using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(BT.Blackboard_Conditional))]
public class Conditonal_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BT.Blackboard_Conditional script = (BT.Blackboard_Conditional)target;

        GUIContent condition_Label = new GUIContent("Key Query");
        script.condition = EditorGUILayout.Popup(condition_Label, script.condition, script.s_condition);


        GUIContent keyLabel = new GUIContent("Key");
        List<string> keyarr = new List<string>();
        foreach (var key in script.bBoard.bb_keys)
        {
            keyarr.Add(key.Name);
        }
        script.keyIdx = EditorGUILayout.Popup(keyLabel, script.keyIdx, keyarr.ToArray());
    }
}
