using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
{
    [SerializeField] private RelationshipManager _relation;
    public static RelationshipManager Relation => Instance._relation;
    public TextMeshProUGUI log;
    public TextMeshProUGUI eventLog;
    public Hero heroA;
    public Hero heroB;
    public Enemy enemyA;
    public Enemy enemyB;
    public TextMeshProUGUI heroALog;
    public TextMeshProUGUI heroBLog;
    public GameManager.MbtiType heroAMBTI;
    public GameManager.MbtiType heroBMBTI;

    private static TestGameManager instance;

    public static TestGameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }

            return instance;
        }
    }


    private void Start()
    {
        if(instance == null)
        {
            GameObject obj = GameObject.Find("TestGameManager");

            if(obj == null)
            {
                obj = new GameObject { name = "TestGameManager" };
                obj.AddComponent<TestGameManager>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<TestGameManager>();
        }

        Debug.Log("Test Game Manager Start");

        Relation.Init();

        heroA = new Hero();
        heroA.Name = "A";
        heroA.MBTI = heroAMBTI;
        heroB = new Hero();
        heroB.Name = "B";
        heroB.MBTI = heroBMBTI;
        enemyA = new Enemy();
        enemyA.Name = "C";
        enemyB = new Enemy();
        enemyB.Name = "D";
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            log.text = "Hero A가 Enemy A 공격";

            BattleLogPanel.Log logInfo = new BattleLogPanel.Log();
            logInfo.Causer = heroA;
            logInfo.Target = enemyA;
            logInfo.Type = BattleLogPanel.LogType.Skill;

            Relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            log.text = "Hero A가 Enemy B 공격";

            BattleLogPanel.Log logInfo = new BattleLogPanel.Log();
            logInfo.Causer = heroA;
            logInfo.Target = enemyB;
            logInfo.Type = BattleLogPanel.LogType.Skill;

            Relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            log.text = "Hero B가 Enemy A 공격";

            BattleLogPanel.Log logInfo = new BattleLogPanel.Log();
            logInfo.Causer = heroB;
            logInfo.Target = enemyA;
            logInfo.Type = BattleLogPanel.LogType.Skill;

            Relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            log.text = "Hero B가 Enemy B 공격";

            BattleLogPanel.Log logInfo = new BattleLogPanel.Log();
            logInfo.Causer = heroB;
            logInfo.Target = enemyB;
            logInfo.Type = BattleLogPanel.LogType.Skill;

            Relation.ApplyLog(logInfo);
        }

    }
}
