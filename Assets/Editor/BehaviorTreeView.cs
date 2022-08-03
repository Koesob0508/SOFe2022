using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class BehaviorTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
     
    // xmlfactory 에 TreeView 등록
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }
    BT.BehaviorTree bTree;
   public BehaviorTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());


        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(bTree);
        AssetDatabase.SaveAssets();
    }

    NodeView FindNodeView(BT.Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    //현재 선택된 bTree를 기준으로 View 생성
    internal void PopulateView(BT.BehaviorTree bTree)
    {
        this.bTree = bTree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements.ToList());
        graphViewChanged += OnGraphViewChanged;

        if(bTree.RootNode == null)
        {
            bTree.RootNode = bTree.CreateNode(typeof(BT.RootNode)) as BT.RootNode;
            EditorUtility.SetDirty(bTree);
            AssetDatabase.SaveAssets();
        }
        
        bTree.nodes.ForEach(n => CreateNodeView(n));

        bTree.nodes.ForEach(n =>
        {
            List<BT.Node> childs = bTree.GetChilds(n);
            childs.ForEach(c =>
            {
                NodeView ParentView = FindNodeView(n);
                NodeView ChildView = FindNodeView(c);

                Edge e = ParentView.output.ConnectTo(ChildView.input);
                AddElement(e);
            });
        });
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    bTree.DeleteNode(nodeView.node);
                }
                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView Parent = edge.output.node as NodeView;
                    NodeView Child = edge.input.node as NodeView;
                    bTree.RemoveChild(Parent.node, Child.node);
                }

            });
        }

        if(graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                bTree.AddChild(parentView.node, childView.node);
            });
        }

        if(graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) => {
                NodeView nv = n as NodeView;
                nv.SortChilds();
             });
        }
        return graphViewChange;
    }


    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => 
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // TaskNode에서 Derived 된 Type들을 알아온다.
        TypeCache.TypeCollection TaskTypes = TypeCache.GetTypesDerivedFrom<BT.TaskNode>();
        foreach(var type in TaskTypes)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (t) => CreateNode(type));
        }

        TypeCache.TypeCollection CompositeTypes = TypeCache.GetTypesDerivedFrom<BT.CompositeNode>();

        foreach (var type in CompositeTypes)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (t) => CreateNode(type));
        }

        TypeCache.TypeCollection DecoratorTypes = TypeCache.GetTypesDerivedFrom<BT.DecoratorNode>();

        foreach (var type in DecoratorTypes)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (t) => CreateNode(type));
        }

        TypeCache.TypeCollection ServiceTypes = TypeCache.GetTypesDerivedFrom<BT.ServiceNode>();

        foreach (var type in ServiceTypes)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (t) => CreateNode(type));
        }
    }

    void CreateNode(System.Type type)
    {
        BT.Node node = bTree.CreateNode(type);
        CreateNodeView(node);
    }

    void CreateNodeView(BT.Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeState()
    {
        nodes.ForEach((n) =>
        {
            NodeView nv = n as NodeView;
            nv.UpdateState();
        });
    }
}
