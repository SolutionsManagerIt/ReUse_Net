using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ReUse.Base;

namespace ReUse.Common
{
    /// <summary>
    /// Common Collections Extensions
    /// </summary>
    public static class Collections_Extensions
    {
        #region IEnumerable

        #region Check 

        /// <summary>
        /// Perform common Check for List content :  List_To_Check is not null and have items; 
        /// List_Contains_One_Not_Null_Item (have not null items); 
        /// Contains_Any_Value_From (have any item from this list); 
        /// Contains_All_Values_From (have all items from this list).
        /// </summary>        
        /// <returns>All specified checks ok</returns>
        public static bool C<T>(this IEnumerable<T> List_To_Check, bool List_Contains_One_Not_Null_Item = true, IEnumerable<T> Contains_Any_Value_From = null, IEnumerable<T> Contains_All_Values_From = null)
        {
            if (List_To_Check == null)
                return false;
            var q = List_To_Check.ToArray();  //  to avoid multiple functions processing in func arrays
            if (q != null && q.Length > 0)
                if (List_Contains_One_Not_Null_Item != true || q.Any(e => e != null))
                    if (Contains_Any_Value_From == null || q.Any(e => Contains_Any_Value_From.Contains(e)))
                        if (Contains_All_Values_From == null || !Contains_All_Values_From.Any(e => !q.Contains(e)))
                            return true;
            return false;
        }

        /// <summary>
        /// Perform common Check for List content :  List_To_Check is not null and have List_Contains_Item; 
        /// </summary>        
        /// <returns>All specified checks ok</returns>
        public static bool C<T>(this IEnumerable<T> List_To_Check, T List_Contains_Item)
        {
            if (List_To_Check == null)
                return false;
            return List_To_Check.Contains(List_Contains_Item);
        }


        #endregion

        #region Get 

        /// <summary>
        /// Perform common Get for ICollection data
        /// </summary>
        public static IEnumerable<T> G<T>(this ICollection<T> DataList, IEnumerable<T> DefaultValueOnEmpty = null, bool NotNullValues = true, bool Distinct = true, IEnumerable<T> ExceptList = null, int? TakeOnly = null)
        {
            if (!DataList.C(NotNullValues))
                return DefaultValueOnEmpty;

            var Result = DataList.Where(e => (e != null));

            if (!NotNullValues)
                Result = DataList.Select(e => e);

            if (Result.Count() == 0)
                return DefaultValueOnEmpty;

            if (Distinct)
                Result = Result.Distinct();

            if (ExceptList != null)
                Result = Result.Except(ExceptList);

            if (Result.C(NotNullValues))
                return Result;

            if (TakeOnly != null)
                Result = Result.Take(TakeOnly.Value);

            if (Result.C(NotNullValues))
                return Result;

            return DefaultValueOnEmpty;
        }

        /// <summary>
        /// Perform common Get for IEnumerable data
        /// </summary>
        public static IEnumerable<T> G<T>(this IEnumerable<T> DataList, IEnumerable<T> DefaultValueOnEmpty = null, bool NotNullValues = true, bool Distinct = true, IEnumerable<T> ExceptList = null, int? TakeOnly = null)
        {
            if (!DataList.C(NotNullValues))
                return DefaultValueOnEmpty;

            var Result = DataList.Where(e => (e != null));

            if (!NotNullValues)
                Result = DataList.Select(e => e);

            if (Result.Count() == 0)
                return DefaultValueOnEmpty;

            if (Distinct)
                Result = Result.Distinct();

            if (ExceptList != null)
                Result = Result.Except(ExceptList);

            if (Result.C(NotNullValues))
                return Result;

            if (TakeOnly != null)
                Result = Result.Take(TakeOnly.Value);

            if (Result.C(NotNullValues))
                return Result;

            return DefaultValueOnEmpty;
        }

        /// <summary>
        /// Perform common Concat for IEnumerable data
        /// </summary>
        public static IEnumerable<T> Gc<T>(this IEnumerable<T> ArrayNo_0, IEnumerable<T> ArrayNo_1, IEnumerable<T> ArrayNo_2 = null, IEnumerable<T> ArrayNo_3 = null, params IEnumerable<T>[] MoreArrays)
        {
            var Result = new T[0].AsEnumerable();
            if (ArrayNo_0.C())
                Result = Result.Concat(ArrayNo_0);
            if (ArrayNo_1.C())
                Result = Result.Concat(ArrayNo_1);
            if (ArrayNo_2.C())
                Result = Result.Concat(ArrayNo_2);
            if (ArrayNo_3.C())
                Result = Result.Concat(ArrayNo_3);
            if (MoreArrays.C())
                foreach (var Array in MoreArrays)
                    if (Array.C())
                        Result = Result.Concat(Array);
            return Result;
        }

        /// <summary>
        /// Create common ForEach Loop to process List from IList
        /// </summary>
        public static IEnumerable<ResT> G<ValueT, ResT>(this IEnumerable<ValueT> DataToProcess, f<KeyValuePair<int, ValueT>, Cx?, ResT> FunctionToProcessValue, Cx? CurrCodeType = null, IEnumerable<ResT> ReturnOnError = null)
        {
            if (!DataToProcess.C() || FunctionToProcessValue == null)
                return ReturnOnError;

            int Counter = 0;
            return DataToProcess.Select(e => FunctionToProcessValue((Counter++)._K(e), CurrCodeType));
        }

        /// <summary>
        /// Create common ForEach Loop to process List from IList
        /// </summary>
        public static IEnumerable<ResT> G<KeyT, ValueT, ResT>(this IEnumerable<KeyT> DataToProcess, f<KeyT, Cx?, ValueT> FunctionToGetValue, f<KeyT, ValueT, Cx?, ResT> FunctionToProcessValue, Cx? CurrCodeType = null, IEnumerable<ResT> ReturnOnError = null)
        {
            if (!DataToProcess.C() || FunctionToGetValue == null || FunctionToProcessValue == null)
                return ReturnOnError;

            return DataToProcess.Select(e => FunctionToProcessValue(e, FunctionToGetValue(e, CurrCodeType), CurrCodeType));
        }

        /// <summary>
        /// Try get Curr ICollection Item with ItemNo (of return DefaultValueOnError)
        /// </summary>
        public static T G<T>(this ICollection<T> CurrObjectFields, int ItemNo = 0, T DefaultValueOnError = default(T))
        {
            if (CurrObjectFields != null && CurrObjectFields.Count > 0 && CurrObjectFields.Count < ItemNo)
                return CurrObjectFields.ElementAt(ItemNo)._T(DefaultValueOnError);

            return DefaultValueOnError;
        }


        #endregion

        #region Get Paging Data Utils

        /// <summary>
        /// Return a page (of size PageSize) no PageNo from DataToPage
        /// </summary>
        public static IEnumerable<T> _P<T>(this IEnumerable<T> DataToPage, int PageSize = 1000, int PageNo = 0)
        {
            return DataToPage.Skip(PageNo * PageSize).Take(PageSize);
        }

        /// <summary>
        /// Return a page (of size PageSize) no PageNo from IQueryable DataToPage
        /// </summary>
        public static IQueryable<T> _Pq<T>(this IQueryable<T> DataToPage, int PageSize = 1000, int PageNo = 0)
        {
            return DataToPage.Skip(PageNo * PageSize).Take(PageSize);
        }

        /// <summary>
        /// Return all pages (of size PageSize) of data from DataToPage
        /// </summary>
        public static IEnumerable<IEnumerable<T>> _Ps<T>(this IEnumerable<T> DataToPage, int PageSize = 1000)
        {
            if (DataToPage == null || PageSize < 1)
                yield return null;

            using (var enumerator = DataToPage.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(PageSize)
                    {
                        enumerator.Current
                    };
                    while (currentPage.Count < PageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }

        #endregion

        #region Set 

        #endregion

        #endregion

        #region IDictionary

        #region Check 


        /// <summary>
        /// Dictionary Check
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="Dictionary_To_Check"></param>
        /// <param name="Keys_Contains_One_Not_Null_Item"></param>
        /// <param name="Values_Contains_One_Not_Null_Item"></param>
        /// <returns></returns>     
        public static bool C<T1, T2>(this IDictionary<T1, T2> Dictionary_To_Check, bool Keys_Contains_One_Not_Null_Item = true, bool Values_Contains_One_Not_Null_Item = false)
        {
            if (Dictionary_To_Check != null)
                if (!Keys_Contains_One_Not_Null_Item || Dictionary_To_Check.Count > 0)
                    if (!Values_Contains_One_Not_Null_Item || Dictionary_To_Check.Values.Any(e => e != null))
                        return true;
            return false;
        }

        /// <summary>
        /// Dictionary Check Keys_Contains
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="Dictionary_To_Check"></param>
        /// <param name="Keys_Contains"></param>
        /// <returns></returns>     
        public static bool C<T1, T2>(this IDictionary<T1, T2> Dictionary_To_Check, T1 Keys_Contains)
        {
            if (Dictionary_To_Check != null)
                if (Keys_Contains != null && Dictionary_To_Check.ContainsKey(Keys_Contains))
                    return true;
            return false;
        }

        #endregion

        #region Get 

        /// <summary>
        /// Perform common Concat for IDictionary data
        /// </summary>
        public static IDictionary<KeyT, ResT> Gc<KeyT, ResT>(this IDictionary<KeyT, ResT> DictNo_0, IDictionary<KeyT, ResT> DictNo_1, IDictionary<KeyT, ResT> DictNo_2 = null, IDictionary<KeyT, ResT> DictNo_3 = null, params IDictionary<KeyT, ResT>[] MoreDicts)
        {
            var Result = new List<KeyValuePair<KeyT, ResT>>();
            if (DictNo_0.C())
                foreach (var item in DictNo_0)
                    Result.Add(item);
            if (DictNo_1.C())
                foreach (var item in DictNo_1)
                    Result.Add(item);
            if (DictNo_2.C())
                foreach (var item in DictNo_2)
                    Result.Add(item);
            if (DictNo_3.C())
                foreach (var item in DictNo_3)
                    Result.Add(item);
            if (MoreDicts.C())
                foreach (var Dict in MoreDicts)
                    if (Dict.C())
                        foreach (var item in Dict)
                            Result.Add(item);

            if (!Result.C())
                return DictNo_0;

            return Result.ToLookup(p => p.Key, p => p.Value).ToDictionary(p => p.Key, p => p.FirstOrDefault());
        }

        ///// <summary>
        ///// Create common ForEach Loop to process Dictionary from IDictionary
        ///// </summary>
        //public static IDictionary<KeyT, ResT> G<KeyT, ValueT, ResT>(this IDictionary<KeyT, ValueT> DataToProcess, f<KeyT, ValueT, Cx?, ResT> FunctionToProcess, Cx? CurrCodeType = null, IDictionary<KeyT, ResT> ReturnOnError = null)
        //{
        //    if (!DataToProcess.C() || FunctionToProcess == null)
        //        return ReturnOnError;

        //    var Res = N.D<KeyT, ResT>();
        //    foreach (var item in DataToProcess)
        //        Res.Add(item.Key, FunctionToProcess(item.Key, item.Value, CurrCodeType));

        //    return Res;
        //}

        ///// <summary>
        ///// Try get Curr IDictionary Value with KeyField (of return DefaultValueOnError)
        ///// </summary>
        //public static T2 G<T1, T2>(this IDictionary<T1, T2> CurrObjectFields, T1 KeyField, T2 DefaultValueOnError = default(T2))
        //{
        //    if (CurrObjectFields != null && CurrObjectFields.Count > 0 && CurrObjectFields.ContainsKey(KeyField))
        //        return CurrObjectFields[KeyField]._T(DefaultValueOnError);

        //    return DefaultValueOnError;
        //}

        #endregion

        #region Set 

        #endregion

        #endregion
    }
}
