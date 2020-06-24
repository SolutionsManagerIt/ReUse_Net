using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReUse_Std.Base;
using ReUse_Std.Common;

namespace ReUse_Std.Base.Texts
{
    /// <summary>
    /// Text and string utilties
    /// </summary>
    public static class CommonText_Extensions
    {
        #region Check

        #region Strings

        /// <summary>
        /// Perform Common String Check
        /// </summary>      
        public static bool C(this string String_To_Check, bool NotEmpty = true, bool NotWhiteSpace = true)
        {
            if (String_To_Check != null)
                if (!NotEmpty || String_To_Check != "")
                    if (!NotWhiteSpace || !string.IsNullOrWhiteSpace(String_To_Check))
                        return true;
            return false;
        }

        /// <summary>
        /// Perform Common String Compare
        /// </summary> 
        public static bool Cs(this string String_To_Check, string String_To_Compare, bool Trim = true, bool IgnoreCase = false, bool NotWhiteSpace = true)
        {
            if (!String_To_Check.C(true, NotWhiteSpace))
                if (!String_To_Compare.C(true, NotWhiteSpace))
                    return true;

            bool? IgnoreCaseValue = null;
            if (IgnoreCase)
                IgnoreCaseValue = true;

            string Val_A = String_To_Check.T(Trim, IgnoreCaseValue);
            string Val_B = String_To_Compare.T(Trim, IgnoreCaseValue);

            if (Val_A == Val_B)
                return true;

            return false;
        }

        /// <summary>
        /// Perform Common String Length Check
        /// </summary> 
        public static bool Cl(this string String_To_Check, int? LimitMaxLength = null, int? LimitMinLength = null)
        {
            if (!String_To_Check.C())
                if (LimitMaxLength == null || String_To_Check.Length <= LimitMaxLength.Value)
                    if (LimitMinLength == null || String_To_Check.Length >= LimitMinLength.Value)
                        return true;
            return false;
        }

        /// <summary>
        /// Perform Common String Contains Check
        /// </summary> 
        public static bool Cc(this string String_To_Check, string Contains = null, string ContainsIn = null, IEnumerable<string> ContainsInList = null, IEnumerable<string> ContainsInValuesList = null)
        {
            if (!String_To_Check.C())
                if (!Contains.C() || String_To_Check.Contains(Contains))
                    if (!ContainsIn.C() || ContainsIn.Contains(String_To_Check))
                        if (ContainsInList == null || ContainsInList.Contains(String_To_Check))
                            if (ContainsInValuesList == null || ContainsInValuesList.Where(e => e != null && e.Contains(String_To_Check)).Count() > 0)
                                return true;
            return false;
        }

        #endregion

        #region List

        /// <summary>
        /// Perform Common String IEnumerable Check
        /// </summary> 
        public static bool Ct(this IEnumerable<string> List_To_Check, bool NotEmpty = true, bool NotNullValues = true, bool NotWhiteSpace = true)
        {
            if (List_To_Check.C(NotNullValues))
                if (List_To_Check.Where(e => !NotWhiteSpace || e.C(NotEmpty, NotNullValues)).C(NotNullValues))
                    return true;
            return false;
        }

        /// <summary>
        /// Perform Common String IEnumerable Contains Check
        /// </summary> 
        public static bool Cc(this IEnumerable<string> List_To_Check, IEnumerable<string> ContainsAny = null, IEnumerable<string> ContainsAll = null, bool NotNullValues = true, bool NotWhiteSpace = true)
        {
            if (List_To_Check.Ct(true, NotNullValues, NotWhiteSpace))
                if (List_To_Check.Where(e => !NotNullValues && !e.C(NotNullValues)).C(NotNullValues, ContainsAny, ContainsAll))
                    return true;
            return false;
        }

        #endregion

        #endregion

        #region Get

        /// <summary>
        /// Get data from DataList with Distinct Trim NotWhiteSpace TakeOnly filters except values from ExceptList
        /// </summary>
        public static IEnumerable<string> Gt(this IEnumerable<string> DataList, IEnumerable<string> DefaultValueOnEmpty = null, bool NotNullValues = true, bool Trim = true, bool NotWhiteSpace = true, bool Distinct = true, IEnumerable<string> ExceptList = null, int? TakeOnly = null)
        {
            if (!DataList.Ct(true, NotNullValues, NotWhiteSpace))
                return DefaultValueOnEmpty;

            var Result = DataList.G(DefaultValueOnEmpty, NotNullValues, Distinct, ExceptList);

            if (!Result.Ct(true, NotNullValues, NotWhiteSpace))
                return DefaultValueOnEmpty;

            Result = Result.Where(e => !NotWhiteSpace || !string.IsNullOrWhiteSpace(e)).Select(e => (e != null && Trim) ? e.Trim() : e).G(DefaultValueOnEmpty, NotNullValues, Distinct, ExceptList, TakeOnly);

            if (Result.Ct(true, NotNullValues, NotWhiteSpace))
                return Result;

            return DefaultValueOnEmpty;
        }

        /// <summary>
        /// Get data from string with Trim ToCaseUpper 
        /// </summary>
        public static string T(this string String_Source, bool Trim = true, bool? ToCaseUpper = null)
        {
            string Result = String_Source;

            if (Trim)
                Result = Result.Trim();

            if (ToCaseUpper == true)
                Result = Result.ToUpperInvariant();
            if (ToCaseUpper == false)
                Result = Result.ToLowerInvariant();
            return Result;
        }

        /// <summary>
        /// Get data from string with Replace Trim ToCaseUpper 
        /// </summary>
        public static string R(this string String_Source, string Pattern = null, string Replace = null, bool Trim = true, bool? ToCaseUpper = null)
        {
            string Result = String_Source;

            if (Trim)
                Result = Result.Trim();

            if (ToCaseUpper == true)
                Result = Result.ToUpperInvariant();
            if (ToCaseUpper == false)
                Result = Result.ToLowerInvariant();

            if (Pattern == null || !Pattern.C(true, false))
                return Result;

            return Result.Replace(Pattern, Replace ?? "");
        }

        /// <summary>
        /// Concatenate ConcatList to String with Delimiter
        /// </summary>
        public static string Gc(this IEnumerable<string> ConcatList, string Delimiter = null, bool Trim = true, bool SkipWhiteSpace = true)
        {
            string Result = "";
            if (ConcatList.Ct(true, false, SkipWhiteSpace))
                foreach (string item in ConcatList.Gt(new List<string>(), true, Trim, SkipWhiteSpace, false))
                    Result += item + Delimiter;

            return Result;
        }

        /// <summary>
        /// Get common values string From values and parameters list ParametersValuesList
        /// with specified Delimiter between records and ResultPrefix and ResultSuffix
        /// Sample  -  ( Val_1, Val_2, Val_3, .... Val_N_1, Val_N )
        /// </summary>        
        /// <returns>Result String or DefaultValueOnError</returns>
        public static string Gv(this IEnumerable<string> ParametersValuesList, string Delimiter = ", ", string ResultPrefix = "(", string ResultSuffix = ")", bool Distinct = true, bool Trim = true, bool SkipWhiteSpace = true, string DefaultValueOnEmpty = null)
        {
            if (!ParametersValuesList.C())
                return DefaultValueOnEmpty;

            string Result = "";

            var DistinctParams = ParametersValuesList.Gt(null, SkipWhiteSpace, Trim, SkipWhiteSpace, Distinct);
            if (!DistinctParams.C())
                return DefaultValueOnEmpty;
            int Counter = 0;

            foreach (var item in DistinctParams)
            {
                if (Counter == 0)
                    Result += item;
                else
                    Result += Delimiter + item;
                Counter++;
            }

            if (!string.IsNullOrEmpty(Result))
                return ResultPrefix + Result + ResultSuffix;

            return DefaultValueOnEmpty;
        }

        /// <summary>
        /// Get Substring current StringData with StartIndex and optional Length
        /// </summary>
        public static string S(this string StringData, int StartIndex, int? Length = null)
        {
            if (StringData == null)
                return null;
            var st = StartIndex;
            if(st < 0)
                st = 0;
            if (Length != null)
                return StringData.Substring(st, Length.Value);
            return StringData.Substring(st);
        }

        /// <summary>
        /// Format current String_Format with FormatValues
        /// </summary>
        /// <param name="String_Format"></param>
        /// <param name="FormatValues"></param>
        /// <returns></returns>
        public static string F(this string String_Format, params object[] FormatValues)
        {
            try
            {
                if (String_Format != null)
                    return string.Format(String_Format, FormatValues);
            }
            catch (Exception e)
            {

            }
            if (String_Format != null)
                return "Error formatting '" + String_Format + "', max parameters : " + FormatValues.Length;
            return String_Format;
        }

        /// <summary>
        /// Format current String_Format with FormatValues using custom template formatter {0}
        /// </summary>
        /// <param name="String_Format"></param>
        /// <param name="FormatValues"></param>
        /// <returns></returns>
        public static string Fc(this string String_Format, params object[] FormatValues)
        {
            if (String_Format == null)
                return String_Format;

            for (int i = 0, l = FormatValues.Length; i < l; i++)
            {
                var s = "{" + i + "}";  // "##${0}$##"
                if (String_Format.Contains(s))
                {
                    var o = FormatValues[i];
                    if (o != null)
                        String_Format = String_Format.Replace(s, o.ToString());
                    else
                        String_Format = String_Format.Replace(s, "");
                }
            }
            return String_Format;
        }

        /// <summary>
        /// Format current String_Format with FormatValues using custom template formatter
        /// </summary>
        /// <param name="String_Format"></param>
        /// <param name="FormatValues"></param>
        /// <returns></returns>
        public static string Ff(this string String_Format, string FormatTypeFormat = null, params object[] FormatValues)
        {
            if (String_Format == null)
                return String_Format;
            var f = FormatTypeFormat ?? "{{0}}"; // "##${0}$##"
            for (int i = 0, l = FormatValues.Length; i < l; i++)
            {
                var s = string.Format(f, i);
                if (String_Format.Contains(s))
                {
                    var o = FormatValues[i];
                    if (o != null)
                        String_Format = String_Format.Replace(s, o.ToString());
                    else
                        String_Format = String_Format.Replace(s, "");
                }
            }
            return String_Format;
        }

        #endregion

        #region strings common

        /// <summary>
        /// Split CurrStringsData with Delimiters using Options
        /// </summary>
        /// <param name="CurrStringsData"></param>
        /// <param name="Delimiters"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public static string[] P(this string CurrStringsData, char[] Delimiters, bool RemoveEmptyEntries = true)
        {
            if (CurrStringsData == null || Delimiters == null)
                return null;
            return CurrStringsData.Split(Delimiters, RemoveEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        /// <summary>
        /// Split CurrStringsData with Delimiters using Options
        /// </summary>
        /// <param name="CurrStringsData"></param>
        /// <param name="Delimiters"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public static string[] P(this string CurrStringsData, string[] Delimiters, bool RemoveEmptyEntries = true)
        {
            if (CurrStringsData == null || Delimiters == null)
                return null;
            return CurrStringsData.Split(Delimiters, RemoveEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        /// <summary>
        /// Split CurrStringsData with Delimiter using Options
        /// </summary>
        /// <param name="CurrStringsData"></param>
        /// <param name="Delimiters"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public static string[] P(this string CurrStringsData, string Delimiter, bool RemoveEmptyEntries = true)
        {
            if (CurrStringsData == null || Delimiter == null)
                return null;
            return CurrStringsData.Split(new string[] { Delimiter }, RemoveEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }


        #endregion

    }
}
