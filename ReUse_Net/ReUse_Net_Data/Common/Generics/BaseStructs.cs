using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReUse.Common
{
    /// <summary>
    /// Common Base Structs Utilities
    /// </summary>
    public static class BaseStructs_Utilities
    {
        #region Get Common struct types

        /// <summary>
        /// Get Common struct type with 2 fields
        /// </summary>
        public static s<T1, T2> _s<T1, T2>(this T1 Val1, T2 Val2 = default(T2))
        {
            return new s<T1, T2>() { _1 = Val1, _2 = Val2 };
        }

        /// <summary>
        /// Get Common struct type with 3 fields
        /// </summary>
        public static s<T1, T2, T3> _s<T1, T2, T3>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3))
        {
            return new s<T1, T2, T3>() { _1 = Val1, _2 = Val2, _3 = Val3 };
        }

        /// <summary>
        /// Get Common struct type with 4 fields
        /// </summary>
        public static s<T1, T2, T3, T4> _s<T1, T2, T3, T4>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4))
        {
            return new s<T1, T2, T3, T4>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4 };
        }

        /// <summary>
        /// Get Common struct type with 5 fields
        /// </summary>
        public static s<T1, T2, T3, T4, T5> _s<T1, T2, T3, T4, T5>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5))
        {
            return new s<T1, T2, T3, T4, T5>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5 };
        }

        /// <summary>
        /// Get Common struct type with 6 fields
        /// </summary>
        public static s<T1, T2, T3, T4, T5, T6> _s<T1, T2, T3, T4, T5, T6>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6))
        {
            return new s<T1, T2, T3, T4, T5, T6>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6 };
        }

        /// <summary>
        /// Get Common struct type with 7 fields
        /// </summary>
        public static s<T1, T2, T3, T4, T5, T6, T7> _s<T1, T2, T3, T4, T5, T6, T7>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7))
        {
            return new s<T1, T2, T3, T4, T5, T6, T7>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7 };
        }

        /// <summary>
        /// Get Common struct type with 8 fields
        /// </summary>
        public static s<T1, T2, T3, T4, T5, T6, T7, T8> _s<T1, T2, T3, T4, T5, T6, T7, T8>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7), T8 Val8 = default(T8))
        {
            return new s<T1, T2, T3, T4, T5, T6, T7, T8>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7, _8 = Val8 };
        }

        /// <summary>
        /// Get Common struct type with 9 fields
        /// </summary>
        public static s<T1, T2, T3, T4, T5, T6, T7, T8, T9> _s<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7), T8 Val8 = default(T8), T9 Val9 = default(T9))
        {
            return new s<T1, T2, T3, T4, T5, T6, T7, T8, T9>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7, _8 = Val8, _9 = Val9 };
        }

        #endregion

        #region Get Common class types

        /// <summary>
        /// Get Common class type with 2 fields
        /// </summary>
        public static c<T1, T2> _c<T1, T2>(this T1 Val1, T2 Val2 = default(T2))
        {
            return new c<T1, T2>() { _1 = Val1, _2 = Val2 };
        }

        /// <summary>
        /// Get Common class type with 3 fields
        /// </summary>
        public static c<T1, T2, T3> _c<T1, T2, T3>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3))
        {
            return new c<T1, T2, T3>() { _1 = Val1, _2 = Val2, _3 = Val3 };
        }

        /// <summary>
        /// Get Common class type with 4 fields
        /// </summary>
        public static c<T1, T2, T3, T4> _c<T1, T2, T3, T4>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4))
        {
            return new c<T1, T2, T3, T4>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4 };
        }

        /// <summary>
        /// Get Common class type with 5 fields
        /// </summary>
        public static c<T1, T2, T3, T4, T5> _c<T1, T2, T3, T4, T5>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5))
        {
            return new c<T1, T2, T3, T4, T5>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5 };
        }

        /// <summary>
        /// Get Common class type with 6 fields
        /// </summary>
        public static c<T1, T2, T3, T4, T5, T6> _c<T1, T2, T3, T4, T5, T6>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6))
        {
            return new c<T1, T2, T3, T4, T5, T6>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6 };
        }

        /// <summary>
        /// Get Common class type with 7 fields
        /// </summary>
        public static c<T1, T2, T3, T4, T5, T6, T7> _c<T1, T2, T3, T4, T5, T6, T7>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7))
        {
            return new c<T1, T2, T3, T4, T5, T6, T7>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7 };
        }

        /// <summary>
        /// Get Common class type with 8 fields
        /// </summary>
        public static c<T1, T2, T3, T4, T5, T6, T7, T8> _c<T1, T2, T3, T4, T5, T6, T7, T8>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7), T8 Val8 = default(T8))
        {
            return new c<T1, T2, T3, T4, T5, T6, T7, T8>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7, _8 = Val8 };
        }

        /// <summary>
        /// Get Common class type with 9 fields
        /// </summary>
        public static c<T1, T2, T3, T4, T5, T6, T7, T8, T9> _c<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this T1 Val1, T2 Val2 = default(T2), T3 Val3 = default(T3), T4 Val4 = default(T4), T5 Val5 = default(T5), T6 Val6 = default(T6), T7 Val7 = default(T7), T8 Val8 = default(T8), T9 Val9 = default(T9))
        {
            return new c<T1, T2, T3, T4, T5, T6, T7, T8, T9>() { _1 = Val1, _2 = Val2, _3 = Val3, _4 = Val4, _5 = Val5, _6 = Val6, _7 = Val7, _8 = Val8, _9 = Val9 };
        }

        #endregion
    }

    #region structs and definitions

    #region function delegate

    #region void

    /// <summary>
    /// Common void function delegate with no parameters
    /// </summary>
    public delegate void v();

    /// <summary>
    /// Common void function delegate with 1 parameter
    /// </summary>
    public delegate void v<in T1>(T1 arg1);
    /// <summary>
    /// Common void function delegate with 2 parameters
    /// </summary>
    public delegate void v<in T1, in T2>(T1 arg1, T2 arg2);
    /// <summary>
    /// Common void function delegate with 3 parameters
    /// </summary>
    public delegate void v<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3);

    #endregion

    #region common function

    /// <summary>
    /// Common function delegate with no parameters
    /// </summary>
    public delegate TResult f<out TResult>();
    /// <summary>
    /// Common function delegate with 1 parameter
    /// </summary>
    public delegate TResult f<in T1, out TResult>(T1 arg1);
    /// <summary>
    /// Common function delegate with 2 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
    /// <summary>
    /// Common function delegate with 3 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);
    /// <summary>
    /// Common function delegate with 4 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// Common function delegate with 5 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, in T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    /// <summary>
    /// Common function delegate with 6 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, in T5, in T6, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    /// <summary>
    /// Common function delegate with 7 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, in T5, in T6, in T7, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    /// <summary>
    /// Common function delegate with 8 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    /// <summary>
    /// Common function delegate with 9 parameters
    /// </summary>
    public delegate TResult f<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    #endregion

    #region void ref

    /// <summary>
    /// Common void ref function delegate with 1 parameter
    /// </summary>
    public delegate void fr<T1>(ref T1 arg1);
    /// <summary>
    /// Common void ref function delegate with 2 parameters
    /// </summary>
    public delegate void frv<T1, in T2>(ref T1 arg1, T2 arg2);

    #endregion

    #region common ref function
    /// <summary>
    /// Common ref function delegate with 1 parameter
    /// </summary>
    public delegate TResult fr<T1, out TResult>(ref T1 arg1);
    /// <summary>
    /// Common ref function delegate with 2 parameters
    /// </summary>
    public delegate TResult fr<T1, in T2, out TResult>(ref T1 arg1, T2 arg2);
    /// <summary>
    /// Common ref function delegate with 3 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, in T3, out TResult>(ref T1 arg1, ref T2 arg2, T3 arg3);
    /// <summary>
    /// Common ref function delegate with 4 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, in T3, in T4, out TResult>(ref T1 arg1, ref T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// Common ref function delegate with 5 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, T3, in T4, in T5, out TResult>(ref T1 arg1, ref T2 arg2, ref T3 arg3, T4 arg4, T5 arg5);
    /// <summary>
    /// Common ref function delegate with 6 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, T3, in T4, in T5, in T6, out TResult>(ref T1 arg1, ref T2 arg2, ref T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    /// <summary>
    /// Common ref function delegate with 7 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, T3, T4, in T5, in T6, in T7, out TResult>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    /// <summary>
    /// Common ref function delegate with 8 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, T3, T4, in T5, in T6, in T7, in T8, out TResult>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    /// <summary>
    /// Common ref function delegate with 9 parameters
    /// </summary>
    public delegate TResult fr<T1, T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TResult>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    #endregion

    #region common out function
    /// <summary>
    /// Common out function delegate with 1 parameter
    /// </summary>
    public delegate TResult fo<T1, out TResult>(out T1 arg1);
    /// <summary>
    /// Common out function delegate with 2 parameters
    /// </summary>
    public delegate TResult fo<T1, in T2, out TResult>(out T1 arg1, T2 arg2);
    /// <summary>
    /// Common out function delegate with 3 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, in T3, out TResult>(out T1 arg1, out T2 arg2, T3 arg3);
    /// <summary>
    /// Common out function delegate with 4 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, in T3, in T4, out TResult>(out T1 arg1, out T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// Common out function delegate with 5 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, T3, in T4, in T5, out TResult>(out T1 arg1, out T2 arg2, out T3 arg3, T4 arg4, T5 arg5);
    /// <summary>
    /// Common out function delegate with 6 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, T3, in T4, in T5, in T6, out TResult>(out T1 arg1, out T2 arg2, out T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    /// <summary>
    /// Common out function delegate with 7 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, T3, T4, in T5, in T6, in T7, out TResult>(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    /// <summary>
    /// Common out function delegate with 8 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, T3, T4, in T5, in T6, in T7, in T8, out TResult>(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    /// <summary>
    /// Common out function delegate with 9 parameters
    /// </summary>
    public delegate TResult fo<T1, T2, T3, T4, in T5, in T6, in T7, in T8, in T9, out TResult>(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

    #endregion

    #region common mixed functions

    /// <summary>
    /// Common out function delegate with 4 parameters
    /// </summary>
    public delegate TResult fc<in T1, in T2, T3, in T4, out TResult>(T1 arg1, T2 arg2, out T3 arg3, T4 arg4);

    #endregion

    #endregion

    #region Custom struct types

    /// <summary>
    /// Common struct type with 2 fields
    /// </summary>
    public struct s<T1, T2>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
    }

    /// <summary>
    /// Common struct type with 3 fields
    /// </summary>
    public struct s<T1, T2, T3>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
    }

    /// <summary>
    /// Common struct type with 4 fields
    /// </summary>
    public struct s<T1, T2, T3, T4>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
    }

    /// <summary>
    /// Common struct type with 5 fields
    /// </summary>
    public struct s<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
    }

    /// <summary>
    /// Common struct type with 6 fields
    /// </summary>
    public struct s<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
    }

    /// <summary>
    /// Common struct type with 7 fields
    /// </summary>
    public struct s<T1, T2, T3, T4, T5, T6, T7>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
    }

    /// <summary>
    /// Common struct type with 8 fields
    /// </summary>
    public struct s<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
        /// <summary>
        /// field no 8
        /// </summary>
        public T8 _8;
    }

    /// <summary>
    /// Common struct type with 9 fields
    /// </summary>
    public struct s<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
        /// <summary>
        /// field no 8
        /// </summary>
        public T8 _8;
        /// <summary>
        /// field no 9
        /// </summary>
        public T9 _9;
    }

    #endregion

    #region Custom class types

    /// <summary>
    /// Common class type with 2 fields
    /// </summary>
    public class c<T1, T2>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
    }

    /// <summary>
    /// Common class type with 3 fields
    /// </summary>
    public class c<T1, T2, T3>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
    }

    /// <summary>
    /// Common class type with 4 fields
    /// </summary>
    public class c<T1, T2, T3, T4>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
    }

    /// <summary>
    /// Common class type with 5 fields
    /// </summary>
    public class c<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
    }

    /// <summary>
    /// Common class type with 6 fields
    /// </summary>
    public class c<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
    }

    /// <summary>
    /// Common class type with 7 fields
    /// </summary>
    public class c<T1, T2, T3, T4, T5, T6, T7>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
    }

    /// <summary>
    /// Common class type with 8 fields
    /// </summary>
    public class c<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
        /// <summary>
        /// field no 8
        /// </summary>
        public T8 _8;
    }

    /// <summary>
    /// Common class type with 9 fields
    /// </summary>
    public class c<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        /// <summary>
        /// field no 1
        /// </summary>
        public T1 _1;
        /// <summary>
        /// field no 2
        /// </summary>
        public T2 _2;
        /// <summary>
        /// field no 3
        /// </summary>
        public T3 _3;
        /// <summary>
        /// field no 4
        /// </summary>
        public T4 _4;
        /// <summary>
        /// field no 5
        /// </summary>
        public T5 _5;
        /// <summary>
        /// field no 6
        /// </summary>
        public T6 _6;
        /// <summary>
        /// field no 7
        /// </summary>
        public T7 _7;
        /// <summary>
        /// field no 8
        /// </summary>
        public T8 _8;
        /// <summary>
        /// field no 9
        /// </summary>
        public T9 _9;
    }

    #endregion

    #region Common data processing struct

    /// <summary>
    /// Common data processing struct
    /// </summary>
    public static class Processing_Utilities //_p<t>
    {
        #region Get

        /// <summary>
        /// Process Get Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static T _g<C, T>(this C CurrRootElem, f<C, T> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, T DefaultValue = default(T), bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return DefaultValue;
            var e = CurrRootElem;
            if (MethodToCheckOnInit != null && !MethodToCheckOnInit(e))
                return DefaultValue;
            if (MethodToPreProcess != null)
                MethodToPreProcess(ref e);
            if (StopIfNull && e == null)
                return DefaultValue;
            var r = MethodToProcess(e);
            if (StopIfNull && r == null)
                return DefaultValue;
            return r;
        }

        /// <summary>
        /// Process Get with chaining (return CurrRootElem) Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static C _gc<C>(this C CurrRootElem, v<C> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return CurrRootElem;

            CurrRootElem._g(c =>
            {
                MethodToProcess(c);
                return true;
            }, MethodToPreProcess, MethodToCheckOnInit, false, StopIfNull);
            return CurrRootElem;
        }

        /// <summary>
        /// Process Get List of Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static IEnumerable<T> _g<C, D, T>(this C CurrRootElem, f<C, IEnumerable<D>> MethodToGetData, f<D, T> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, T DefaultValue = default(T), IEnumerable<T> DefaultResult = null, bool StopIfNull = true)
        {
            if (MethodToGetData == null || CurrRootElem == null || MethodToProcess == null)
                return DefaultResult;

            return CurrRootElem._g(c =>
            {
                var d = MethodToGetData(c);
                if (StopIfNull && !d.C())
                    return DefaultResult;
                var r = d.G((i, ct) => MethodToProcess(i.Value));
                if (StopIfNull && !r.C())
                    return DefaultResult;
                return r;
            }, MethodToPreProcess, MethodToCheckOnInit, DefaultResult, StopIfNull);
        }

        /// <summary>
        /// Process Get List of Common Data with chaining (return CurrRootElem) From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static C _gc<C, D>(this C CurrRootElem, f<C, IEnumerable<D>> MethodToGetData, v<D> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return CurrRootElem;

            CurrRootElem._g(MethodToGetData, c =>
            {
                MethodToProcess(c);
                return true;
            }, MethodToPreProcess, MethodToCheckOnInit, false, null, StopIfNull);
            return CurrRootElem;
        }

        #endregion

        #region Set

        /// <summary>
        /// Process Set Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static T _s<C, T>(this C CurrRootElem, fr<C, T> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, T DefaultValue = default(T), bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return DefaultValue;
            var e = CurrRootElem;
            if (MethodToCheckOnInit != null && !MethodToCheckOnInit(e))
                return DefaultValue;
            if (MethodToPreProcess != null)
                MethodToPreProcess(ref e);
            if (StopIfNull && e == null)
                return DefaultValue;
            var r = MethodToProcess(ref e);
            if (StopIfNull && r == null)
                return DefaultValue;
            return r;
        }

        /// <summary>
        /// Process Set with chaining (return CurrRootElem) Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static C _sc<C>(this C CurrRootElem, v<C> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return CurrRootElem;

            CurrRootElem._g(c =>
            {
                MethodToProcess(c);
                return true;
            }, MethodToPreProcess, MethodToCheckOnInit, false, StopIfNull);
            return CurrRootElem;
        }

        /// <summary>
        /// Process Set List of Common Data From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static IEnumerable<T> _s<C, D, T>(this C CurrRootElem, f<C, IEnumerable<D>> MethodToGetData, f<D, T> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, T DefaultValue = default(T), IEnumerable<T> DefaultResult = null, bool StopIfNull = true)
        {
            if (MethodToGetData == null || CurrRootElem == null || MethodToProcess == null)
                return DefaultResult;

            return CurrRootElem._g(c =>
            {
                var d = MethodToGetData(c);
                if (StopIfNull && d == null)
                    return DefaultResult;
                var r = d.G((i, ct) => MethodToProcess(i.Value));
                if (StopIfNull && r == null)
                    return DefaultResult;
                return r;
            }, MethodToPreProcess, MethodToCheckOnInit, DefaultResult, StopIfNull);
        }

        /// <summary>
        /// Process Set List of Common Data with chaining (return CurrRootElem) From current CurrRootElem with MethodToProcess with optional MethodToPreProcess (or CurrRootElem), MethodToCheckOnInit and StopIfNull (to check for nulls)
        /// </summary>
        public static C _sc<C, D>(this C CurrRootElem, f<C, IEnumerable<D>> MethodToGetData, v<D> MethodToProcess, fr<C> MethodToPreProcess = null, f<C, bool> MethodToCheckOnInit = null, bool StopIfNull = true)
        {
            if (MethodToProcess == null || CurrRootElem == null)
                return CurrRootElem;

            CurrRootElem._g(MethodToGetData, c =>
            {
                MethodToProcess(c);
                return true;
            }, MethodToPreProcess, MethodToCheckOnInit, false, null, StopIfNull);
            return CurrRootElem;
        }

        #endregion
    }

    #endregion

    #endregion
}
