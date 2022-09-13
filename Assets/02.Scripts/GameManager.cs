using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Enum Types
    public enum MbtiType
    {
        INFP,
        ENFP,
        INFJ,
        ENFJ,
        INTJ,
        ENTJ,
        INTP,
        ENTP,
        ISFP,
        ESFP,
        ISTP,
        ESTP,
        ISFJ,
        ESFJ,
        ISTJ,
        ESTJ
    }

    public enum MapType
    {
        Jungle,
        Dessert,
        Boss
    }

    public enum ObjectType
    {
        Hero,
        Enemy,
        Item
    }
    #endregion

    private static GameManager instance;
    private static string game_version;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    #region Managers

    [SerializeField] private DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    [SerializeField] private StageManager _stage;
    public static StageManager Stage { get { return Instance._stage; } }

    [SerializeField] private CustomSceneManager _scene = new CustomSceneManager();
    public static CustomSceneManager Scene { get { return Instance._scene; } }

    [SerializeField] private HeroManager _hero; // HeroManager가 MonoBehaviour를 상속 받고 있기 때문에 Scene에서 직접 할당 필요
    public static HeroManager Hero { get { return Instance._hero; } }

    [SerializeField] private RelationshipManager _relation;
    public static RelationshipManager Relation { get { return Instance._relation; } }

    public static BattleSceneManager Battle = null; // Battle 돌입 때마다 새로 할당

    #endregion 

    private void Start()
    {
        Init();
        Test();
    }

    private void Init()
    {
        if (instance == null)
        {
            #region Initialize GameManager

            GameObject obj = GameObject.Find("Game Manager");

            if (obj == null)
            {
                obj = new GameObject { name = "Game Manager" };
                obj.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<GameManager>();

            #endregion
            Data.Init();
            Stage.Init();
            Scene.Init();
            Hero.Init();
            Relation.Init();
            game_version = Application.version;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Test()
    {
        Debug.Log("테스트를 진행합니다.");
        Hero.Test();
        // 이 밑으로 진행할 Test 코드를 입력한 후, Start 함수에 가서 Test의 주석 처리를 해제하면 됩니다. 
        Data.Save();
    }

    public string GetVersion()
    {
        return game_version;
    }

    public void TestFunc()
    {
        Scene.ToBattleScene();
    }

    
    
}
