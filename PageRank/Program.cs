using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageRank
{
    class Program
    {
        static void Main(string[] args)
        {
            var pageRank = new PageRank<char>(Map.Count);
            //计算30轮
            for (int i = 1; i <= 30; i++)
            {
                pageRank.NextCircle();
                foreach (var item in Map)
                {
                    pageRank.Calc(item.Value);
                }
                foreach (var item in Map)
                {
                    var cRank = pageRank.GetCurrentRank(item.Key);
                    item.Value.Rank = cRank;
                }
                var str = string.Join("\t", Map.Select(item => item.Value.Rank.ToString("N3")));
                Console.WriteLine("第{0}轮\t {1}", i, str);
            }
            Console.ReadLine();
        }

        static Dictionary<char, SparseMatrix<char>> Map = new Dictionary<char, SparseMatrix<char>> 
        {
            {'A', new SparseMatrix<char>('A', 0.25){LinkedItems = new List<char>{'A', 'C'}}},
            {'B', new SparseMatrix<char>('B', 0.25){LinkedItems = new List<char>{'A','B', 'D'}}},
            {'C', new SparseMatrix<char>('C', 0.25){LinkedItems = new List<char>{'C'}}},
            {'D', new SparseMatrix<char>('D', 0.25){LinkedItems = new List<char>{'B', 'C'}}}
        };
    }
}
