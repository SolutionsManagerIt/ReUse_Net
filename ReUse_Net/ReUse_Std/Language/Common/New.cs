using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ReUse_Std.Base;

namespace ReUse_Std.Common
{
    /// <summary>
    /// Fast Create New Structs Data Utilities
    /// </summary>
    public static class CreateNewStructsUtilities
    {
        #region List and Array Utilities

        #region Lists

        #region Add Items

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) from current ListToAdd and MoreItems if necessary
        /// </summary>
        public static List<T> a<T>(this List<T> CurrentList, IEnumerable<T> ListToAdd, params T[] MoreItems)
        {
            if (ListToAdd != null)
                CurrentList.AddRange(ListToAdd);
            if (MoreItems != null)
                CurrentList.AddRange(MoreItems);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0-5 and MoreItems if necessary
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4, T ItemNo_5, params T[] MoreItems)
        {
            var Result = new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4, ItemNo_5 };
            if (MoreItems != null)
                Result.AddRange(MoreItems);
            CurrentList.AddRange(Result);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0-4
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4)
        {
            CurrentList.Add(ItemNo_0);
            CurrentList.Add(ItemNo_1);
            CurrentList.Add(ItemNo_2);
            CurrentList.Add(ItemNo_3);
            CurrentList.Add(ItemNo_4);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0-3
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3)
        {
            CurrentList.Add(ItemNo_0);
            CurrentList.Add(ItemNo_1);
            CurrentList.Add(ItemNo_2);
            CurrentList.Add(ItemNo_3);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0-2
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0, T ItemNo_1, T ItemNo_2)
        {
            CurrentList.Add(ItemNo_0);
            CurrentList.Add(ItemNo_1);
            CurrentList.Add(ItemNo_2);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0-1
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0, T ItemNo_1)
        {
            CurrentList.Add(ItemNo_0);
            CurrentList.Add(ItemNo_1);
            return CurrentList;
        }

        /// <summary>
        /// Add Items to CurrentList (with chaining, return CurrentList) with current Items No 0
        /// </summary>
        public static List<T> A<T>(this List<T> CurrentList, T ItemNo_0)
        {
            CurrentList.Add(ItemNo_0);
            return CurrentList;
        }

        #endregion

        #region Create

        /// <summary>
        /// Create List with current Items No 0-5 and MoreItems if necessary
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4, T ItemNo_5, params T[] MoreItems)
        {
            var Result = new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4, ItemNo_5 };
            if (MoreItems.C())
                Result.AddRange(MoreItems);
            return Result;
        }

        /// <summary>
        /// Create List with current Items No 0-5
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4, T ItemNo_5 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4, ItemNo_5 };
        }

        /// <summary>
        /// Create List with current Items No 0-4
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4 };
        }

        /// <summary>
        /// Create List with current Items No 0-3
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3 };
        }

        /// <summary>
        /// Create List with current Items No 0-2
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2 };
        }

        /// <summary>
        /// Create List with current Items No 0-1
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0, T ItemNo_1 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1 };
        }

        /// <summary>
        /// Create List with current ItemNo_0
        /// </summary>
        public static List<T> L<T>(this T ItemNo_0)
        {
            return new List<T>() { ItemNo_0 };
        }

        /// <summary>
        /// Create Empty List with SampleItem Type
        /// </summary>
        public static List<T> Ls<T>(this T SampleItem)
        {
            return new List<T>();
        }

        /// <summary>
        /// Create new List with ListToAdd and MoreItems to add
        /// </summary>
        public static List<T> L<T>(this IEnumerable<T> ListToAdd, params T[] MoreItems)
        {
            var r = new List<T>();
            if (ListToAdd != null)
                r = ListToAdd.ToList();
            if (MoreItems != null)
                r.AddRange(MoreItems);
            return r;
        }
        #endregion

        #endregion

        #region IEnumerable

        /// <summary>
        /// Create IEnumerable with current Items No 0-5 and MoreItems if necessary
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4, T ItemNo_5, params T[] MoreItems)
        {
            var Result = new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4, ItemNo_5 };
            if (MoreItems.C())
                Result.AddRange(MoreItems);
            return Result;
        }

        /// <summary>
        /// Create IEnumerable with current Items No 0-5
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4, T ItemNo_5 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4, ItemNo_5 };
        }

        /// <summary>
        /// Create IEnumerable with current Items No 0-4
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3, T ItemNo_4 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3, ItemNo_4 };
        }

        /// <summary>
        /// Create IEnumerable with current Items No 0-3
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, T ItemNo_3 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2, ItemNo_3 };
        }

        /// <summary>
        /// Create IEnumerable with current Items No 0-2
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1, ItemNo_2 };
        }

        /// <summary>
        /// Create IEnumerable with current Items No 0-1
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0, T ItemNo_1 = default(T))
        {
            return new List<T>() { ItemNo_0, ItemNo_1 };
        }

        /// <summary>
        /// Create IEnumerable with current ItemNo_0
        /// </summary>
        public static IEnumerable<T> I<T>(this T ItemNo_0)
        {
            return new List<T>() { ItemNo_0 };
        }

        /// <summary>
        /// Create Empty IEnumerable with SampleItem Type
        /// </summary>
        public static IEnumerable<T> Is<T>(this T SampleItem)
        {
            return new List<T>();
        }

        /// <summary>
        /// Create Empty IEnumerable with Type
        /// </summary>
        public static IEnumerable<T> I<T>()
        {
            return new List<T>();
        }

        #endregion

        #region Arrays

        /// <summary>
        /// Create Empty Array with SampleItem Type and ItemsNo
        /// </summary>
        public static T[] As<T>(this T SampleItem, int ItemsNo)
        {
            if (ItemsNo < 0)
                return null;
            return new T[ItemsNo];
        }

        /// <summary>
        /// Create Empty Array with  ItemsNo
        /// </summary>
        public static T[] A<T>(this int ItemsNo)
        {
            if (ItemsNo < 0)
                return null;
            return new T[ItemsNo];
        }

        ///// <summary>
        ///// Create Array From current ImportDataFrom IEnumerable
        ///// </summary>
        //public static T[] A<T>(this IEnumerable<T> ImportDataFrom)
        //{
        //    if (ImportDataFrom == null)
        //        return null;
        //    return ImportDataFrom.ToArray();
        //}

        /// <summary>
        /// Create Array with current Items No 0-2  and more ItemsNo3_AndMore if necessary
        /// </summary>
        public static T[] A<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2, params T[] MoreItems)
        {
            var Result = new T[3] { ItemNo_0, ItemNo_1, ItemNo_2 };
            if (MoreItems.C())
                return Result.Gc(MoreItems).ToArray();
            return Result;
        }

        /// <summary>
        /// Create Array with current Items No 0-2
        /// </summary>
        public static T[] A<T>(this T ItemNo_0, T ItemNo_1, T ItemNo_2 = default(T))
        {
            return new T[3] { ItemNo_0, ItemNo_1, ItemNo_2 };
        }

        /// <summary>
        /// Create Array with current Items No 0-1
        /// </summary>
        public static T[] A<T>(this T ItemNo_0, T ItemNo_1 = default(T))
        {
            return new T[2] { ItemNo_0, ItemNo_1 };
        }

        /// <summary>
        /// Create Array with current ItemNo_0
        /// </summary>
        public static T[] A<T>(this T ItemNo_0)
        {
            return new T[1] { ItemNo_0 };
        }

        #endregion

        #endregion

        #region Dictionary and KeyValuePair Utilities

        /// <summary>
        /// Add to CurrentDictionary one item with current Key and Value with chaining
        /// </summary>
        public static IDictionary<KeyT, ValueT> A<KeyT, ValueT>(this IDictionary<KeyT, ValueT> CurrentDictionary, KeyT Key, ValueT Value = default(ValueT))
        {
            CurrentDictionary.Add(Key, Value);
            return CurrentDictionary;
        }

        /// <summary>
        /// Add or Update (if Key exists) to CurrentDictionary one item with current Key and Value with chaining
        /// </summary>
        public static IDictionary<KeyT, ValueT> U<KeyT, ValueT>(this IDictionary<KeyT, ValueT> CurrentDictionary, KeyT Key, ValueT Value = default(ValueT))
        {
            if (!CurrentDictionary.ContainsKey(Key))
                CurrentDictionary.Add(Key, Value);
            else
                CurrentDictionary[Key] = Value;
            return CurrentDictionary;
        }

        ///// <summary>
        ///// Add to CurrentDictionary one item with current Key and Value with chaining using current Formatter
        ///// </summary>
        //public static IDictionary<string, T> A<T>(this IDictionary<string, T> CurrentDictionary, string Key, Frt Formatter, T Value = default(T))
        //{
        //    var f = Key;
        //    if (Formatter != null)
        //        f = Formatter.G(Key);
        //    if (f == null)
        //        return CurrentDictionary;

        //    if (!CurrentDictionary.ContainsKey(f))
        //        CurrentDictionary.Add(f, Value);
        //    else
        //        CurrentDictionary[f] = Value;
        //    return CurrentDictionary;
        //}


        /// <summary>
        /// Update values in CurrentDictionary with same Key and Value from UpdateFrom with chaining and optionally AddAdditionalValues
        /// </summary>
        public static IDictionary<KeyT, ValueT> U<KeyT, ValueT>(this IDictionary<KeyT, ValueT> CurrentDictionary, IDictionary<KeyT, ValueT> UpdateFrom, bool AddAdditionalValues = false)
        {
            if (UpdateFrom == null || CurrentDictionary == null)
                return CurrentDictionary;

            var k = CurrentDictionary.Keys.Where(e => UpdateFrom.ContainsKey(e));

            foreach (var v in k)
                CurrentDictionary[v] = UpdateFrom[v];

            if (AddAdditionalValues)
            {
                var ak = UpdateFrom.Keys.Where(e => !CurrentDictionary.ContainsKey(e));
                foreach (var v in ak)
                    CurrentDictionary.Add(v, UpdateFrom[v]);
            }

            return CurrentDictionary;
        }

        /// <summary>
        /// Update keys in CurrentDictionary from UpdateKeysFrom with chaining
        /// </summary>
        public static IDictionary<KeyT, ValueT> Uk<KeyT, ValueT>(this IDictionary<KeyT, ValueT> CurrentDictionary, IDictionary<KeyT, KeyT> UpdateKeysFrom)
        {
            if (UpdateKeysFrom == null || CurrentDictionary == null)
                return CurrentDictionary;

            var k = CurrentDictionary.Keys.Where(e => UpdateKeysFrom.ContainsKey(e) && UpdateKeysFrom[e] != null);

            foreach (var v in k)
                if (!CurrentDictionary.ContainsKey(UpdateKeysFrom[v]))
                {
                    CurrentDictionary.Add(UpdateKeysFrom[v], CurrentDictionary[v]);
                    CurrentDictionary.Remove(v);
                }

            return CurrentDictionary;
        }

        /// <summary>
        /// Add keys from values in CurrentDictionary from AddKeysFrom with chaining
        /// </summary>
        public static IDictionary<KeyT, ValueT> Ak<KeyT, ValueT>(this IDictionary<KeyT, ValueT> CurrentDictionary, IDictionary<KeyT, KeyT> AddKeysFrom)
        {
            if (AddKeysFrom == null || CurrentDictionary == null)
                return CurrentDictionary;

            foreach (var v in AddKeysFrom)
                if (!CurrentDictionary.ContainsKey(v.Value))
                    CurrentDictionary.Add(v.Value, default(ValueT));
                else
                    if (!CurrentDictionary.ContainsKey(v.Key))
                    CurrentDictionary.Add(v.Key, default(ValueT));

            return CurrentDictionary;
        }


        /// <summary>
        /// Add ValuePrefix to values in CurrentDictionary with chaining (for mapping parameters)
        /// </summary>
        public static IDictionary<string, string> Ap(this IDictionary<string, string> CurrentDictionary, string ValuePrefix)
        {
            if (!ValuePrefix.C() || CurrentDictionary == null)
                return CurrentDictionary;

            foreach (var v in CurrentDictionary)
                if (v.Value != null)
                    CurrentDictionary[v.Key] = ValuePrefix + v.Value;

            return CurrentDictionary;
        }

        /// <summary>
        /// Get Value by KeyValue in CurrentDictionary with chaining (for mapping parameters), ValuePrefix and DefaultValue
        /// </summary>
        public static string Gv(this IDictionary<string, string> CurrentDictionary, string KeyValue, string ValuePrefix = null, string DefaultValue = null)
        {
            if (KeyValue == null || CurrentDictionary == null)
                return ValuePrefix + (DefaultValue ?? KeyValue);

            var r = DefaultValue;

            if (CurrentDictionary.ContainsKey(KeyValue))
                r = CurrentDictionary[KeyValue];

            return ValuePrefix + r ?? KeyValue;
        }


        #region KeyValuePairs
        
        /// <summary>
        /// Create KeyValuePair with current Key and Value
        /// </summary>
        public static KeyValuePair<KeyT, ValueT> K<KeyT, ValueT>(this KeyT Key, ValueT Value = default(ValueT))
        {
            return new KeyValuePair<KeyT, ValueT>(Key, Value);
        } 
        
        /// <summary>
        /// Create Nullable KeyValuePair with current Key and Value
        /// </summary>
        public static KeyValuePair<KeyT, ValueT>? Kn<KeyT, ValueT>(this KeyT Key, ValueT Value = default(ValueT))
        {
            if (Key.Equals(null))
                return null;
            return new KeyValuePair<KeyT, ValueT>(Key, Value);
        }
        #endregion
        
        /// <summary>
        /// Create New Dictionary with one item with current Key and Value
        /// </summary>
        public static IDictionary<KeyT, ValueT> D<KeyT, ValueT>(this KeyT Key, ValueT Value = default(ValueT))
        {
            var res = new Dictionary<KeyT, ValueT>();
            res.Add(Key, Value);
            return res;
        }

        /// <summary>
        /// Create New Dictionary with two items with current Keys and Values
        /// </summary>
        public static IDictionary<KeyT, ValueT> D<KeyT, ValueT>(this KeyT Key1, KeyT Key2, ValueT Value1 = default(ValueT), ValueT Value2 = default(ValueT))
        {
            var res = new Dictionary<KeyT, ValueT>();
            res.Add(Key1, Value1);
            res.Add(Key2, Value2);
            return res;
        }

        /// <summary>
        /// Create New Dictionary with two items with current Keys and Values
        /// </summary>
        public static IDictionary<KeyT, ValueT> D<KeyT, ValueT>(this KeyT Key1, KeyT Key2, ValueT Value1, ValueT Value2, KeyValuePair<KeyT, ValueT> Val3, KeyValuePair<KeyT, ValueT>? Val4 = null, params KeyValuePair<KeyT, ValueT>[] MoreItems)
        {
            var res = new Dictionary<KeyT, ValueT>();
            res.Add(Key1, Value1);
            res.Add(Key2, Value2);
            res.Add(Val3.Key, Val3.Value);
            if (Val4 != null)
                res.Add(Val4.Value.Key, Val4.Value.Value);
            if (MoreItems.C())
                foreach (var item in MoreItems)
                    res.Add(item.Key, item.Value);
            return res;
        }

        /// <summary>
        /// Create New Dictionary with two items with current Keys and Func Method Values
        /// </summary>
        public static IDictionary<KeyT, ValueT> Df<KeyT, ValueT>(this KeyT Key1, KeyT Key2, f<KeyT, ValueT> Value1, f<KeyT, ValueT> Value2, KeyValuePair<KeyT, f<KeyT, ValueT>> Val3, KeyValuePair<KeyT, f<KeyT, ValueT>>? Val4 = null, params KeyValuePair<KeyT, f<KeyT, ValueT>>[] MoreItems)
        {
            var res = new Dictionary<KeyT, ValueT>();
            res.Add(Key1, Value1(Key1));
            res.Add(Key2, Value2(Key2));
            res.Add(Val3.Key, Val3.Value(Val3.Key));
            if (Val4 != null)
                res.Add(Val4.Value.Key, Val4.Value.Value(Val4.Value.Key));
            if (MoreItems.C())
                foreach (var item in MoreItems)
                    res.Add(item.Key, item.Value(item.Key));
            return res;
        }

        /// <summary>
        /// Create New Dictionary with two items with current Keys and Func Method Values
        /// </summary>
        public static IDictionary<KeyT, ValueT> Df<KeyT, ValueT>(this KeyT Key1, f<KeyT, ValueT> Value1)
        {
            var res = new Dictionary<KeyT, ValueT>();
            res.Add(Key1, Value1(Key1));
            return res;
        }

        #endregion

    }


    #region IDisposable Utilities

    /// <summary>
    /// Common Disposable class with custom OnDisposeMethods
    /// </summary>
    public class D<T> : IDisposable
    {
        // Pointer to an external unmanaged resource.
        //private IntPtr handle;
        // Other managed resource this class uses.
        //private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;

        /// <summary>
        /// UseTryCatch OnDisposeMethods
        /// </summary>
        protected bool _T = true;
        /// <summary>
        /// custom OnDisposeMethods
        /// </summary>
        protected f<T> _Dm;

        /// <summary>
        /// Init Disposable class with custom UseTryCatch OnDisposeMethods
        /// </summary>
        public D(f<T> OnDisposeMethod, bool UseTryCatch = true)
        {
            this._Dm = OnDisposeMethod;
            this._T = UseTryCatch;
        }

        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    //component.Dispose();
                    if (_Dm != null)
                        _Dm();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                //CloseHandle(handle);
                //handle = IntPtr.Zero;

                // Note disposing has been done.
                disposed = true;

            }
        }

        //// Use interop to call the method necessary
        //// to clean up the unmanaged resource.
        //[System.Runtime.InteropServices.DllImport("Kernel32")]
        //private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        //~_D()
        //{
        //    // Do not re-create Dispose clean-up code here.
        //    // Calling Dispose(false) is optimal in terms of
        //    // readability and maintainability.
        //    Dispose(false);
        //}
    }

    #endregion
}
