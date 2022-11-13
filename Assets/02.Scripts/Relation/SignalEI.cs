using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignalEI : CustomSignal
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
        preHero = null;
        postHero = null;
        preCharacter = null;
        postCharacter = null;
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        // 일단 subject가 Hero여야 실행
        if (_log.Causer is Hero)
        {
            preHero = postHero;
            preCharacter = postCharacter;
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;

            //if (preHero.Name != postHero.Name && object.ReferenceEquals(preCharacter, postCharacter))
            //{
            //    Debug.Log("서로 다른 히어로, 같은 대상에 대해 행동");
            //    return true;
            //}

            if (preHero == null)
            {
                return false;
            }

        }

        return true;
    }

    protected override void Apply()
    {
        Debug.Log("E와 I 간 Event 발생");

        if (GameManager.Relation.IsI(preHero))
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
            UpdateLog(0, "아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
            UpdateLog(0, "이것도 천생연분?!");
        }

        if (GameManager.Relation.IsI(postHero))
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
            UpdateLog(1, "아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
            UpdateLog(1, "이것도 천생연분?!");
        }
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
