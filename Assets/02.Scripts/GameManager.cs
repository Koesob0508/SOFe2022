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

    [SerializeField] private HeroManager _hero = new HeroManager();
    public static HeroManager Hero { get { return Instance._hero; } }

    #endregion

    public List<Character> characters = new List<Character>();


    private void Awake()
    {
        ImportCharData();
    }

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

    void ImportCharData()
    {
        List<Character> chars_tmp = new List<Character>();
        CSVImporter csvImp = new CSVImporter();
        csvImp.OpenFile("Data/Heros_values");
        csvImp.ReadHeader();
        string line = csvImp.Readline();

        while (line != null)
        {
            string[] elems = line.Split(',');

            Hero hero = new Hero();
            hero.GUID = uint.Parse(elems[0]);
            hero.Name = elems[1];
            hero.MaxHP = float.Parse(elems[2]);
            hero.AttackDamage = float.Parse(elems[3]);
            hero.AttackSpeed = float.Parse(elems[4]);
            hero.DefensePoint = float.Parse(elems[5]);
            hero.MaxMana = float.Parse(elems[6]);
            hero.MoveSpeed = float.Parse(elems[7]);
            hero.AttackRange = float.Parse(elems[8]);
            line = csvImp.Readline();

            chars_tmp.Add(hero);
        }

        CSVImporter csvImp1 = new CSVImporter();
        csvImp1.OpenFile("Data/Monsters_values");
        csvImp1.ReadHeader();
        string line1 = csvImp1.Readline();

        while (line1 != null)
        {
            string[] elems = line1.Split(',');

            Enemy enemy = new Enemy();
            enemy.GUID = uint.Parse(elems[0]);
            enemy.Name = elems[1];
            enemy.MaxHP = float.Parse(elems[2]);
            enemy.AttackDamage = float.Parse(elems[3]);
            enemy.AttackSpeed = float.Parse(elems[4]);
            enemy.DefensePoint = float.Parse(elems[5]);
            enemy.MaxMana = float.Parse(elems[6]);
            enemy.MoveSpeed = float.Parse(elems[7]);
            enemy.AttackRange = float.Parse(elems[8]);
            line1 = csvImp1.Readline();

            chars_tmp.Add(enemy);
        }
        characters = chars_tmp;
    }
}
