using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
public class BattleLogPanel : MonoBehaviour
{
    public uint curLogCount = 0;
    public uint maxLogCount = 5;

    public float logRemainTime = 2.0f;
    public float spacing = 5f;

    public GameObject logPrefab;
    float ancBetween = 0.0f;

    Queue<Tuple<Character, float, Character>> logQueue = new Queue<Tuple<Character, float, Character>>();
    /// <summary>
    /// Save the anchor vector to according index (ancMin.y to x, ancMax.y to y)
    /// </summary>
    public List<Vector2> AnchorVecList;
    List<GameObject> ActiveLog;
    IObjectPool<BattleLog> LogPool;

    public void Start()
    {
        ActiveLog = new List<GameObject>();
        AnchorVecList = new List<Vector2>();

        LogPool = new ObjectPool<BattleLog>(
            CreateLog,
            OnGet,
            OnRelease,
            OnDestroyLog,
            maxSize: (int)maxLogCount
            );

        ancBetween = 1.0f / maxLogCount;

        // vector2(ancmin.y, ancmax.y)
        for(int i = 0; i < maxLogCount; i++)
        {
            float anc = 1.0f - ancBetween * i;
            AnchorVecList.Add(new Vector2(anc - ancBetween, anc));
        }
    }

    private void Update()
    {
        if (logQueue.Count > 0 && curLogCount < maxLogCount)
            PublishLog();
    }
    void PublishLog()
    {
        BattleLog log = LogPool.Get();
        log.ConstructWithInfo(logQueue.Dequeue());
        log.SetLifeTime(logRemainTime);
        log.SetAnchorMoveOffset(ancBetween);
        log.AnchorMoveTo(AnchorVecList[(int)curLogCount++]);
    }
    /// <summary>
    /// Add Log to Panel
    /// </summary>
    /// <param name="logData"> 1. Causer, 2. Damage 3. Target </param>
    public void AddLog(Tuple<Character, float, Character> log)
    {
        logQueue.Enqueue(log);
    }

    BattleLog CreateLog()
    {
        GameObject g = GameObject.Instantiate(logPrefab,this.gameObject.transform);
        g.GetComponent<BattleLog>().SetPool(LogPool);
        g.SetActive(false);
        return g.GetComponent<BattleLog>();
    }
    void OnGet(BattleLog log)
    {
        log.gameObject.SetActive(true);
        ActiveLog.Add(log.gameObject);
    }
    void OnRelease(BattleLog log)
    {
        log.gameObject.SetActive(false);
        ActiveLog.Remove(log.gameObject);
        curLogCount--;
        foreach(var alog in ActiveLog)
        {
            alog.GetComponent<BattleLog>().AnchorMoveUp();
        }
    }
    void OnDestroyLog(BattleLog log)
    {
        Destroy(log.gameObject);
    }
}

   