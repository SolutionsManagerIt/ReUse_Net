using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Text;

namespace ReUse_Std.Common
{
    /// <summary>
    /// Common Linq Queries Extensions
    /// </summary>
    public static class Linq_Extensions
    {
        #region Common queries

        /// <summary>
        /// Perform common Linq Where Query with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<T> W<T>(this IEnumerable<T> Data, f<T, bool> WhereCondition, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            var r = (WhereCondition == null) ? Data : Data.Where(e => WhereCondition(e));
            if (OrderByCondition != null)
                r = r.OrderBy(e => OrderByCondition(e));
            if (Distinct)
                r = r.Distinct();
            if (TakeNRecords != null && TakeNRecords.Value > 0)
                r = r.Take(TakeNRecords.Value);
            return r;
        }

        /// <summary>
        /// Perform common Linq Where Query (to array) with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static T[] w<T>(this IEnumerable<T> Data, f<T, bool> WhereCondition, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            return Data.W(WhereCondition, OrderByCondition, Distinct, TakeNRecords).A();
        }

        /// <summary>
        /// Perform common Linq Any Query with AnyWhereCondition
        /// </summary>
        public static bool? A<T>(this IEnumerable<T> Data, f<T, bool> AnyWhereCondition)
        {
            if (Data == null || AnyWhereCondition == null)
                return null;

            return Data.Any(e => AnyWhereCondition(e));
        }

        /// <summary>
        /// Perform common Linq Select Where Query with SelectCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<R> S<T, R>(this IEnumerable<T> Data, f<T, R> SelectCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || SelectCondition == null)
                return null;
            return Data.W(WhereCondition, OrderByCondition, Distinct, TakeNRecords).Select(e => SelectCondition(e));
        }

        /// <summary>
        /// Perform common Linq Select Where Query with SelectCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<R> S<T, R>(this IEnumerable<T> Data, f<T, int, R> SelectCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || SelectCondition == null)
                return null;
            return Data.W(WhereCondition, OrderByCondition, Distinct, TakeNRecords).Select((e, i) => SelectCondition(e, i));
        }

        /// <summary>
        /// Perform common Linq Select Where Query (to array) with SelectCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static R[] s<T, R>(this IEnumerable<T> Data, f<T, R> SelectCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || SelectCondition == null)
                return null;
            return Data.S(SelectCondition, WhereCondition, OrderByCondition, Distinct, TakeNRecords).A();
        }

        /// <summary>
        /// Perform common Linq Select Where Query (to array) with SelectCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static R[] s<T, R>(this IEnumerable<T> Data, f<T, int, R> SelectCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || SelectCondition == null)
                return null;
            return Data.S(SelectCondition, WhereCondition, OrderByCondition, Distinct, TakeNRecords).A();
        }

        /// <summary>
        /// Perform common Linq GroupBy Where Query with GroupByCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<IGrouping<R, T>> G<T, R>(this IEnumerable<T> Data, f<T, R> GroupByCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || GroupByCondition == null)
                return null;
            return Data.W(WhereCondition, OrderByCondition, Distinct, TakeNRecords).GroupBy(e => GroupByCondition(e));
        }

        /// <summary>
        /// Perform common Linq GroupBy Where Query (to array) with GroupByCondition and optional WhereCondition, OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IGrouping<R, T>[] g<T, R>(this IEnumerable<T> Data, f<T, R> GroupByCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null || GroupByCondition == null)
                return null;
            return Data.G(GroupByCondition, WhereCondition, OrderByCondition, Distinct, TakeNRecords).A();
        }

        /// <summary>
        /// Perform common Linq Distinct Query with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<T> D<T>(this IEnumerable<T> Data, f<T, T, bool> DistinctCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            var r = Data.W(WhereCondition, OrderByCondition, true, TakeNRecords);
            if (DistinctCondition != null)
            {
                var q = new Eq<T>(DistinctCondition);
                r = r.Distinct(q);
            }

            return r;
        }

        /// <summary>
        /// Perform common Linq Distinct Query (to array) with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static T[] d<T>(this IEnumerable<T> Data, f<T, T, bool> DistinctCondition, f<T, bool> WhereCondition = null, f<T, double?> OrderByCondition = null, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            return Data.D(DistinctCondition, WhereCondition, OrderByCondition, TakeNRecords).A();

        }

        /// <summary>
        /// Perform common Linq OrderBy Query with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<T> O<T>(this IEnumerable<T> Data, f<T, int?> OrderByCondition, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            var r = Data;
            if (OrderByCondition != null)
                r = r.OrderBy(e => OrderByCondition(e));
            if (Distinct)
                r = r.Distinct();
            if (TakeNRecords != null && TakeNRecords.Value > 0)
                r = r.Take(TakeNRecords.Value);
            return r;
        }

        /// <summary>
        /// Perform common Linq OrderBy Query (to array) with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static T[] o<T>(this IEnumerable<T> Data, f<T, int?> OrderByCondition, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            return Data.O(OrderByCondition, Distinct, TakeNRecords).A();
        }


        /// <summary>
        /// Perform common Linq OrderBy Query with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static IEnumerable<T> O<T>(this IEnumerable<T> Data, f<T, double?> OrderByCondition, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            var r = Data;
            if (OrderByCondition != null)
                r = r.OrderBy(e => OrderByCondition(e));
            if (Distinct)
                r = r.Distinct();
            if (TakeNRecords != null && TakeNRecords.Value > 0)
                r = r.Take(TakeNRecords.Value);
            return r;
        }

        /// <summary>
        /// Perform common Linq OrderBy Query (to array) with WhereCondition and optional OrderByCondition, Distinct, TakeNRecords
        /// </summary>
        public static T[] o<T>(this IEnumerable<T> Data, f<T, double?> OrderByCondition, bool Distinct = false, int? TakeNRecords = null)
        {
            if (Data == null)
                return null;
            return Data.O(OrderByCondition, Distinct, TakeNRecords).A();
        }


        #endregion


    }

    #region Comparers structs

    /// <summary>
    /// Common Equality Comparer with CustomMethodToCompare and CustomMethodToGetHashCode
    /// </summary>
    public struct Eq<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Custom Method To Compare
        /// </summary>
        private f<T, T, bool> cm;

        /// <summary>
        /// Custom Method To Get Hash Code
        /// </summary>
        private f<T, int> gh;

        public Eq(f<T, T, bool> CustomMethodToCompare, f<T, int> CustomMethodToGetHashCode = null)
        {
            cm = CustomMethodToCompare;
            gh = CustomMethodToGetHashCode;
        }

        public bool Equals(T b1, T b2)
        {
            if (cm != null)
                return cm(b1, b2);
            else
                return false;
        }
        public int GetHashCode(T b1)
        {
            if (gh != null)
                return gh(b1);
            else
                return -1;
        }
    }

    /// <summary>
    /// Common Equality Comparer for object keys with CustomMethodToGetKeys and CustomMethodToGetHashCode
    /// </summary>
    public struct Eq<T, S> : IEqualityComparer<T> where S : IComparable
    {
        /// <summary>
        /// Custom Method To Get Keys
        /// </summary>
        private f<T, S> ck;

        /// <summary>
        /// Custom Method To Get Hash Code
        /// </summary>
        private f<T, int> gh;

        public Eq(f<T, S> CustomMethodToGetKeys, f<T, int> CustomMethodToGetHashCode = null)
        {
            ck = CustomMethodToGetKeys;
            gh = CustomMethodToGetHashCode;
        }

        public bool Equals(T b1, T b2)
        {
            if (ck != null)
                return ck(b1).CompareTo(ck(b2)) == 0;
            else
                return false;
        }
        public int GetHashCode(T b1)
        {
            if (gh != null)
                return gh(b1);
            else
                return -1;
        }
    }

    #endregion
}
