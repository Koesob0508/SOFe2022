using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogPanel : MonoBehaviour
{
    uint curLogCount = 0;
    public uint maxLogCount = 5;
    public float logRemainTime = 0.5f;

    public GameObject logPrefab;

    List<GameObject> logPool = new List<GameObject>();
    uint PoolMax = 5;
    uint curPoolCount = 0;
    public void AddLog(uint causerID, uint targetID, float dmg)
    {
        
    }
}
