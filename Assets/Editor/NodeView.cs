using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public BT.Node node;
    public Port input;
    public Port output;
    public NodeView (BT.Node node) : base("Assets/Editor/NodeView.uxml")
    {
        
        this.node = node;
        this.title = node.name;
        viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        if (node is BT.TaskNode)
        {
            AddToClassList("task");
        }
        else if (node is BT.CompositeNode)
        {
            AddToClassList("composite");
        }
        else if (node is BT.DecoratorNode)
        {
            AddToClassList("decorator");
        }
        else if (node is BT.ServiceNode)
        {
            AddToClassList("service");
        }
        else
        {
            AddToClassList("root");
        }


        CreateInputPorts();
        CreateOutputPorts();
    }

    
    private void CreateInputPorts()
    {
        if(node is BT.TaskNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is BT.CompositeNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is BT.DecoratorNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if(node is BT.ServiceNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }

        if(input != null)
        {
            input.portName = "";
            input.style.flexDirection = UnityEngine.UIElements.FlexDirection.Column;
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        if (node is BT.TaskNode)
        { }
        else if (node is BT.RootNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if (node is BT.CompositeNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is BT.DecoratorNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if (node is BT.ServiceNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        if (output != null)
        {
            output.portName = "";
            output.style.flexDirection = UnityEngine.UIElements.FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }
    }

   
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behavior Tree (Set Position)");
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if(OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }

    public void SortChilds()
    {
        BT.CompositeNode CompNode = node as BT.CompositeNode;
        if (CompNode)
        {
            CompNode.Childs.Sort(SortByX);
        }
    }

    private int SortByX(BT.Node lhs, BT.Node rhs)
    {
        return lhs.position.x < rhs.position.x ? -1 : 1;
    }

    public void UpdateState()
    {
        RemoveFromClassList("inprogress");
        RemoveFromClassList("succeeded");
        RemoveFromClassList("failed");
        RemoveFromClassList("aborted");
        if(Application.isPlaying)
        {
            switch (node.state)
            {
                case BT.Node.State.InProgress:
                    if(node.started)
                        AddToClassList("inprogress");   
                    break;
                case BT.Node.State.Succeeded:
                    AddToClassList("succeeded");
                    break;
                case BT.Node.State.Failed:
                    AddToClassList("failed");
                    break;
                case BT.Node.State.Aborted:
                    AddToClassList("aborted");
                    break;
            }
        }

    }
}