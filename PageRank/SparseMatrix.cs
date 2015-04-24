using System.Collections.Generic;

namespace PageRank
{
    internal class SparseMatrix<T>
    {
        public SparseMatrix(T head, double rank)
        {
            Head = head;
            LinkedItems = new List<T>();
            Rank = rank;
        }

        /// <summary>
        ///     稀疏矩阵头
        /// </summary>
        public T Head { get; private set; }

        public double Rank { get; set; }

        /// <summary>
        ///     稀疏矩阵链接的项目
        /// </summary>
        public List<T> LinkedItems { get; set; }

        public void AddLink(T linkedItem)
        {
            LinkedItems.Add(linkedItem);
        }
    }
}