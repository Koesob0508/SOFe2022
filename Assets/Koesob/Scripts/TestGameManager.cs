using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public RelationshipManager relation;

    private void Start()
    {
        Debug.Log("Test Game Manager Start");

        relation.Init();
    }
}
