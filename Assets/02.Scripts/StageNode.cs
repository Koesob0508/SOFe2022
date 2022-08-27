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
    public Vector2 position { get; private set; }
    public bool isCompleted { get; private set; }
    public List<Enemy> enemies { get; private set; }
    public Button button;

    public void Init(StageType _type, Vector2 _position, bool _isCompleted)
    {
        type = _type;
        position = _position;
        isCompleted = _isCompleted;

        if (_type == StageType.Battle)
        {
            enemies = new List<Enemy>();

            Enemy e1 = (Enemy)GameManager.Instance.ObjectCodex[100];
            Enemy e2 = (Enemy)GameManager.Instance.ObjectCodex[100];
            Enemy e3 = (Enemy)GameManager.Instance.ObjectCodex[100];

            e1.Position = new Vector2(3, 5);
            e2.Position = new Vector2(0, 8);
            e3.Position = new Vector2(3, -5);

            enemies.Add(e1);
            enemies.Add(e2);
            enemies.Add(e3);
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
}


