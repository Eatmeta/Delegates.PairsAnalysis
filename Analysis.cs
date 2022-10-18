using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates.PairsAnalysis
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> source)
        {
            T item1 = default, item2 = default;
            var index = 0;
            foreach (var current in source)
            {
                switch (index)
                {
                    case 0:
                        item1 = current;
                        index++;
                        continue;
                    case 1:
                        item2 = current;
                        index++;
                        yield return Tuple.Create(item1, item2);
                        continue;
                }
                item1 = item2;
                item2 = current;
                yield return Tuple.Create(item1, item2);
                index++;
            }
            if (index < 2)
                throw new InvalidOperationException();
        }

        public static int MaxIndex<T>(this IEnumerable<T> source)
            where T : IComparable
        {
            T max = default;
            var bestIndex = 0;
            var index = 0;
            foreach (var item in source)
            {
                if (index == 0)
                {
                    max = item;
                    index++;
                    continue;
                }
                if (item.CompareTo(max) > 0)
                {
                    max = item;
                    bestIndex = index;
                }
                index++;
            }
            if (index == 0)
                throw new InvalidOperationException();

            return bestIndex;
        }
    }

    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
        {
            return data.Pairs().Select(x => x.Item2 - x.Item1).MaxIndex();
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            return data.Pairs().Select(x => (x.Item2 - x.Item1) / x.Item1).Average();
        }
    }
}