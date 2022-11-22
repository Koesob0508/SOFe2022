using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class StageManager
{
    [System.Serializable]
    public class SerializedNode
    {
        #region StageData
        public int stageLevel;
        public GameManager.MapType stageMapType;
        public int stageStep;
        #endregion

        public StageManager.StageType type;
        public int step;
        public int index;
        public Vector3 position;
        public bool isMerged;
        public bool isCompleted;
        public bool isPassPoint;
        public bool isInteractable;
        public List<int> nextStages;
        public List<Enemy> enemies;

        public string ToLog()
        {
            string resultLog = "";

            resultLog += "Type : " + type;
            resultLog += " Step : " + step;
            resultLog += " Index : " + index;
            resultLog += " Position : " + position;

            return resultLog;
        }
    }

    [System.Serializable]
    public class Step : ISerializationCallbackReceiver
    {
        public List<SerializedNode> serializeNodes;
        private List<StageNode> stageNodes;

        public Step()
        {
            stageNodes = new List<StageNode>();
            serializeNodes = new List<SerializedNode>();
        }

        public void AddStageNode(StageNode _stageNode)
        {
            stageNodes.Add(_stageNode);
        }

        public StageNode GetStageNode(int _index)
        {
            return stageNodes[_index];
        }

        public List<StageNode> GetStageNodes()
        {
            return stageNodes;
        }

        public void OnAfterDeserialize()
        {
            //Debug.Log("±¸ÇöÇÏ¼¼¿ê");
        }

        public void OnBeforeSerialize()
        {
            serializeNodes.Clear();

            foreach (StageNode stage in stageNodes)
            {
                SerializedNode node = new SerializedNode();

                node.stageLevel = stage.StageLevel;
                node.stageMapType = stage.StageMapType;
                node.stageStep = stage.StageStep;

                node.type = stage.Type;
                node.step = stage.Step;
                node.index = stage.Index;
                node.position = stage.transform.position;
                node.isMerged = stage.IsMerged;
                node.isCompleted = stage.IsCompleted;
                node.isPassPoint = stage.IsPassPoint;
                node.isInteractable = stage.IsInteractable;
                node.nextStages = stage.NextStages;
                node.enemies = stage.Enemies;


                serializeNodes.Add(node);
            }
        }

        public void OnDebugMode()
        {
            foreach(StageNode stageNode in stageNodes)
            {
                stageNode.button.interactable = true;

                stageNode.ChangeNodeAlpha(true);
            }
        }

        public void OffDebugMode()
        {
            foreach(StageNode stageNode in stageNodes)
            {
                if(stageNode.IsInteractable)
                {
                    stageNode.button.interactable = true;
                    stageNode.ChangeNodeAlpha(true);
                }
                else
                {
                    stageNode.button.interactable = false;
                    stageNode.ChangeNodeAlpha(false);
                }
            }
        }
    }

    [System.Serializable]
    public class StageMap
    {
        public List<Step> stages;

        public StageMap(List<Step> _stageList)
        {
            stages = _stageList;
        }
    }
}
