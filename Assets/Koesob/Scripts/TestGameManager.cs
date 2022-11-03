using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
{
    public RelationshipManager relation;
    public TextMeshProUGUI log;
    public TextMeshProUGUI eventLog;
    public Hero heroA;
    public Hero heroB;
    public Enemy enemyA;
    public Enemy enemyB;


    private void Start()
    {
        Debug.Log("Test Game Manager Start");

        relation.Init();

        heroA = new Hero();
        heroB = new Hero();
        enemyA = new Enemy();
        enemyB = new Enemy();
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            log.text = "Hero A가 Enemy A 공격";

            LogInfo logInfo = new LogInfo();
            logInfo.Subjective = heroA;
            logInfo.Objective = enemyA;
            logInfo.Verb = Verb.Attack;

            relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            log.text = "Hero A가 Enemy B 공격";

            LogInfo logInfo = new LogInfo();
            logInfo.Subjective = heroA;
            logInfo.Objective = enemyB;
            logInfo.Verb = Verb.Attack;

            relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            log.text = "Hero B가 Enemy A 공격";

            LogInfo logInfo = new LogInfo();
            logInfo.Subjective = heroB;
            logInfo.Objective = enemyA;
            logInfo.Verb = Verb.Attack;

            relation.ApplyLog(logInfo);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            log.text = "Hero B가 Enemy B 공격";

            LogInfo logInfo = new LogInfo();
            logInfo.Subjective = heroB;
            logInfo.Objective = enemyB;
            logInfo.Verb = Verb.Attack;

            relation.ApplyLog(logInfo);
        }

    }
}
