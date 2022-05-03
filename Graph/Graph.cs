using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {

        List<Vertex> Vertexes = new List<Vertex>();    // Список вершин

        List<Edge> Edges = new List<Edge>();        // Список ребер

        public int VertexCount => Vertexes.Count;
        public int EdgeCount => Edges.Count;

        public Graph()
        {

        }

        public void Add(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }

        public void AddEdge(Vertex from, Vertex to, int weight = 1)
        {
            var edge = new Edge(from, to, weight);
            Edges.Add(edge);
        }


        public int[,] GetMatrix()
        {
            var matrix = new int[Vertexes.Count, Edges.Count+1];

            foreach (var edge in Edges)
            {
                var row = edge.From.Number;
                var col = edge.To.Number;

                matrix[row, col] = edge.Weight;
            }

            return matrix;
        }

        /// <summary>
        /// Передаем вершину, получаем все смежные с ней
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Vertex> GetVertexList(Vertex vertex) 
        {
            var result = new List<Vertex>();
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex))
                {
                    result.Add(edge.To);
                }
            }

            return result;
        }


        /// <summary>
        /// Обход графа волнами.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public bool FindPath(int startIndex, int finishIndex, out List<Vertex> fullpath, out int lenght)
        {
            if ( startIndex < 1 || finishIndex < 1)
            {
                throw new ArgumentOutOfRangeException("Start index and FinishIndex must be >= 1.");
            }
            Vertex start = Vertexes[startIndex-1];
            Vertex finish = Vertexes[finishIndex-1];

            var path = new List<ValueTuple<int, Vertex>>();         // int - wavecount, Vertex - neighboring vertex
            fullpath = new List<Vertex>();
            lenght = 0;

            path.Add((0, start));

            if (start == finish)
            {
                return true;
            }

            var finded = false;
            var wavecount = 0;
            while (!finded)
            {
                var current = path.Where(x => x.Item1 == wavecount).Select(x => x.Item2).ToList();            
                wavecount++;

                foreach (var vertex in current)
                {

                    foreach (var v in GetVertexList(vertex))
                    {

                        if (v.Equals(finish))
                        {
                            path.Add((wavecount, v));
                            finded = true;
                            break;
                        }
                        else if (!path.Select(x => x.Item2).ToList().Contains(v))
                        {
                            path.Add((wavecount, v));
                        }
                    }            
                    
                    if (finded)
                    {
                        break;
                    }

                }

                if (wavecount >= EdgeCount)
                {   // Если количество волн превысило количество ребер - к искомому элементу дороги нет.
                    return false;
                }
            }

            // Выбрать все элементы соседние с конечным
            // Выбрать среди них единственный с меньшим индексом волн
            // Добавить его в список и отталкиваться от него

            var result = new List<Vertex>();
            var currentItem = path.Last().Item2;

            for (var i = wavecount-1; i >= 0; i--)
            {
                result.Add(currentItem);

                var allneighbors = path.Where(x => x.Item1 == i).Select(x => x.Item2).ToList();

                foreach (var neighbor in allneighbors)
                {
                    if (GetVertexList(neighbor).Contains(currentItem))
                    {
                        currentItem = neighbor;
                    }
                }
            }

            result.Add(currentItem);
            result.Reverse();
            fullpath = result;

            for (var i = 0; i < result.Count-1; i++)
            {

                lenght += Edges.Where(x=> x.From == result[i]).First(x => x.To == result[i+1]).Weight; 

            }


            return true;
        }

    }





}
