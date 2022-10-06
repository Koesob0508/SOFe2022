using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyPanel : MonoBehaviour
{
    Dictionary<Observer_Battle.SynergyEvent,GameObject> childDict = new Dictionary<Observer_Battle.SynergyEvent,GameObject>();
    public GameObject synergyTextPrefab;
    public void AddSynergy(Observer_Battle.SynergyEvent e)
    {
        GameObject g = Instantiate(synergyTextPrefab, transform);
        var textComp = g.GetComponent<TMPro.TextMeshProUGUI>();
        switch (e)
        {
            case Observer_Battle.SynergyEvent.SendMeHome:
                textComp.text = e.ToString();
                break;
            case Observer_Battle.SynergyEvent.SilentScream:
                textComp.text = e.ToString();
                break;
            case Observer_Battle.SynergyEvent.TillPlanZ:
                textComp.text = e.ToString();
                break;
            case Observer_Battle.SynergyEvent.NoPlan:
                textComp.text = e.ToString();
                break;
        }
        childDict.Add(e, g);
    }
    public void RemoveSynergy(Observer_Battle.SynergyEvent e)
    {
        Destroy(childDict[e]);
        childDict.Remove(e);
    }
}
