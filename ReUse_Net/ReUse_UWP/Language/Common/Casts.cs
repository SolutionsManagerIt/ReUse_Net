using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ReUse_UWP.Common
{
    /// <summary>
    /// Common Casts Utilities
    /// </summary>
    public static class Casts
    {
        #region Parse

        /// <summary>
        /// Try cast CurrObject to specified Type (of return DefaultValueOnError)
        /// </summary>
        public static T _T<T>(this object CurrObject, T DefaultValueOnError = default(T))
        {
            if (CurrObject != null)
            {
                if (CurrObject is T)
                    return (T)CurrObject;
                if (typeof(T) == typeof(float) && CurrObject is double)
                    return (T)((object)(float)(double)CurrObject);
                return (T)Convert.ChangeType(CurrObject, typeof(T));
            }
            return DefaultValueOnError;
        }

        ///// <summary>
        ///// Get data from string file with ColumnsDelimiter (CSV)
        ///// </summary>
        //public static IEnumerable<T> _Gdf<T>(this string FilePath, f<string, int, string, T> GetDataFromRowMethod, string ColumnsDelimiter = ";")
        //{
        //    var FileData = FilePath._FGs();

        //    if (FileData == null || FileData.Count() < 1)
        //        return null;

        //    var f = FileData.ToArray();

        //    var Result = new List<T>();

        //    //string del = ColumnsDelimiter ?? ";";
        //    //if (GetDataFromRowMethod(f[0], -1, ColumnsDelimiter) == null && (f.Length == 1 || GetDataFromRowMethod(f[1], -1, ColumnsDelimiter) == null))
        //    //    ColumnsDelimiter = ",";

        //    int RowCounter = 0;
        //    foreach (string item in FileData)
        //        Result.Add(GetDataFromRowMethod(item, RowCounter++, ColumnsDelimiter ?? ";"));

        //    return Result;
        //}

        /// <summary>
        /// Parse current Data Row To Columns with ColumnsDelimiter (CSV)
        /// </summary>
        public static IEnumerable<string> _Pc(this string DataRowContent, string ColumnsDelimiter = ";")
        {
            return DataRowContent.Split(new string[] { ColumnsDelimiter }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// Parse String To Custom type with chaining
        /// </summary>
        public static IEnumerable<string> _Pt<T>(this IEnumerable<string> Content, out T Result, f<int, string, T> ParseDataMethod, int ColumnNo = 0, T DefaultValue = default(T))
        {
            Result = DefaultValue;
            if (Content == null || Content.Count() < ColumnNo)
                return Content;
            var c = Content.ElementAt(ColumnNo);
            if (string.IsNullOrWhiteSpace(c))
                return Content;
            Result = ParseDataMethod(ColumnNo, c);
            return Content;
        }

        /// <summary>
        /// Parse String To Custom type with chaining from IDictionary
        /// </summary>
        public static IDictionary<K, string> _Pt<K, T>(this IDictionary<K, string> Content, out T Result, f<K, string, T> ParseDataMethod, K ItemKey = default(K), T DefaultValue = default(T))
        {
            Result = DefaultValue;
            if (Content == null || Content.Count < 0 || !Content.ContainsKey(ItemKey))
                return Content;
            var c = Content[ItemKey];
            if (string.IsNullOrWhiteSpace(c))
                return Content;
            Result = ParseDataMethod(ItemKey, c);
            return Content;
        }

        /// <summary>
        /// Parse String To Custom type
        /// </summary>
        public static T _Pt<T>(this string Content, out T Result, f<string, T> ParseDataMethod, T DefaultValue = default(T))
        {
            Result = DefaultValue;
            if (string.IsNullOrWhiteSpace(Content))
                return DefaultValue;
            Result = ParseDataMethod(Content);
            return Result;
        }


        #endregion

        #region Cast string to types

        #region Common Casts

        /// <summary>
        /// Parse String To Int
        /// </summary>
        public static int? _P(this string Content, out int? Result, int? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.I(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To float
        /// </summary>
        public static float? _P(this string Content, out float? Result, float? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.F(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To double
        /// </summary>
        public static double? _P(this string Content, out double? Result, double? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.D(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To bool
        /// </summary>
        public static bool? _P(this string Content, out bool? Result, bool? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.B(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime
        /// </summary>
        public static DateTime? _P(this string Content, out DateTime? Result, DateTime? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.Dt(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To long
        /// </summary>
        public static long? _P(this string Content, out long? Result, long? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.L(-1, s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid
        /// </summary>
        public static Guid? _P(this string Content, out Guid? Result, Guid? DefaultValue = null)
        {
            return Content._Pt(out Result, (s) => _c<int>.G(-1, s, DefaultValue), DefaultValue);
        }

        #endregion

        #region Cast from collections with chainings

        #region IEnumerable

        /// <summary>
        /// Parse String To Int? with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out int? Result, int ColumnNo = 0, int? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.I(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To Int with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out int Result, int ColumnNo = 0, int DefaultValue = 0)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.Iv(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out float? Result, int ColumnNo = 0, float? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.F(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out float Result, int ColumnNo = 0, float DefaultValue = 0)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.Fv(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To double with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out double? Result, int ColumnNo = 0, double? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.D(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out bool? Result, int ColumnNo = 0, bool? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.B(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out DateTime? Result, int ColumnNo = 0, DateTime? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.Dt(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To long with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out long? Result, int ColumnNo = 0, long? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.L(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> _P(this IEnumerable<string> Content, out Guid? Result, int ColumnNo = 0, Guid? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<int>.G(i, s, DefaultValue), ColumnNo, DefaultValue);
        }

        #endregion

        #region IDictionary

        /// <summary>
        /// Parse String To Int with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out int? Result, T ItemKey = default(T), int? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.I(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To Int with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out int Result, T ItemKey = default(T), int DefaultValue = 0)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.Iv(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out float? Result, T ItemKey = default(T), float? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.F(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out float Result, T ItemKey = default(T), float DefaultValue = 0)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.Fv(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To double with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out double? Result, T ItemKey = default(T), double? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.D(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out bool? Result, T ItemKey = default(T), bool? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.B(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out bool Result, T ItemKey = default(T), bool DefaultValue = true)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.Bv(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out DateTime? Result, T ItemKey = default(T), DateTime? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.Dt(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To long with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out long? Result, T ItemKey = default(T), long? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.L(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> _P<T>(this IDictionary<T, string> Content, out Guid? Result, T ItemKey = default(T), Guid? DefaultValue = null)
        {
            return Content._Pt(out Result, (i, s) => _c<T>.G(i, s, DefaultValue), ItemKey, DefaultValue);
        }

        #endregion

        #endregion

        #endregion
    }


    /// <summary>
    /// Common cast string to common types functions
    /// </summary>
    public static class _c<t>
    {
        #region Private cast string to common types functions



        /// <summary>
        /// Common cast string to nullable int
        /// </summary>
        public static f<t, string, int?, int?> I = (i, s, d) =>
        {
            int res;
            if (int.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to int
        /// </summary>
        public static f<t, string, int, int> Iv = (i, s, d) =>
        {
            int res;
            if (int.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to nullable float
        /// </summary>
        public static f<t, string, float?, float?> F = (i, s, d) =>
        {
            float res;
            if (float.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to float
        /// </summary>
        public static f<t, string, float, float> Fv = (i, s, d) =>
        {
            float res;
            if (float.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to double
        /// </summary>
        public static f<t, string, double?, double?> D = (i, s, d) =>
        {
            double res;
            if (double.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to nullable bool
        /// </summary>
        public static f<t, string, bool?, bool?> B = (i, s, d) =>
        {
            bool res;
            if (bool.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to bool
        /// </summary>
        public static f<t, string, bool, bool> Bv = (i, s, d) =>
        {
            bool res;
            if (bool.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to DateTime
        /// </summary>
        public static f<t, string, DateTime?, DateTime?> Dt = (i, s, d) =>
        {
            DateTime res;
            if (DateTime.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to long
        /// </summary>
        public static f<t, string, long?, long?> L = (i, s, d) =>
        {
            long res;
            if (long.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to long
        /// </summary>
        public static f<t, string, Guid?, Guid?> G = (i, s, d) =>
        {
            Guid res;
            if (Guid.TryParse(s, out res))
                return res;
            else return d;
        };

        #endregion

    }

    /// <summary>
    /// Base Type Casts Utilities for code generation
    /// </summary>
    public static class BaseTypeCastsUtilities
    {
        #region To string

        /// <summary>
        /// Format current Value to string with CustomFormat
        /// </summary>
        public static string F<T>(this T Value, string CustomFormat = null)
        {
            if (Value == null)
                return null;
            if (CustomFormat == null)
                return CustomFormat.ToString();
            return string.Format(CustomFormat, Value);
        }

        #endregion

        #region From string

        #region integer

        #region signed
        /// <summary>
        /// Cast current string Value to int with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static int Ti(this string Value, int DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            int r = DefaultValue;
            int.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to short with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static short Ts(this string Value, short DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            short r = DefaultValue;
            short.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to long with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static long Tl(this string Value, long DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            long r = DefaultValue;
            long.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to signed sbyte with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static sbyte Tbt(this string Value, sbyte DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            sbyte r = DefaultValue;
            sbyte.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        #endregion

        #region unsigned
        /// <summary>
        /// Cast current string Value to unsigned uint with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static uint Tiu(this string Value, uint DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            uint r = DefaultValue;
            uint.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to unsigned ushort with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static ushort Tsu(this string Value, ushort DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            ushort r = DefaultValue;
            ushort.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to unsigned ulong with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static ulong Tlu(this string Value, ulong DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            ulong r = DefaultValue;
            ulong.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to unsigned byte with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static byte Tbtu(this string Value, byte DefaultValue = 0, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            byte r = DefaultValue;
            byte.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r);
            return r;
        }
        #endregion

        #endregion

        #region floating
        /// <summary>
        /// Cast current string Value to float with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static float Tf(this string Value, float DefaultValue = 0, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            float r = DefaultValue;
            float.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to decimal with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static decimal Tdc(this string Value, decimal DefaultValue = 0, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            decimal r = DefaultValue;
            decimal.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to double with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static double Tdb(this string Value, double DefaultValue = 0, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            double r = DefaultValue;
            double.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r);
            return r;
        }

        #endregion

        #region other

        /// <summary>
        /// Cast current string Value to char with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static char Tc(this string Value, char DefaultValue = '0')
        {
            if (Value == null || Value.Length != 1)
                return DefaultValue;
            char r = DefaultValue;
            char.TryParse(Value, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to bool with DefaultValue
        /// </summary>
        public static bool Tb(this string Value, bool DefaultValue = false)
        {
            if (Value == null || Value.Length != 1)
                return DefaultValue;
            bool r = DefaultValue;
            bool.TryParse(Value, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to Guid with DefaultValue
        /// </summary>
        public static Guid Tg(this string Value, Guid DefaultValue = new Guid())
        {
            if (Value == null || Value.Length != 1)
                return DefaultValue;
            Guid r = DefaultValue;
            Guid.TryParse(Value, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to Enum with DefaultValue
        /// </summary>
        public static T Te<T>(this string Value, T DefaultValue = default(T)) where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(Value)) return DefaultValue;
            var q = typeof(T);
            if (!q.IsEnum) return DefaultValue; //throw new ArgumentException("T must be an enumerated type");

            var s = Enum.GetValues(q);
            foreach (T i in s)
                if (i.ToString().ToLower().Equals(Value.Trim().ToLower()))
                    return i;

            return DefaultValue;
        }

        #endregion

        #region datetime and timespan

        /// <summary>
        /// Cast current string Value to DateTime with DefaultValue and optional CustomDateTimeStyle
        /// </summary>
        public static DateTime Td(this string Value, DateTime DefaultValue = new DateTime(), DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            DateTime r = DefaultValue;
            DateTime.TryParse(Value, null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to DateTime with CustomFormat, DefaultValue and optional CustomDateTimeStyle
        /// </summary>
        public static DateTime Td(this string Value, string CustomFormat, DateTime DefaultValue = new DateTime(), DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            DateTime r = DefaultValue;
            DateTime.TryParseExact(Value, CustomFormat, null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to DateTime with CustomFormats, DefaultValue and optional CustomDateTimeStyle
        /// </summary>
        public static DateTime Td(this string Value, IEnumerable<string> CustomFormats, DateTime DefaultValue = new DateTime(), DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            DateTime r = DefaultValue;
            DateTime.TryParseExact(Value, CustomFormats._A(), null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r);
            return r;
        }


        /// <summary>
        /// Cast current string Value to TimeSpan with DefaultValue
        /// </summary>
        public static TimeSpan Tt(this string Value, TimeSpan DefaultValue = new TimeSpan())
        {
            if (Value == null)
                return DefaultValue;
            TimeSpan r = DefaultValue;
            TimeSpan.TryParse(Value, null, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to TimeSpan with CustomFormat, DefaultValue and optional CustomTimeSpanStyle
        /// </summary>
        public static TimeSpan Tt(this string Value, string CustomFormat, TimeSpan DefaultValue = new TimeSpan(), TimeSpanStyles? CustomTimeSpanStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            TimeSpan r = DefaultValue;
            TimeSpan.TryParseExact(Value, CustomFormat, null, CustomTimeSpanStyle ?? TimeSpanStyles.None, out r);
            return r;
        }

        /// <summary>
        /// Cast current string Value to TimeSpan with CustomFormats, DefaultValue and optional CustomTimeSpanStyle
        /// </summary>
        public static TimeSpan Tt(this string Value, IEnumerable<string> CustomFormats, TimeSpan DefaultValue = new TimeSpan(), TimeSpanStyles? CustomTimeSpanStyle = null)
        {
            if (Value == null)
                return DefaultValue;
            TimeSpan r = DefaultValue;
            TimeSpan.TryParseExact(Value, CustomFormats._A(), null, CustomTimeSpanStyle ?? TimeSpanStyles.None, out r);
            return r;
        }

        #endregion

        #endregion
    }
}
