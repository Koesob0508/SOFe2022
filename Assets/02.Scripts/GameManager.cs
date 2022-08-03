using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public  static GameManager Instance { get { Init(); return instance; } }

    // 다음과 같이 만드시면 됩니다.
    #region Core
    [SerializeField] private StageManager _stage = new StageManager();

    public static StageManager Stage { get { return Instance._stage; } }

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

            // 여기서부터 각자의 Manager를 Init하면 됩니다.
            Stage.Init();
        }
    }
}
