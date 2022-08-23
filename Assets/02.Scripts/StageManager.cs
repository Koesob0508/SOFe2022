using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private StageNode currentStageNode;
    public List<StageNode> stageNodes;
    private GameObject Stages;

    // 현재는 GameManager에 StageManager가 할당되어 있어야 한다.
    public void Init()
    {
        DontDestroyOnLoad(this);

        stageNodes = new List<StageNode>();

        Stages = GameObject.Find("Stages");

        if(Stages == null)
        {
            Stages = new GameObject { name = "Stages" };
        }

        Stages.transform.position = Vector3.zero;

        DontDestroyOnLoad(Stages);

        InitStageMap(); // 이거 나중에 큰일날거 같은 느낌이 들어...

        Debug.Log("Stage Manager Init");
    }

    public void InitStageMap()
    {
        Debug.Log("Stage Node Instantiate");

        Test_SetTempStage();
    }

    public void ShowStageMap()
    {
        foreach (StageNode stageNode in stageNodes)
        {
            stageNode.gameObject.SetActive(true);
        }
    }

    public void HideStageMap()
    {
        foreach (StageNode stageNode in stageNodes)
        {
            stageNode.gameObject.SetActive(false);
        }
    }
    
    public List<Enemy> GetEnemies()
    {
        if(currentStageNode.type == StageNode.StageType.Battle)
        {
            return currentStageNode.enemies;
        }

        Debug.Log("This Stage is not Battle Stage");
        return null;
    }

    public void Test_SetTempStage()
    {
        GameObject BattleStage = GameObject.Find("Battle Stage");

        if (BattleStage == null)
        {
            BattleStage = new GameObject { name = "Battle Stage" };
            BattleStage.AddComponent<StageNode>();
        }

        DontDestroyOnLoad(BattleStage);
        BattleStage.transform.parent = Stages.transform;
        BattleStage.transform.position = new Vector2(-5, -3);
        BattleStage.SetActive(false);
        BattleStage.GetComponent<StageNode>().Init(StageNode.StageType.Battle, new Vector2(-5, -3), false);

        stageNodes.Add(BattleStage.GetComponent<StageNode>());

        GameObject TownStage = GameObject.Find("Town Stage");

        if (TownStage == null)
        {
            TownStage = new GameObject { name = "Town Stage" };
            TownStage.AddComponent<StageNode>();
        }

        DontDestroyOnLoad(TownStage);
        TownStage.GetComponent<StageNode>().Init(StageNode.StageType.Town, new Vector2(0, -3), false);
        TownStage.transform.parent = Stages.transform;
        TownStage.transform.position = new Vector2(0, -3);
        TownStage.SetActive(false);

        stageNodes.Add(TownStage.GetComponent<StageNode>());

        GameObject EventStage = GameObject.Find("Event Stage");

        if (EventStage == null)
        {
            EventStage = new GameObject { name = "Event Stage" };
            EventStage.AddComponent<StageNode>();
        }

        DontDestroyOnLoad(EventStage);
        EventStage.GetComponent<StageNode>().Init(StageNode.StageType.Event, new Vector2(5, -3), false);
        EventStage.transform.parent = Stages.transform;
        EventStage.transform.position = new Vector2(5, -3);
        EventStage.SetActive(false);

        stageNodes.Add(EventStage.GetComponent<StageNode>());
    }

    public void Test_SetBattleStage()
    {
        currentStageNode = stageNodes[0];

        Debug.Log("Current Stage is Battle Stage");
    }

}
