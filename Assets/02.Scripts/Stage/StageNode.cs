using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class StageNode : MonoBehaviour
{
    public StageManager.StageType Type { get; private set; }
    public int Step { get; private set; }
    public int Index { get; private set; }
    public bool IsMerged { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsPassPoint { get; private set; }
    public bool IsInteractable { get; set; }
    public List<int> NextStages { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public Button button;
    public UnityAction<StageNode> RegistStageNode;

    public void Init(StageManager.Seed _seed, float _scale)
    {
        Type = _seed.Type;
        Step = _seed.Step;
        Index = _seed.Index;
        IsMerged = _seed.IsMerged;
        IsCompleted = false;
        IsPassPoint = false;
        IsInteractable = false;
        NextStages = new List<int>();

        name = string.Format("Step : {0} Index : {1}", Step, Index);
        transform.localScale = new Vector3(_scale, _scale, 1f);

        if (Step == 0)
        {
            IsInteractable = true;
            button.interactable = true;
        }
        else
        {
            IsInteractable = true;
            button.interactable = false;
        }

        if(_seed.Type == StageManager.StageType.Battle)
        {
            Enemies = new List<Enemy>();

            Enemy enemy1 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy enemy2 = (Enemy)GameManager.Data.ObjectCodex[100];
            Enemy enemy3 = (Enemy)GameManager.Data.ObjectCodex[100];

            enemy1.Position = new Vector2(3, 5);
            enemy2.Position = new Vector2(0, 8);
            enemy3.Position = new Vector2(3, -5);

            Enemies.Add(enemy1);
            Enemies.Add(enemy2);
            //Enemies.Add(enemy3);
        }

        button.onClick.AddListener(() => RegistStageNode(this));

        switch (Type)
        {
            case StageManager.StageType.Battle:
                button.onClick.AddListener(GameManager.Scene.ToBattleScene);
                break;

            case StageManager.StageType.Town:
                button.onClick.AddListener(GameManager.Scene.ToTownScene);
                break;

            case StageManager.StageType.Event:
                button.onClick.AddListener(GameManager.Scene.ToEventScene);
                break;
        }
    }

    public void LoadInit(StageManager.SerializedNode _saved, float _scale)
    {
        Type = _saved.type;
        Step = _saved.step;
        Index = _saved.index;
        IsMerged = _saved.isMerged;
        IsCompleted = _saved.isCompleted;
        IsPassPoint = _saved.isPassPoint;
        //IsInteractable = _saved.isInteractable;
        IsInteractable = _saved.isInteractable;
        NextStages = _saved.nextStages;
        Enemies = _saved.enemies;

        name = string.Format("Step : {0} Index : {1}", Step, Index);
        transform.localScale = new Vector3(_scale, _scale, 1f);  

        if(IsMerged)
        {
            gameObject.SetActive(false);
            button.interactable = false;
        }
        else
        {
            if (IsInteractable)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }

            button.onClick.AddListener(() => RegistStageNode(this));

            switch (Type)
            {
                case StageManager.StageType.Battle:
                    button.onClick.AddListener(GameManager.Scene.ToBattleScene);
                    break;

                case StageManager.StageType.Town:
                    button.onClick.AddListener(GameManager.Scene.ToTownScene);
                    break;

                case StageManager.StageType.Event:
                    button.onClick.AddListener(GameManager.Scene.ToEventScene);
                    break;
            }
        }
    }


    public void AddNextStage(StageNode _nextStage)
    {
        if (!this.NextStages.Contains(_nextStage.Index))
        {
            int nextStage = _nextStage.Index;
            NextStages.Add(nextStage);
        }
    }

    public void Complete(List<StageManager.Step> _stages)
    {
        IsCompleted = true;
        IsPassPoint = true;
        IsInteractable = false;

        foreach (int stage in NextStages)
        {
            _stages[Step + 1].GetStageNode(stage).button.interactable = true;
            _stages[Step + 1].GetStageNode(stage).IsInteractable = true;
        }
    }

    public void GenerateLane(GameObject _lanePrefab, List<StageManager.Step> _stages)
    {
        //foreach (StageNode nextStage in nextStages)
        //{
        foreach (int nextStage in NextStages)
        {
            // ���� ��ġ�� ��ǥ ��ġ �޾ƿ�
            Vector2 startPosition = this.transform.position;
            // Vector2 targetPosition = nextStage.transform.position; // �� �κи� �ٲٸ� ��
            int targetStep = Step + 1;
            int targetIndex = nextStage;
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