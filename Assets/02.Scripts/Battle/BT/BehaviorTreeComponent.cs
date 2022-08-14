using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeComponent : MonoBehaviour
{
    public BT.BehaviorTree TreeObject;
    bool isActive = false;
    protected void Start()
    {
        TreeObject = TreeObject.Clone();
    }
    protected void Update()
    {
        if(isActive)
            TreeObject.UpdateTree(this);
    }
    public void Initalize()
    {
        isActive = true;
    }
    public void Terminate()
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
