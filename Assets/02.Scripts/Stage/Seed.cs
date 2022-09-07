using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager
{
    public class Seed
    {
        private int index;
        private int step;
        private Vector2 position;
        private List<int> nextStage;
        private Seed pointer;

        public Seed(int _index, int _step)
        {
            this.index = _index;
            this.step = _step;
            this.nextStage = new List<int>();

        }

        public string ToLog()
        {
            string log = string.Format("index : {0}, step : {1}, Position : x={2}, y={3}", index, step, position.x, position.y);
            log += ", ";
            string next = "Next Stage : ";
            string nextPointer = "Pointer : ";

            foreach (int nextIndex in this.nextStage)
            {
                next += nextIndex;
            }
            next += ", ";

            if (this.pointer != null)
            {
                nextPointer += this.pointer.index.ToString();
            }
            else
            {
                nextPointer += "this";
            }

            return log + next + nextPointer;
        }

        public int GetIndex()
        {
            return this.index;
        }

        public int GetStep()
        {
            return this.step;
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public void SetPosition(float _xPosition, float _yPosition)
        {
            float xPosition = _xPosition;
            float yPosition = _yPosition;
            position = new Vector2(xPosition, yPosition);
        }

        public List<int> GetNextStage()
        {
            return this.nextStage;
        }

        public void SetNextStage(int _index)
        {
            if (!nextStage.Contains(_index))
            {
                this.nextStage.Add(_index);
            }
        }

        public bool isMerged()
        {
            if (pointer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RandomizePosition(float _xRange, float _yRange)
        {
            var randomPosition = position;

            randomPosition.x += Random.Range(0f, _xRange);
            randomPosition.y += Random.Range(0f, _yRange);

            position = randomPosition;
        }

        public void Merge(Seed _to)
        {
            this.pointer = _to;
            // to¿« nextStage∏¶ πŸ≤„¡‡æﬂ «—¥Ÿ.
            foreach (int index in this.nextStage)
            {
                if (!_to.nextStage.Contains(index))
                {
                    _to.nextStage.Add(index);
                }
            }
        }

        public int GetResultPointer()
        {
            int resultIndex = this.index;

            if (this.pointer != null)
            {
                resultIndex = this.pointer.GetResultPointer();
            }

            return resultIndex;
        }
    }
}

