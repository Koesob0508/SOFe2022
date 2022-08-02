using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private static StageManager stage = null;
    private BattleManager bManager = null;

    private void Awake()
    {
        // 教臂沛 备泅何
        {
            if (instance == null)
            {
                instance = this;
                SceneManager.sceneLoaded += OnSceneLoaded;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public static GameManager Instance
    {
        // 教臂沛 备泅何
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleSelectScene")
        {
            bManager = new BattleManager();
        }
    }
}
