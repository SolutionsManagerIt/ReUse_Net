using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ReUse_Std.Common
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
        public static T T<T>(this object CurrObject, T DefaultValueOnError = default(T))
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

        /// <summary>
        /// Parse current Data Row To Columns with ColumnsDelimiter (CSV)
        /// </summary>
        public static IEnumerable<string> Pc(this string DataRowContent, string ColumnsDelimiter = ";")
        {
            return DataRowContent.Split(new string[] { ColumnsDelimiter }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// Parse String To Custom type with chaining
        /// </summary>
        public static IEnumerable<string> Pt<T>(this IEnumerable<string> Content, out T Result, f<int, string, T> ParseDataMethod, int ColumnNo = 0, T DefaultValue = default(T))
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
        public static IDictionary<K, string> Pt<K, T>(this IDictionary<K, string> Content, out T Result, f<K, string, T> ParseDataMethod, K ItemKey = default(K), T DefaultValue = default(T))
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
        public static T Pt<T>(this string Content, out T Result, f<string, T> ParseDataMethod, T DefaultValue = default(T))
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
        public static int? P(this string Content, out int? Result, int? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.I(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To float
        /// </summary>
        public static float? P(this string Content, out float? Result, float? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.F(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To double
        /// </summary>
        public static double? P(this string Content, out double? Result, double? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.D(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To bool
        /// </summary>
        public static bool? P(this string Content, out bool? Result, bool? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.B(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime
        /// </summary>
        public static DateTime? P(this string Content, out DateTime? Result, DateTime? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.Dt(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To long
        /// </summary>
        public static long? P(this string Content, out long? Result, long? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.L(s, DefaultValue), DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid
        /// </summary>
        public static Guid? P(this string Content, out Guid? Result, Guid? DefaultValue = null)
        {
            return Content.Pt(out Result, (s) => pcs.G(s, DefaultValue), DefaultValue);
        }

        #endregion

        #region Cast from collections with chainings

        #region IEnumerable values

        /// <summary>
        /// Parse String To Int? with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out int? Result, int ColumnNo = 0, int? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.I(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To Int with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out int Result, int ColumnNo = 0, int DefaultValue = 0)
        {
            return Content.Pt(out Result, (i, s) => pcs.Iv(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out float? Result, int ColumnNo = 0, float? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.F(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out float Result, int ColumnNo = 0, float DefaultValue = 0)
        {
            return Content.Pt(out Result, (i, s) => pcs.Fv(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To double with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out double? Result, int ColumnNo = 0, double? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.D(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out bool? Result, int ColumnNo = 0, bool? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.B(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out DateTime? Result, int ColumnNo = 0, DateTime? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.Dt(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To long with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out long? Result, int ColumnNo = 0, long? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.L(s, DefaultValue), ColumnNo, DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid with chainings from IEnumerable
        /// </summary>
        public static IEnumerable<string> P(this IEnumerable<string> Content, out Guid? Result, int ColumnNo = 0, Guid? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.G(s, DefaultValue), ColumnNo, DefaultValue);
        }

        #endregion

        #region IDictionary values

        /// <summary>
        /// Parse String To Int with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out int? Result, T ItemKey = default(T), int? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.I(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To Int with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out int Result, T ItemKey = default(T), int DefaultValue = 0)
        {
            return Content.Pt(out Result, (i, s) => pcs.Iv(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out float? Result, T ItemKey = default(T), float? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.F(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To float with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out float Result, T ItemKey = default(T), float DefaultValue = 0)
        {
            return Content.Pt(out Result, (i, s) => pcs.Fv(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To double with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out double? Result, T ItemKey = default(T), double? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.D(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out bool? Result, T ItemKey = default(T), bool? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.B(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To bool with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out bool Result, T ItemKey = default(T), bool DefaultValue = true)
        {
            return Content.Pt(out Result, (i, s) => pcs.Bv(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To DateTime with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out DateTime? Result, T ItemKey = default(T), DateTime? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.Dt(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To long with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out long? Result, T ItemKey = default(T), long? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.L(s, DefaultValue), ItemKey, DefaultValue);
        }

        /// <summary>
        /// Parse String To Guid with chainings from IDictionary
        /// </summary>
        public static IDictionary<T, string> P<T>(this IDictionary<T, string> Content, out Guid? Result, T ItemKey = default(T), Guid? DefaultValue = null)
        {
            return Content.Pt(out Result, (i, s) => pcs.G(s, DefaultValue), ItemKey, DefaultValue);
        }

        #endregion

        #endregion

        #endregion
    }


    /// <summary>
    /// Common parsing cast string to common types functions
    /// </summary>
    static class pcs
    {
        #region Private cast string to common types functions

        /// <summary>
        /// Common cast string to nullable int
        /// </summary>
        public static f<string, int?, int?> I = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            int res;
            if (int.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to int
        /// </summary>
        public static f<string, int, int> Iv = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            int res;
            if (int.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to nullable float
        /// </summary>
        public static f<string, float?, float?> F = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            float res;
            if (float.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to float
        /// </summary>
        public static f<string, float, float> Fv = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            float res;
            if (float.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to double
        /// </summary>
        public static f<string, double?, double?> D = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            double res;
            if (double.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to nullable bool
        /// </summary>
        public static f<string, bool?, bool?> B = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            bool res;
            if (bool.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to bool
        /// </summary>
        public static f<string, bool, bool> Bv = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            bool res;
            if (bool.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to DateTime
        /// </summary>
        public static f<string, DateTime?, DateTime?> Dt = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            DateTime res;
            if (DateTime.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to long
        /// </summary>
        public static f<string, long?, long?> L = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
            long res;
            if (long.TryParse(s, out res))
                return res;
            else return d;
        };

        /// <summary>
        /// Common cast string to Guid
        /// </summary>
        public static f<string, Guid?, Guid?> G = (s, d) =>
        {
            if (s == null || s.Length == 0) return d;
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
        /// Format current object Value to string with CustomFormat
        /// </summary>
        public static string Fo<T>(this T Value, string CustomFormat = null)
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
        public static int? tI(this string Value, int? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
                        
            int r;
            if(int.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to short with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static short? tS(this string Value, short? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            short r;
            if (short.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to long with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static long? tL(this string Value, long? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            long r;
            if (long.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to signed sbyte with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static sbyte? tBt(this string Value, sbyte? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            sbyte r;
            if (sbyte.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        #endregion

        #region unsigned
        /// <summary>
        /// Cast current string Value to unsigned uint with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static uint? ti(this string Value, uint? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            uint r;
            if (uint.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to unsigned ushort with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static ushort? tu(this string Value, ushort? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            ushort r;
            if (ushort.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to unsigned ulong with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static ulong? tl(this string Value, ulong? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            ulong r;
            if (ulong.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to unsigned byte with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static byte? tbt(this string Value, byte? DefaultValue = null, bool UseHex = false, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            byte r;
            if (byte.TryParse(Value, CustomNumberStyle ?? (UseHex ? NumberStyles.HexNumber : NumberStyles.Any), null, out r))
                return r;
            return DefaultValue;
        }
        #endregion

        #endregion

        #region floating
        /// <summary>
        /// Cast current string Value to float with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static float? tF(this string Value, float? DefaultValue = null, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            float r;
            if (float.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to decimal with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static decimal? tDc(this string Value, decimal? DefaultValue = null, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            decimal r;
            if (decimal.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to double with DefaultValue and optional CustomNumberStyle
        /// </summary>
        public static double? tDb(this string Value, double? DefaultValue = null, NumberStyles? CustomNumberStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            double r;
            if (double.TryParse(Value, CustomNumberStyle ?? NumberStyles.Any, null, out r))
                return r;
            return DefaultValue;
        }

        #endregion

        #region other

        /// <summary>
        /// Cast current string Value to char with DefaultValue, UseHex and optional CustomNumberStyle
        /// </summary>
        public static char? tC(this string Value, char? DefaultValue = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            char r;
            if (char.TryParse(Value, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to bool with DefaultValue
        /// </summary>
        public static bool? tB(this string Value, bool? DefaultValue = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            bool r;
            if (bool.TryParse(Value, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to Guid with DefaultValue
        /// </summary>
        public static Guid? tG(this string Value, Guid? DefaultValue = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            Guid r;
            if (Guid.TryParse(Value, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to Enum with DefaultValue
        /// </summary>
        public static T tE<T>(this string Value, T DefaultValue = default(T)) where T : struct, IConvertible
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
        public static DateTime? tD(this string Value, DateTime? DefaultValue = null, DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            DateTime r;
            if (DateTime.TryParse(Value, null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to DateTime with CustomFormat, DefaultValue and optional CustomDateTimeStyle
        /// </summary>
        public static DateTime? tD(this string Value, string CustomFormat, DateTime? DefaultValue = null, DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            DateTime r;
            if (DateTime.TryParseExact(Value, CustomFormat, null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to DateTime with CustomFormats, DefaultValue and optional CustomDateTimeStyle
        /// </summary>
        public static DateTime? tD(this string Value, IEnumerable<string> CustomFormats, DateTime? DefaultValue = null, DateTimeStyles? CustomDateTimeStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            DateTime r;
            if (DateTime.TryParseExact(Value, CustomFormats.A(), null, CustomDateTimeStyle ?? DateTimeStyles.AllowWhiteSpaces, out r))
                return r;
            return DefaultValue;
        }


        /// <summary>
        /// Cast current string Value to TimeSpan with DefaultValue
        /// </summary>
        public static TimeSpan? tT(this string Value, TimeSpan? DefaultValue = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            TimeSpan r;
            if (TimeSpan.TryParse(Value, null, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to TimeSpan with CustomFormat, DefaultValue and optional CustomTimeSpanStyle
        /// </summary>
        public static TimeSpan? tT(this string Value, string CustomFormat, TimeSpan? DefaultValue = null, TimeSpanStyles? CustomTimeSpanStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            TimeSpan r;
            if (TimeSpan.TryParseExact(Value, CustomFormat, null, CustomTimeSpanStyle ?? TimeSpanStyles.None, out r))
                return r;
            return DefaultValue;
        }

        /// <summary>
        /// Cast current string Value to TimeSpan with CustomFormats, DefaultValue and optional CustomTimeSpanStyle
        /// </summary>
        public static TimeSpan? tT(this string Value, IEnumerable<string> CustomFormats, TimeSpan? DefaultValue = null, TimeSpanStyles? CustomTimeSpanStyle = null)
        {
            if (Value == null || Value.Length == 0)
                return DefaultValue;
            TimeSpan r;
            if (TimeSpan.TryParseExact(Value, CustomFormats.A(), null, CustomTimeSpanStyle ?? TimeSpanStyles.None, out r))
                return r;
            return DefaultValue;
        }

        #endregion

        #endregion
    }
}
