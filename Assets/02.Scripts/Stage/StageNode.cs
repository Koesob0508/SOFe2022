using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class StageNode : MonoBehaviour
{
    public enum StageType
    {
        Battle = 0,
        Town,
        Event
    }

    public StageType Type { get; private set; }
    public int Step { get; private set; }
    public int Index { get; private set; }
    public bool IsMerged { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsPassPoint { get; private set; }
    public List<Vector2Int> NextStages { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public Button button;
    public UnityAction<StageNode> RegistStageNode;

    public void Init(StageType _type, int _step, int _index, bool _isMerged, float _scale)
    {
        Type = _type;
        Step = _step;
        Index = _index;
        IsMerged = false;
        IsCompleted = false;
        IsPassPoint = false;
        NextStages = new List<Vector2Int>();

        name = string.Format("Step : {0} Index : {1}", Step, Index);
        transform.localScale = new Vector3(_scale, _scale, 1f);
        

        if (this.Step == 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

        if (_type == StageType.Battle)
        {
            Enemies = new List<Enemy>();

            Enemy e1 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy e2 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy e3 = (Enemy)GameManager.Data.ObjectCodex[100];

            e1.Position = new Vector2(3, 5);
            e2.Position = new Vector2(0, 8);
            e3.Position = new Vector2(3, -5);

            Enemies.Add(e1);
            Enemies.Add(e2);
            // enemies.Add(e3);
        }

        button.onClick.AddListener(() => RegistStageNode(this));

        switch (Type)
        {
            case StageType.Battle:
                button.onClick.AddListener(GameManager.Scene.ToBattleScene);
                break;

            case StageType.Town:
                button.onClick.AddListener(GameManager.Scene.ToTownScene);
                break;

            case StageType.Event:
                button.onClick.AddListener(GameManager.Scene.ToEventScene);
                break;
        }
    }

    public void AddNextStage(StageNode _nextStage)
    {
        if (!this.NextStages.Contains(new Vector2Int(_nextStage.Step, _nextStage.Index)))
        {
            Vector2Int nextStage = new Vector2Int(_nextStage.Step, _nextStage.Index);
            NextStages.Add(nextStage);
        }
    }

    public void Complete(List<StageManager.Step> _stages)
    {
        IsCompleted = true;
        IsPassPoint = true;

        foreach (Vector2Int stage in NextStages)
        {
            _stages[stage.x].GetStageNode(stage.y).button.interactable = true;
        }
    }

    public void GenerateLane(GameObject _lanePrefab, List<StageManager.Step> _stages)
    {
        //foreach (StageNode nextStage in nextStages)
        //{
        foreach (Vector2Int nextStage in NextStages)
        {
            // ���� ��ġ�� ��ǥ ��ġ �޾ƿ�
            Vector2 startPosition = this.transform.position;
            // Vector2 targetPosition = nextStage.transform.position; // �� �κи� �ٲٸ� ��
            int targetStep = nextStage.x;
            int targetIndex = nextStage.y;
            Vector2 targetPosition = _stages[targetStep].GetStageNode(targetIndex).transform.position;
            Vector2 instantiatePosition = (targetPosition + startPosition) / 2;
            Quaternion angle;

            // lanePrefab�� ũ�� ��������
            Vector3 spriteSize = this.GetSpriteSize(_lanePrefab);

            // Rotation ����
            // Scale ����

            // ������ ���ΰ�?
            float radianAngle = Mathf.Atan2(targetPosition.y - startPosition.y, targetPosition.x - startPosition.x);
            float degreeAngle = 180 / Mathf.PI * radianAngle;

            // ������ ���� ���ϱ�
            float length = (targetPosition.x - startPosition.x) / Mathf.Cos(radianAngle);
            // ��ǥ scale = ������ ���� / ���� ����
            // �� �� ���� �������� ��ǥ scale ��

            // x������ �� �� ������Ʈ���
            if (spriteSize.x > spriteSize.y)
            {
                // ���� �״�� z�� ���
                angle = Quaternion.Euler(0f, 0f, degreeAngle);
            }
            //y ������ �� �� ������Ʈ���
            else
            {
                // -(pi/2 - angle) ������ z�� ȸ�� 90��
                angle = Quaternion.Euler(0f, 0f, -(90f - degreeAngle));
            }

            // �߰� �������� Instantiate
            GameObject lane = Instantiate(_lanePrefab, instantiatePosition, angle, this.transform.parent);

            var size = lane.GetComponent<SpriteRenderer>().size;
            size.y = length;
            lane.GetComponent<SpriteRenderer>().size = size;
        }
    }

    private Vector3 GetSpriteSize(GameObject _sprite)
    {
        Vector3 worldSize = Vector3.zero;

        if (_sprite.GetComponent<SpriteRenderer>())
        {
            Vector2 spriteSize = _sprite.GetComponent<SpriteRenderer>().sprite.rect.size;
            Vector2 localSpriteSize = spriteSize / _sprite.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            worldSize = localSpriteSize;
            worldSize.x *= _sprite.transform.lossyScale.x;
            worldSize.y *= _sprite.transform.lossyScale.y;
        }
        else
        {
            Debug.Log("SpriteRenderer Null");
        }

        return worldSize;
    }

    public void SetIsMerged(bool _isMerged)
    {
        IsMerged = _isMerged;
    }
}