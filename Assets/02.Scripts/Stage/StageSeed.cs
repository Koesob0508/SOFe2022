using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager
{
    public enum StageType
    {
        Battle = 0,
        Town,
        Event
    }

    public class Seed
    {
        public int StageLevel { get; set; }
        public GameManager.MapType StageMapType { get; set; }
        public int StageStep { get; set; }

        public StageManager.StageType Type { get; set; }
        public int Index { get; private set; }
        public int Step { get; private set; }
        public Vector2 Position { get; private set; }
        public List<int> NextStages { get; private set; }
        public Seed Pointer { get; private set; }
        public bool IsMerged
        {
            get
            {
                if (Pointer == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Seed()
        {

        }

        public Seed(int _index, int _step)
        {
            Type = StageManager.StageType.Battle; // 임시로 Battle로 고정
            Index = _index;
            this.Step = _step;
            this.NextStages = new List<int>();
        }

        public string ToLog()
        {
            string log = string.Format("index : {0}, step : {1}, Position : x={2}, y={3}", Index, Step, Position.x, Position.y);
            log += ", ";
            string next = "Next Stage : ";
            string nextPointer = "Pointer : ";

            foreach (int nextIndex in this.NextStages)
            {
                next += nextIndex;
            }
            next += ", ";

            if (this.Pointer != null)
            {
                nextPointer += this.Pointer.Index.ToString();
            }
            else
            {
                nextPointer += "this";
            }

            return log + next + nextPointer;
        }

        public void SetPosition(float _xPosition, float _yPosition)
        {
            float xPosition = _xPosition;
            float yPosition = _yPosition;
            Position = new Vector2(xPosition, yPosition);
        }

        public void SetNextStage(int _index)
        {
            if (!NextStages.Contains(_index))
            {
                this.NextStages.Add(_index);
            }
        }

        public void RandomizePosition(float _xRange, float _yRange)
        {
            var randomPosition = Position;

            randomPosition.x += Random.Range(0f, _xRange);
            randomPosition.y += Random.Range(0f, _yRange);

            Position = randomPosition;
        }

        public void Merge(Seed _to)
        {
            Pointer = _to;
            // to의 nextStage를 바꿔줘야 한다.
            foreach (int index in NextStages)
            {
                if (!_to.NextStages.Contains(index))
                {
                    _to.NextStages.Add(index);
                }
            }
        }

        public int GetResultPointer()
        {
            int resultIndex = Index;

            if (this.Pointer != null)
            {
                resultIndex = this.Pointer.GetResultPointer();
            }

            return resultIndex;
        }
    }
}

