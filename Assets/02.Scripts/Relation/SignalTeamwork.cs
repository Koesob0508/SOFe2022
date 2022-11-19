using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTeamwork : CustomSignal
{
    [SerializeField] private Hero preHero;
    [SerializeField] private Hero postHero;
    [SerializeField] private Character preCharacter;
    [SerializeField] private Character postCharacter;
    [SerializeField] private GameObject preChat;
    [SerializeField] private GameObject postChat;
    [SerializeField] private TMP_Text preText;
    [SerializeField] private TMP_Text postText;

    public override void Init()
    {
        signalTitle = "팀워크!";
        signalExplain = "두 용병이 함께 적을 처치";
        preHero = new Hero();
        postHero = new Hero();
        preCharacter = new Character();
        postCharacter = new Character();
    }
    protected override bool Condition(BattleLogPanel.Log _log)
    {
        if (_log.Causer is Hero && _log.Target is Enemy)
        {
            preHero = postHero.DeepCopy();
            preCharacter = postCharacter.DeepCopy();
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;

            if (preHero.IsActive == false)
            {
                return false;
            }

            if (preHero.Name != postHero.Name &&
               _log.Type == BattleLogPanel.LogType.Kill &&
               preCharacter.Equals(postCharacter))
            {
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("팀워크!");

        GameManager.Relation.SetChangeRelationship(preHero, postHero, 1);
        UpdateLog(0, "중요한 것은");
        GameManager.Relation.SetChangeRelationship(postHero, preHero, 1);
        UpdateLog(1, "꺾이지 않는 마음");
    }

    private void UpdateLog(int _index, string _string)
    {
        if (_index == 0)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(preHero).transform.position;
            preChat.transform.position = position;
            preChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preChat.SetActive(true);
            StartCoroutine(SetDisable(preChat));
        }
        else if (_index == 1)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(postHero).transform.position;
            postChat.transform.position = position;
            postChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postChat.SetActive(true);
            StartCoroutine(SetDisable(postChat));
        }
    }

    private IEnumerator SetDisable(GameObject _gameObject)
    {
        yield return new WaitForSeconds(1f);

        _gameObject.SetActive(false);
    }
}
