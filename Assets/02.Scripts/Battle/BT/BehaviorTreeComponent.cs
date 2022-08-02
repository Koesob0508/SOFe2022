using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeComponent : MonoBehaviour
{
    public BT.BehaviorTree TreeObject;
    bool isInitalized = false;
    protected void Start()
    {
        TreeObject = TreeObject.Clone();
    }
    protected void Update()
    {
        if(isInitalized)
            TreeObject.UpdateTree(this);
    }
    public void Initalize()
    {
        isInitalized = true;
    }
    public Blackboard GetBlackBoardComponent()
    {
        if (isInitalized)
            return TreeObject.bBoard;
        else
            return null;
    }
}
