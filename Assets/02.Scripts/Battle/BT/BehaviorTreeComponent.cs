using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeComponent : MonoBehaviour
{
    public BT.BehaviorTree TreeObject;
    bool isActive = false;
    protected void Update()
    {
        if(isActive)
            TreeObject.UpdateTree(this);
    }
    public void Initalize()
    {
        TreeObject = TreeObject.Clone();
    }
    public void StartTree()
    {
        isActive = true;

    }
    public void StopTree()
    {
        isActive = false;
    }
    public Blackboard GetBlackBoardComponent()
    {
        if (isActive)
            return TreeObject.bBoard;
        else
            return null;
    }
}
