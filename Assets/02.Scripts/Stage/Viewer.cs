using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager
{
    class Viewer
    {
        private StageManager stage;
        public Viewer()
        {

        }
        public Viewer(StageManager _stage)
        {
            this.stage = _stage;
        }
        private GameObject canvas;
        private float screenHeight;
        private float screenWidth;
        private float stageNodeScale;
        public void Init()
        {
            // stageNode들을 Stage 오브젝트의 자식 오브젝트로 둘 예정
            canvas = GameObject.Find("Stage Canvas");

            if (canvas == null)
            {
                canvas = new GameObject { name = "Stage Canvas" };
            }

            canvas.transform.position = Vector3.zero;

            DontDestroyOnLoad(canvas);

            screenHeight = Camera.main.orthographicSize * 2;
            screenWidth = screenHeight * Camera.main.aspect;
            stageNodeScale = screenHeight * 0.1f;

            Vector3 stageManagerPosition = stage.transform.position;
            stageManagerPosition.x -= screenWidth * 3 / 8;
            stageManagerPosition.y -= screenHeight * 3 / 8;

            canvas.transform.position = stageManagerPosition;
            canvas.SetActive(false);
        }

        public void Clear()
        {
            Destroy(canvas);
        }

        public void On()
        {
            canvas.SetActive(true);
        }

        public void Off()
        {
            canvas.SetActive(false);
        }

        public List<Step> GenerateNode(List<List<Seed>> _seeds)
        {
            List<Step> resultList = new List<Step>();

            foreach (List<Seed> steps in _seeds)
            {
                Step stageSteps = new Step();

                foreach (Seed seed in steps)
                {
                    Vector2 position = seed.Position;
                    position *= screenHeight / (stage.startCount + 1);
                    position += new Vector2(canvas.transform.position.x, canvas.transform.position.y);
                    StageNode stageNode = null;

                    switch (seed.Type)
                    {
                        case StageType.Battle:
                            switch(seed.StageMapType)
                            {
                                case GameManager.MapType.Jungle:
                                    stageNode = Instantiate(stage.jungleNode, position, Quaternion.identity, canvas.transform);
                                    break;
                                case GameManager.MapType.Dessert:
                                    stageNode = Instantiate(stage.caveNode, position, Quaternion.identity, canvas.transform);
                                    break;
                                case GameManager.MapType.Boss:
                                    stageNode = Instantiate(stage.bossNode, position, Quaternion.identity, canvas.transform);
                                    break;
                                default:
                                    Debug.Log("뭔가 잘못됨");
                                    break;
                            }
                            break;
                        case StageType.Town:
                            stageNode = Instantiate(stage.townNode, position, Quaternion.identity, canvas.transform);
                            break;
                        case StageType.Event:
                            stageNode = Instantiate(stage.eventNode, position, Quaternion.identity, canvas.transform);
                            break;
                    }

                    stageNode.Init(seed, stageNodeScale);

                    stageNode.RegistStageNode += GameManager.Stage.UpdateCurrentNode;

                    stageSteps.AddStageNode(stageNode);
                }

                resultList.Add(stageSteps);
            }

            return resultList;
        }

        public void ReconstructStageNodes(StageMap _stageMap)
        {
            Debug.Log("Stage Node Reconstruct");
            foreach (Step step in _stageMap.stages)
            {
                foreach (SerializedNode node in step.serializeNodes)
                {
                    Vector2 position = node.position;
                    StageNode stageNode = null;

                    switch (node.type)
                    {
                        case StageType.Battle:
                            stageNode = Instantiate(stage.jungleNode, position, Quaternion.identity, canvas.transform);
                            break;
                        case StageType.Town:
                            stageNode = Instantiate(stage.townNode, position, Quaternion.identity, canvas.transform);
                            break;
                        case StageType.Event:
                            stageNode = Instantiate(stage.eventNode, position, Quaternion.identity, canvas.transform);
                            break;
                    }

                    stageNode.LoadInit(node, stageNodeScale);

                    stageNode.RegistStageNode += GameManager.Stage.UpdateCurrentNode;

                    step.AddStageNode(stageNode);
                }
            }
        }

        public void UpdateCanvas(float _positionX)
        {
            if (_positionX > screenWidth / 2)
            {
                float moveX = _positionX - screenWidth / 2;
                var canvasPosition = canvas.transform.position;
                canvasPosition.x -= moveX;
                canvas.transform.position = canvasPosition;
            }
        }

        public void MoveLeftCanvas(float _amount)
        {
            var resultX = canvas.transform.position.x - _amount;

            var resultPosition = canvas.transform.position;
            resultPosition.x = resultX;
            canvas.transform.position = resultPosition;
        }

        public void MoveRightCanvas(float _amount)
        {
            var resultX = canvas.transform.position.x + _amount;

            var resultPosition = canvas.transform.position;
            resultPosition.x = resultX;
            canvas.transform.position = resultPosition;
        }
    }
}
