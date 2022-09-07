using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageNode : MonoBehaviour
{
    public enum StageType
    {
        Battle = 0,
        Town,
        Event
    }

    public StageType type { get; private set; }
    private int step;
    public bool isMerged;
    private bool isCompleted;
    private bool isPassPoint;
    private List<StageNode> nextStages;
    public List<Enemy> enemies { get; private set; }
    public Button button;

    public void Init(StageType _type, int _step)
    {
        type = _type;
        step = _step;
        isMerged = false;
        isCompleted = false;
        isPassPoint = false;
        nextStages = new List<StageNode>();

        if(this.step == 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

        if (_type == StageType.Battle)
        {
            enemies = new List<Enemy>();

            Enemy e1 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy e2 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy e3 = (Enemy)GameManager.Data.ObjectCodex[100];

            e1.Position = new Vector2(3, 5);
            e2.Position = new Vector2(0, 8);
            e3.Position = new Vector2(3, -5);

            enemies.Add(e1);
            enemies.Add(e2);
            // enemies.Add(e3);
        }
        
        switch(type)
        {
            case StageType.Battle:
                button.onClick.AddListener(GameManager.Scene.ToBattleScene);
                break;

            case StageType.Town:
                button.onClick.AddListener(GameManager.Scene.ToTownScene);
                break;

            case StageType.Event:
                button.onClick.AddListener(GameManager.Scene.ToEventScene);
                break;
        }
    }

    public void AddNextStage(StageNode _nextStage)
    {
        if(!this.nextStages.Contains(_nextStage))
        {
            nextStages.Add(_nextStage);
        }
    }

    public void Complete()
    {
        isCompleted = true;
        isPassPoint = true;

        foreach(StageNode stage in nextStages)
        {
            stage.button.interactable = true;
        }
    }
}


