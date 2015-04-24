using System.Collections.Generic;
using System.Linq;

namespace PageRank
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">T is the item identify type</typeparam>
    internal class PageRank<T>
    {
        private MapReduce<T> _mapReduce;
        private readonly double _stayProbability;
        private readonly double _averageRank;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount">项目总数</param>
        /// <param name="stayProbability">保持留在某个项目, 不跳转的概率</param>
        public PageRank(int totalCount, double stayProbability = 0.8)
        {
            _mapReduce = new MapReduce<T>();
            _stayProbability = stayProbability;
            _averageRank = 1.0 / totalCount;
        }

        /// <summary>
        ///     计算下一轮PageRank
        /// </summary>
        public void NextCircle()
        {
            _mapReduce = new MapReduce<T>();
        }

        /// <summary>
        ///     计算一条行列式
        /// </summary>
        /// <param name="sparseMatrix">稀疏矩阵</param>
        public void Calc(SparseMatrix<T> sparseMatrix)
        {
            var outputRank = 1.0 / sparseMatrix.LinkedItems.Count;
            foreach (var item in sparseMatrix.LinkedItems)
            {
                _mapReduce.Reduce(item,
                    _stayProbability * outputRank * sparseMatrix.Rank);
            }
            //当没有其它链接指向Head的时候, 以防漏项
            _mapReduce.Reduce(sparseMatrix.Head, (1 - _stayProbability) * _averageRank);
        }

        /// <summary>
        ///     一轮PageRank迭代之后, 获取最新的PageRank并更新
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double GetCurrentRank(T key)
        {
            return _mapReduce.GetOrSetDefaultValue(key);
        }
    }
}