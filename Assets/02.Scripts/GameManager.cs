using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private static StageManager stage = null;

    private void Awake()
    {
        // �̱��� ������
        {
            if (instance == null)
            {
                instance = this;
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
        // �̱��� ������
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

}
