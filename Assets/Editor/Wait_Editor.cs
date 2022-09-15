using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(BT.Wait))]
public class Wait_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BT.Wait script = (BT.Wait)target;

        GUIContent keyLabel = new GUIContent("Key");
        List<string> keyarr = new List<string>();
        foreach (var key in script.bBoard.bb_keys)
        {
            if (key.Type == BT_Key.KeyType.E_float)
            {
                keyarr.Add(key.Name);
            }
        }
        script.keyIdx = EditorGUILayout.Popup(keyLabel, script.keyIdx, keyarr.ToArray());
        script.keyName = (keyarr.ToArray())[script.keyIdx];
    }
}
