using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleLogPanel : MonoBehaviour
{
    uint curLogCount = 0;
    public uint maxLogCount = 5;

    public float logRemainTime = 1f;
    public float spacing = 5f;

    public GameObject logPrefab;

    List<GameObject> logPool = new List<GameObject>();

    uint heightPixel;
    uint widthPixel;

    float minAncY = 0.8f;
    float maxAncY = 1.0f;

    Queue<Tuple<Character, float, Character>> logData = new Queue<Tuple<Character, float, Character>>();

    public void Start()
    {
        var cs = GetComponentInParent<CanvasScaler>();
        float wRatio = Screen.width / cs.referenceResolution.x;
        float hRatio = Screen.height / cs.referenceResolution.y;
        float ratio = wRatio * (1 - cs.matchWidthOrHeight) + hRatio * (cs.matchWidthOrHeight);
        heightPixel = (uint)(GetComponent<RectTransform>().rect.height * ratio);
        widthPixel = (uint)(GetComponent<RectTransform>().rect.width * ratio);


        for(int i = 0; i < maxLogCount; i++)
        {
            GameObject g = Instantiate(logPrefab, transform);
            g.SetActive(false);
            logPool.Add(g);
        }
    } 

    GameObject FindObjectInPool()
    {
        GameObject g = logPool.Find((elem) => elem.activeSelf == false);
        if(g == null)
        {
            g = Instantiate(logPrefab, this.transform);
        }
        logPool.Add(g);
        return g;

    }
    /// <summary>
    /// Add Log to Panel
    /// </summary>
    /// <param name="logData"> 1. Causer, 2. Damage 3. Target </param>
    public void AddLog(Tuple<Character, float, Character> log)
    {
        logData.Enqueue(log);
        if (curLogCount < maxLogCount)
            PublishLog();
        else
            StartCoroutine(WaitAndPublishLog());

    }
    
    void PublishLog()
    {
        var Data = logData.Dequeue();
        curLogCount++;
        GameObject logObj = FindObjectInPool();
        var tmp = logObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        logObj.SetActive(true);
        RectTransform rt = logObj.GetComponent<RectTransform>();
        logObj.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Battle.GetUIImage(Data.Item1.GUID);
        logObj.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.Battle.GetUIImage(Data.Item3.GUID);
        logObj.GetComponent<RectTransform>().LeanAlpha(1, 0.01f);
        tmp.color = new Vector4(1,1,1,1);

        rt.anchorMax = new Vector2(1, maxAncY);
        rt.anchorMin = new Vector2(0, minAncY);
        rt.offsetMin = new Vector2(0, 5);
        rt.offsetMax = new Vector2(0, 0);  
        maxAncY -= 0.2f;
        minAncY -= 0.2f;
        tmp.SetText(Data.Item1.Name + " Set Damage " + Data.Item2 + " to " + Data.Item3.Name);
        StartCoroutine(DeleteLog(logObj, logRemainTime));
    }

    IEnumerator WaitAndPublishLog()
    {
        yield return new WaitUntil(() => curLogCount < maxLogCount);
        PublishLog();
    }

    IEnumerator DeleteLog(GameObject logObj,float time)
    {
        logObj.GetComponent<RectTransform>().LeanAlpha(0, time);
        LeanTweenExt.LeanAlphaText(logObj.GetComponentInChildren<TMPro.TextMeshProUGUI>(), 0, time);
        yield return new WaitForSecondsRealtime(time);
        logObj.SetActive(false);
        curLogCount--;
        maxAncY += 0.2f;
        minAncY += 0.2f;
    }
}
