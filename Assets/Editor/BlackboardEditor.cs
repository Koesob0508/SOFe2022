using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;

public class BlackboardEditor : EditorWindow
{
    BlackboardView boardView;
    [MenuItem("BehaviorTreeEditor/BlackBoardEditor")]
    public static void OpenWindow()
    {
        BlackboardEditor wnd = GetWindow<BlackboardEditor>();
        wnd.titleContent = new GUIContent("BlackboardEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        //현재 열린 Asset이 BehaviorTree라면
        if (Selection.activeObject is Blackboard)
        {
            //BT Editor Open
            OpenWindow();
            return true;
        }
        else
            return false;
    }
   
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BlackboardEditor.uxml");
        visualTree.CloneTree(root);

        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BlackboardEditor.uss");
        root.styleSheets.Add(styleSheet);

        // 현재 Window에 존재하는 TreeView, InspectorView Query
        boardView = root.Q<BlackboardView>();
        OnSelectionChange();
    }


    private void OnSelectionChange()
    {
        Blackboard board = Selection.activeObject as Blackboard;
        if (Application.isPlaying)
        {
            if (board)
            {
                boardView.Populateboard(board);
            }
        }
        else
        {
            if (board)
            {
                boardView.Populateboard(board);
            }
        }
    }
}
