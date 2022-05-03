using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// Класс вершины графа
    /// </summary>
    public class Vertex
    {

        public int Number { get; set; }
        // Тут могут быть дополнительные данные, название города, название магазина, и т.д.
        public string Name { get; set; }


        public Vertex (int number, string name = "")
        {
            Number = number;
            Name = name;
        }

        public override string ToString()
        {
            return Number.ToString() + " " + Name;
        }
    }
}
