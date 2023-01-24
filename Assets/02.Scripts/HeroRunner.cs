using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroRunner : MonoBehaviour
{

    public GameObject bg1;
    public GameObject bg1_2;
    public GameObject bg2;
    public GameObject bg2_2;
    public GameObject bg3;
    public GameObject bg3_2;

    public GameObject startPos;
    public GameObject endPos;

    private int length;
    public float ScrollSpeed = 1f;
    void Start()
    {
        length = Screen.width;
        bg1_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width,0);
        bg1_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width-2,0);

        bg2_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);
        bg2_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);

        bg3_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);
        bg3_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);

        float step = Mathf.Abs(endPos.transform.position.x - startPos.transform.position.x) / 4;

        if(GameManager.Data.IsExistSaveData())
        {
            List<uint> activeHero = new List<uint>();
            for (uint i = 0; i < 19; i++)
            {
                Hero h = GameManager.Data.ObjectCodex[i] as Hero;
                if (h.IsActive)
                {
                    activeHero.Add(i);
                    Debug.Log("Hero Name(" + h.Name + ") Added To Active Hero");
                }
            }
            System.Random rand = new System.Random();
            var shuffled = activeHero.OrderBy(_ => rand.Next()).ToList();
            for (int i = 0; i < shuffled.Count; i++)
            {
                GameObject g = Instantiate(Resources.Load<GameObject>("MainHero/" + GameManager.Data.ObjectCodex[shuffled[i]].GUID.ToString()));
                g.transform.position = new Vector3(startPos.transform.position.x + (step * i), startPos.transform.position.y, startPos.transform.position.z);
            }
        }
        else
        {
            List<uint> activeHero = new List<uint>();
            for (uint i = 0; i < 19; i++)
            {
                activeHero.Add(i);
            }
            System.Random rand = new System.Random();
            var shuffled = activeHero.OrderBy(_ => rand.Next()).ToList();
            for (int i = 0; i < 4; i++)
            {
                GameObject g = Instantiate(Resources.Load<GameObject>("MainHero/" + GameManager.Data.ObjectCodex[shuffled[i]].GUID.ToString()));
                g.transform.position = new Vector3(startPos.transform.position.x + (step * i) + 2, startPos.transform.position.y, startPos.transform.position.z);
            }
        }
    }

    void Update()
    {
        Vector2 offsetMax1_1 = bg1.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin1_1 = bg1.GetComponent<RectTransform>().offsetMin;
        Vector2 offsetMax1_2 = bg1_2.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin1_2 = bg1_2.GetComponent<RectTransform>().offsetMin;


        offsetMax1_1.x = offsetMax1_1.x - 0.1f;
        offsetMin1_1.x = offsetMin1_1.x - 0.1f;
        offsetMax1_2.x = offsetMax1_2.x - 0.1f;
        offsetMin1_2.x = offsetMin1_2.x - 0.1f;

        bg1.GetComponent<RectTransform>().offsetMax     = offsetMax1_1;
        bg1.GetComponent<RectTransform>().offsetMin     = offsetMin1_1;
        bg1_2.GetComponent<RectTransform>().offsetMax   = offsetMax1_2;
        bg1_2.GetComponent<RectTransform>().offsetMin = offsetMin1_2;

        if (offsetMax1_1.x <= -Screen.width)
        {
            bg1.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width,0);
            bg1.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width,0);

        }
        if ( offsetMax1_2.x <= -Screen.width)
        {
            bg1_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);
            bg1_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);

        }

        Vector2 offsetMax2_1 = bg2.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin2_1 = bg2.GetComponent<RectTransform>().offsetMin;
        Vector2 offsetMax2_2 = bg2_2.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin2_2 = bg2_2.GetComponent<RectTransform>().offsetMin;


        offsetMax2_1.x = offsetMax2_1.x - 0.5f;
        offsetMin2_1.x = offsetMin2_1.x - 0.5f;
        offsetMax2_2.x = offsetMax2_2.x - 0.5f;
        offsetMin2_2.x = offsetMin2_2.x - 0.5f;

        bg2.GetComponent<RectTransform>().offsetMax = offsetMax2_1;
        bg2.GetComponent<RectTransform>().offsetMin = offsetMin2_1;
        bg2_2.GetComponent<RectTransform>().offsetMax = offsetMax2_2;
        bg2_2.GetComponent<RectTransform>().offsetMin = offsetMin2_2;

        if (offsetMax2_1.x <= -Screen.width)
        {
            bg2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);
            bg2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);
            
        }
        if (offsetMax2_2.x <= -Screen.width)
        {
            bg2_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);
            bg2_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);
        }

        Vector2 offsetMax3_1 = bg3.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin3_1 = bg3.GetComponent<RectTransform>().offsetMin;
        Vector2 offsetMax3_2 = bg3_2.GetComponent<RectTransform>().offsetMax;
        Vector2 offsetMin3_2 = bg3_2.GetComponent<RectTransform>().offsetMin;


        offsetMax3_1.x = offsetMax3_1.x - 1;
        offsetMin3_1.x = offsetMin3_1.x - 1;
        offsetMax3_2.x = offsetMax3_2.x - 1;
        offsetMin3_2.x = offsetMin3_2.x - 1;

        bg3.GetComponent<RectTransform>().offsetMax = offsetMax3_1;
        bg3.GetComponent<RectTransform>().offsetMin = offsetMin3_1;
        bg3_2.GetComponent<RectTransform>().offsetMax = offsetMax3_2;
        bg3_2.GetComponent<RectTransform>().offsetMin = offsetMin3_2;

        if (offsetMax3_1.x <= -Screen.width)
        {
            bg3.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);
            bg3.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);

        }
        if (offsetMax3_2.x <= -Screen.width)
        {
            bg3_2.GetComponent<RectTransform>().offsetMax = new Vector2(Screen.width, 0);
            bg3_2.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width, 0);

        }
    }
}
