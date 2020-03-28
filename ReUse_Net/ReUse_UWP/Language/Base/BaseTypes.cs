using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReUse_UWP.Common;

namespace ReUse_UWP.Base
{
    /// <summary>
    /// Abstract Common Structs and Data Utilities -  Extensions version
    /// </summary>
    public static class BaseTypes
    {
        #region Logic Operations

        /// <summary>
        /// Checks Or Logic - true if any of values are true
        /// </summary>        
        public static bool o(this bool Value_1, bool Value_2, bool Value_3 = false, bool Value_4 = false, bool Value_5 = false, bool Value_6 = false, bool Value_7 = false, bool Value_8 = false, bool Value_9 = false, bool Value_10 = false, params bool[] MoreValues)
        {
            var Result = Value_1 || Value_2 || Value_3 || Value_4 || Value_5 || Value_6 || Value_7 || Value_8 || Value_9 || Value_10;
            if (MoreValues.C())
                foreach (var Value in MoreValues)
                    Result = Result || Value;
            return Result;
        }

        /// <summary>
        /// Checks And Logic - true if all values are true
        /// </summary> 
        public static bool a(this bool Value_1, bool Value_2, bool Value_3 = true, bool Value_4 = true, bool Value_5 = true, bool Value_6 = true, bool Value_7 = true, bool Value_8 = true, bool Value_9 = true, bool Value_10 = true, params bool[] MoreValues)
        {
            var Result = Value_1 && Value_2 && Value_3 && Value_4 && Value_5 && Value_6 && Value_7 && Value_8 && Value_9 && Value_10;
            if (MoreValues.C())
                foreach (var Value in MoreValues)
                    Result = Result && Value;
            return Result;
        }

        /// <summary>
        /// Checks if all values are null
        /// </summary>          
        public static bool _na(this object Value_1, object Value_2, object Value_3 = null, object Value_4 = null, object Value_5 = null, object Value_6 = null, object Value_7 = null, object Value_8 = null, object Value_9 = null, object Value_10 = null, params object[] MoreValues)
        {
            var Result = Value_1 == null && Value_2 == null && Value_3 == null && Value_4 == null && Value_5 == null && Value_6 == null && Value_7 == null && Value_8 == null && Value_9 == null && Value_10 == null;
            if (MoreValues.C())
                foreach (var Value in MoreValues)
                    Result = Result && (Value == null);
            return Result;
        }

        #endregion

        #region Checks not null

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value)
        {
            return Value != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2)
        {
            return Value_1 != null && Value_2 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3)
        {
            return Value_1 != null && Value_2 != null && Value_3 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3, object Value_4)
        {
            return Value_1 != null && Value_2 != null && Value_3 != null && Value_4 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3, object Value_4, object Value_5)
        {
            return Value_1 != null && Value_2 != null && Value_3 != null && Value_4 != null && Value_5 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3, object Value_4, object Value_5, object Value_6)
        {
            return Value_1 != null && Value_2 != null && Value_3 != null && Value_4 != null && Value_5 != null && Value_6 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3, object Value_4, object Value_5, object Value_6, object Value_7)
        {
            return Value_1 != null && Value_2 != null && Value_3 != null && Value_4 != null && Value_5 != null && Value_6 != null && Value_7 != null;
        }

        /// <summary>
        /// Checks all Parameters are not null
        /// </summary>
        public static bool _n(this object Value_1, object Value_2, object Value_3, object Value_4, object Value_5, object Value_6, object Value_7, params object[] MoreValues)
        {
            var Result = Value_1 != null && Value_2 != null && Value_3 != null && Value_4 != null && Value_5 != null && Value_6 != null && Value_7 != null;
            if (MoreValues.C())
                foreach (var Value in MoreValues)
                    Result = Result && (Value == null);
            return Result;
        }

        #endregion
    }
}
