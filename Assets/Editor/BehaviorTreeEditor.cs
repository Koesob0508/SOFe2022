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

    // Asset Open �� Callback �Ǵ� �Լ�
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        //���� ���� Asset�� BehaviorTree���
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
        
        // ���� Window�� �����ϴ� TreeView, InspectorView Query
        treeView = root.Q<BehaviorTreeView>();
        inspectorView = root.Q<InspectorView>();
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    // ���� �����ϰ� �ִ� BT ScriptableObject�� �ٸ� ������Ʈ�� �ٲ���� ��.
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

    //1�ʿ� 10�� Update�� ���� ���, �ٸ��ǹ� x  
    private void OnInspectorUpdate()
    {
        treeView.UpdateNodeState();
    }
}