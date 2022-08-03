using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public  static GameManager Instance { get { Init(); return instance; } }

    // ������ ���� ����ø� �˴ϴ�.
    #region Core
    [SerializeField] private StageManager _stage = new StageManager();

    public static StageManager Stage { get { return Instance._stage; } }
    public BattleSceneManager Battle = null;

    #endregion



    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject obj = GameObject.Find("Game Manager");

            if(obj == null)
            {
                obj = new GameObject { name = "Game Manager" };
                obj.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<GameManager>();

            // ���⼭���� ������ Manager�� Init�ϸ� �˴ϴ�.
            Stage.Init();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleSelectScene")
        {
            Battle = new BattleSceneManager();
        }
    }


}
