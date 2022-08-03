using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Path
{
    public class PathManager : MonoBehaviour
    {

        private GameObject Background;
        private GameObject Obstacles_Parent;

        List<Collider2D> Obstacles = new List<Collider2D>();

        ArrayList nodes = new ArrayList();
        ArrayList tmpPath = new ArrayList();

        [Range(0, 20)]
        public int ColCount = 0;
        [Range(0,20)]
        public int RowCount = 0;

        float offsetX = 0, offsetY = 0;
        float Width = 0, Height = 0;

        public static float cellSizeX = 0, cellSizeY = 0;

        float xMin, yMin;
        
        void Start()
        {
            Background = GameObject.FindGameObjectWithTag("Background");
            SpriteRenderer backSpriteRender = Background.GetComponent<SpriteRenderer>();
            Width = backSpriteRender.bounds.size.x;
            Height = backSpriteRender.bounds.size.y;
            
            xMin = backSpriteRender.bounds.center.x - (backSpriteRender.bounds.size.x / 2);
            yMin = backSpriteRender.bounds.center.y - (backSpriteRender.bounds.size.y / 2);
            cellSizeX = Width / ColCount;
            cellSizeY = Height / RowCount;

            offsetX = cellSizeX / 2;
            offsetY = cellSizeY / 2;


            int temp = 0;
            for (int i = 0; i < ColCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    nodes.Add(new Node(xMin + (cellSizeX * i) + offsetX, yMin + (cellSizeY * j) + offsetY,temp));
                    temp++;
                }
            }

            RefreshObstacles();
        
            //tmpPath = FindPath(nodes[0] as Node, nodes[35] as Node);
            //foreach(Node n in tmpPath)
            //{
            //    Debug.Log(n.index);
            //}
        }
        void RefreshObstacles()
        {
            foreach (var obj in Obstacles_Parent.GetComponentsInChildren<Collider2D>())
            {
                Obstacles.Clear();
                Obstacles.Add(obj);
                foreach(Node node in nodes)
                {
                    if(obj.OverlapPoint(node.pos))
                    {
                        node.SetObstacle();

                    }
                }
            }
        }
        private void OnDrawGizmos()
        {

           float size = 0.1f;
           foreach(Node n in nodes)
            {
                if (n.GetObstacle())
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawSphere(n.GetPosition(), size);
            }
            float t = 0;
            foreach (Node n in tmpPath)
            {
                t += 0.1f;
                Gizmos.color = Color.Lerp(Color.green, Color.cyan, t);
                Gizmos.DrawSphere(n.GetPosition(), 0.1f);
            }
        }

        private class TmpComparer : Comparer<KeyValuePair<int, float>>
        {
            public override int Compare(KeyValuePair<int, float> x, KeyValuePair<int, float> y)
            {
                return (int)(x.Value.CompareTo(y.Value));
            }
        }


        public Node GetClosestNode(Vector2 pos)
        {
            float min = float.MaxValue;
            Node temp = new Node(0,0);
            foreach (Node node in nodes)
            {
                float dist = Vector2.Distance(node.pos, pos);
                if (dist < min)
                {
                    min = dist;
                    temp = node;
                }
            }
            return temp;
        }
        public ArrayList FindPath(Node start, Node end)
        {
            ArrayList path = new ArrayList();
            List<int> closed = new List<int>();
            List<KeyValuePair<int, float>> opened = new List<KeyValuePair<int, float>>();

            closed.Add(start.index);

            Node CurSelected = start;
            while(CurSelected != end)
            {

                int idx = CurSelected.index;
                // 자기 자신을 닫힌 목록에 추가
                closed.Add(idx);
                if(opened.Exists((a) => { return a.Key == idx; }))
                {
                    opened.RemoveAt(opened.FindIndex((a) => { return a.Key == idx; }));
                }
                //1차원 배열의 row , col 설정
                int row = idx % RowCount;
                int col = idx / RowCount;


                //인접 8개 타일 탐색
                for (int i = col - 1; i <= col + 1; i++)
                {
                    //만약 타일의 맨 끝을 벗어난다면 스킵
                    if (i < 0 || i >= RowCount)
                        continue;

                    for(int j = row - 1; j <= row + 1;  j++)
                    {
                        float CurG = CurSelected.G;

                        //만약 타일의 맨 끝을 벗어난다면 스킵..
                        if (j < 0 || j  >= ColCount)
                            continue;
                        //현재 선택한 타일의 인덱스
                        int tempIdx = i * (RowCount) + j;

                        // 자기 자신일 때 Pass...
                        if (tempIdx == idx)
                            continue;

                        //만약 현재 타일이 닫혀있다면 Pass..
                        if (closed.Contains(tempIdx))
                            continue;

                        Node temp = nodes[tempIdx] as Node;

                        // 장애물일 때 Pass....
                        if (temp.GetObstacle())
                            continue;

                        //G, H 계산하여 
                        float G = CurG + Vector2.Distance(CurSelected.pos, temp.pos);
                        float H = Mathf.Abs(temp.pos.x - end.pos.x) + Mathf.Abs(temp.pos.y - end.pos.y);
                   

                        //open 에 존재하지 않으면 추가
                        if(!opened.Exists((a) => { return a.Key == tempIdx; }))
                        {
                            temp.Parent = CurSelected;
                            temp.G = G;
                            opened.Add(new KeyValuePair<int, float>(tempIdx, G + H));

                        }
                        else
                        {
                            //존재하는데 현재 G가 더 낮아 갱신이 필요할 경우
                           if(temp.G > G)
                            {
                                G = CurSelected.G + Vector2.Distance(CurSelected.pos, temp.pos);
                                temp.Parent = CurSelected;
                                temp.G = G;
                                opened.RemoveAt(opened.FindIndex((a) => { return a.Key == tempIdx; }));
                                opened.Add(new KeyValuePair<int, float>(tempIdx, G + H));
                            }
                        }


                    }
                }
                opened.Sort(new TmpComparer());
                Node NextNode = nodes[opened[0].Key] as Node;
                closed.Add(NextNode.index);
                CurSelected = NextNode;
            }

            do
            {
                path.Add(CurSelected);
                CurSelected = CurSelected.Parent;
            }
            while (CurSelected != start);

            path.Add(start);
            path.Reverse();
            tmpPath = path;
            return path;
        }
    }

}
