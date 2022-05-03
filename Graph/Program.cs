using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var graph = new Graph();

            #region AddVertexAndEdges
            var Kyiv = new Vertex(1, "Киев");
            var Zhytomyr = new Vertex(2, "Житомир");
            var Rovno = new Vertex(3, "Ровно");
            var Lviv = new Vertex(4, "Львов");
            var BilaTserkva = new Vertex(5, "Белая Церковь");
            var Uman = new Vertex(6, "Умань");
            var Odessa = new Vertex(7, "Одесса");
            var Kropivnitskiy = new Vertex(8, "Кропивницкий");
            var Nikolaev = new Vertex(9, "Николаев");
            var KrivoyRog = new Vertex(10, "Кривой Рог");
            var Ternopil = new Vertex(11, "Тернополь");
            var v12 = new Vertex(12);
            var v13 = new Vertex(13);
            var v14 = new Vertex(14);
            var v15 = new Vertex(15);
            var v16 = new Vertex(16);

            graph.Add(Kyiv);
            graph.Add(Zhytomyr);
            graph.Add(Rovno);
            graph.Add(Lviv);
            graph.Add(BilaTserkva);
            graph.Add(Uman);
            graph.Add(Odessa);
            graph.Add(Kropivnitskiy);
            graph.Add(Nikolaev);
            graph.Add(KrivoyRog);
            graph.Add(Ternopil);
            graph.Add(v12);
            graph.Add(v13);
            graph.Add(v14);
            graph.Add(v15);
            graph.Add(v16);

            graph.AddEdge(Kyiv, Zhytomyr, 140);   // Киев - Житомир, 140 км
            graph.AddEdge(Zhytomyr, Rovno, 188);   // Житомир - Ровно, 188 км
            graph.AddEdge(Rovno, Lviv, 210);   // Ровно - Львов, 210 км
            graph.AddEdge(Kyiv, BilaTserkva, 86);    // Киев - Белая Церковь, 86
            graph.AddEdge(BilaTserkva, Uman, 128);   // Белая Церковь - Умань, 128
            graph.AddEdge(Uman, Odessa, 271);   // Умань - Одесса, 271
            graph.AddEdge(Uman, Kropivnitskiy, 166);   // Умань - Кропивницкий, 166
            graph.AddEdge(Odessa, Nikolaev, 133);  // Одесса - Николаев, 133
            graph.AddEdge(Kropivnitskiy, KrivoyRog, 119);   // Кропивницкий - Кривой Рог, 119
            graph.AddEdge(KrivoyRog, Kropivnitskiy, 125);   // Кривой Рог - Николаев, 125
            graph.AddEdge(Lviv, Ternopil, 134);
            graph.AddEdge(BilaTserkva, v12, 7);
            graph.AddEdge(v12, v13, 8);
            graph.AddEdge(v12, v14, 9);
            graph.AddEdge(v12, v15, 10);
            graph.AddEdge(v12, v16, 11);
            #endregion

            PrintMatrix(graph);

            Console.WriteLine("\n\n");

            Console.WriteLine("Все связанные вершины с:");
            PrintVertex(graph, Kyiv);
            PrintVertex(graph, Lviv);
            PrintVertex(graph, Odessa);
            PrintVertex(graph, KrivoyRog);

            Console.WriteLine();


            while (true)
            {
                Console.WriteLine("\nВведите индекс города к которому вы хотите построить маршрут с Киева:");

                var finishindex = int.Parse(Console.ReadLine());

                if (graph.FindPath(1, finishindex, out var path, out var lenght))
                {
                    Console.WriteLine($"Удалось построить маршрут длинной {lenght} км.");
                    PrintPath(path);
                }
                else
                {
                    Console.WriteLine("Не удалось построить маршрут.");
                }

            }

        }

        /// <summary>
        /// Вывести все смежные вершины
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertex"></param>
        public static void PrintVertex(Graph graph, Vertex vertex)
        {
            Console.Write($"{vertex}: ");
            foreach (var v in graph.GetVertexList(vertex))
            {
                Console.Write(v+", ");
            }
            Console.WriteLine();
        }


        /// <summary>
        /// Вывести матрицу графа
        /// </summary>
        /// <param name="graph"></param>
        public static void PrintMatrix(Graph graph)
        {
            var matrix = graph.GetMatrix();

            for (int i = 0; i < graph.VertexCount; i++)
            {

                if (i == 0)
                {
                    Console.Write("\t");
                    for (int j = 0; j < graph.EdgeCount; j++)
                    {
                        if (j < 10)
                        {
                            Console.Write(j + "   ");
                        }
                        else
                        {
                            Console.Write(j + "  ");
                        }
                       
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("\t--------------------------------------------------------------------");

                Console.Write(i + "|\t");
                for (int j = 0; j < graph.EdgeCount; j++)
                {
                    if (matrix[i, j] < 10)
                    {
                        Console.Write(matrix[i, j] + "   ");
                    }
                    else
                    {
                        Console.Write(matrix[i, j] + "  ");
                    }
                }
                Console.WriteLine();


            }
            Console.WriteLine("\t--------------------------------------------------------------------");
        }


        public static void PrintPath(List<Vertex> listV)
        {
            Console.WriteLine($"Маршрут от {listV.First()} до {listV.Last()}");
            foreach (var res in listV)
            {
                Console.Write(res + ", ");
            }
            Console.WriteLine();
        }








    }
}
