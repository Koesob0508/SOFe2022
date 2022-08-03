using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

public class BehaviorTreeEditor : EditorWindow
{
    BehaviorTreeView treeView;
    InspectorView inspectorView;

    [MenuItem("BehaviorTreeEditor/Editor")]
    public static void OpenWindow()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditor");
    }

    // Asset Open 시 Callback 되는 함수
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        //현재 열린 Asset이 BehaviorTree라면
        if (Selection.activeObject is BT.BehaviorTree)
        {
            //BT Editor Open
            OpenWindow();
            return true;
        }
        else
            return false;
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch (obj)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }

    }


    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(root);

        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor.uss");
        root.styleSheets.Add(styleSheet);
        
        // 현재 Window에 존재하는 TreeView, InspectorView Query
        treeView = root.Q<BehaviorTreeView>();
        inspectorView = root.Q<InspectorView>();
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    // 현재 선택하고 있는 BT ScriptableObject를 다른 오브젝트로 바뀌었을 때.
    private void OnSelectionChange()
    {
        BT.BehaviorTree tree = Selection.activeObject as BT.BehaviorTree;
        if(!tree)
        {
            if(Selection.activeGameObject)
            {
                BehaviorTreeComponent bComp = Selection.activeGameObject.GetComponent<BehaviorTreeComponent>();
                if (bComp)
                    tree = bComp.TreeObject;
            }
        }
        if(Application.isPlaying)
        {
            if(tree)
            {
                treeView.PopulateView(tree);
            }
        }
        else
        {
            if (tree && AssetDatabase.OpenAsset(tree.GetInstanceID()))
            {
                treeView.PopulateView(tree);
            }
        }
    }

    void OnNodeSelectionChanged(NodeView nodeView)
    {
        inspectorView.Update(nodeView);
    }

    //1초에 10번 Update를 위해 사용, 다른의미 x  
    private void OnInspectorUpdate()
    {
        treeView.UpdateNodeState();
    }
}