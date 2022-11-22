using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
public class BattleLogPanel : MonoBehaviour
{

    public enum LogType
    {
        Kill,
        Dead,
        Skill,
        Event,
        Buff,
        Positive,
        Negative
    }

    public struct Log
    {
        public Character Causer;
        public Character Target;
        public LogType Type;
        public GameObject CauserObject;
        public GameObject TargetObject;
        public Log(Character Causer, Character Target, LogType type)
        {
            this.Causer = Causer;
            this.Target = Target;
            Type = type;
            this.CauserObject = null;
            this.TargetObject = null;
        }

        public Log(Character Causer, Character Target, LogType type, GameObject _causerobject, GameObject _targetObject)
        {
            this.Causer = Causer;
            this.Target = Target;
            Type = type;
            this.CauserObject = _causerobject;
            this.TargetObject = _targetObject;
        }
    }

    public uint curLogCount = 0;
    public uint maxLogCount = 5;

    public float logRemainTime = 1.0f;
    public float spacing = 5f;

    public GameObject logPrefab;
    float ancBetween = 0.0f;

    Queue<Log> logQueue = new Queue<Log>();
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
            OnDestroyLog
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
        log.name = log.name + curLogCount;
        log.ConstructWithInfo(logQueue.Dequeue());
        log.SetLifeTime(logRemainTime);
        log.SetAnchorMoveOffset(ancBetween);
        log.Setnum((int)curLogCount);
        log.AnchorMoveTo(AnchorVecList[(int)curLogCount++]);
    }
    /// <summary>
    /// Add Log to Panel
    /// </summary>
    /// <param name="logData"> 1. Causer, 2. Damage 3. Target </param>
    public void AddLog(Log log)
    {
        logQueue.Enqueue(log);
        GameManager.Relation.ApplyLog(log);
    }

    BattleLog CreateLog()
    {
        GameObject g = Instantiate(logPrefab,this.gameObject.transform);
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
        curLogCount--;
        ActiveLog.Remove(log.gameObject);
        log.gameObject.SetActive(false);
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

   