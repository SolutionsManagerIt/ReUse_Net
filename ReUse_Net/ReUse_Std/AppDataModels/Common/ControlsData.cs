using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.Base;

namespace ReUse_Std.AppDataModels.Common
{
    class ControlsData
    {
    }

    /// <summary>
    /// Common App values data storage
    /// </summary>
    [Serializable]
    public class Vd
    {
        /// <summary>
        /// Current values data guid
        /// </summary>
        public Guid VdId { get; set; } = _.g;
        /// <summary>
        /// field value type
        /// </summary>
        public Fv Tp { get; set; }
        /// <summary>
        /// Int values
        /// </summary>
        public Vdn I { get; set; }
        /// <summary>
        /// float double values
        /// </summary>
        public Vdf D { get; set; }
        /// <summary>
        /// additional types values
        /// </summary>
        public Vda A { get; set; }
        /// <summary>
        /// select from list field type value
        /// </summary>
        public Vds L { get; set; }

        /// <summary>
        /// Texts custom value
        /// </summary>
        public Vdt T { get; set; }
        /// <summary>
        /// simple string value
        /// </summary>
        public string S { get; set; }
        /// <summary>
        /// bool value
        /// </summary>
        public bool? B { get; set; }
        /// <summary>
        /// Custom display format
        /// </summary>
        public string F { get; set; }

    }

    #region custom types

    /// <summary>
    /// Common App values data int number type storage
    /// </summary>
    [Serializable]
    public class Vdn
    {
        /// <summary>
        /// Current values int data number type guid
        /// </summary>
        public Guid VdnId { get; set; } = _.g;
        /// <summary>
        /// Int value
        /// </summary>
        public int? I { get; set; }
        /// <summary>
        /// long int value
        /// </summary>
        public long? L { get; set; }
        /// <summary>
        /// short int value
        /// </summary>
        public short? S { get; set; }
        /// <summary>
        /// byte int value
        /// </summary>
        public byte? B { get; set; }

        /// <summary>
        /// long max value
        /// </summary>
        public long? Mx { get; set; }
        /// <summary>
        /// long min value
        /// </summary>
        public long? Mn { get; set; }
    }

    /// <summary>
    /// Common App values data float number type storage
    /// </summary>
    [Serializable]
    public class Vdf
    {
        /// <summary>
        /// Current values data float number type guid
        /// </summary>
        public Guid VdfId { get; set; } = _.g;
        /// <summary>
        /// decimal value
        /// </summary>
        public decimal? I { get; set; }
        /// <summary>
        /// float value
        /// </summary>
        public float? F { get; set; }
        /// <summary>
        /// double value
        /// </summary>
        public double? D { get; set; }
        /// <summary>
        /// double max value
        /// </summary>
        public double? Mx { get; set; }
        /// <summary>
        /// double min value
        /// </summary>
        public double? Mn { get; set; }
    }

    /// <summary>
    /// Common App values data additional types (datetime, guid) storage
    /// </summary>
    [Serializable]
    public class Vda
    {
        /// <summary>
        /// Current values data type guid
        /// </summary>
        public Guid VddId { get; set; } = _.g;
        /// <summary>
        /// DateTime value
        /// </summary>
        public DateTime? D { get; set; }
        /// <summary>
        /// TimeSpan value
        /// </summary>
        public TimeSpan? T { get; set; }

        /// <summary>
        /// TimeSpan ticks value
        /// </summary>
        public long? Tl { get; set; }

        /// <summary>
        /// Guid value
        /// </summary>
        public Guid? G { get; set; }
    }

    /// <summary>
    /// Common App values select from list field choice data type storage
    /// </summary>
    [Serializable]
    public class Vds
    {
        /// <summary>
        /// Current values data type guid
        /// </summary>
        public Guid VdsId { get; set; } = _.g;
        /// <summary>
        /// select from list field type value
        /// </summary>
        public Sl St { get; set; }

        /// <summary>
        /// select from external data (table, collections) field type
        /// </summary>
        public Cs E { get; set; }

        /// <summary>
        /// select from list field choice values
        /// </summary>
        public List<Cs> V { get; set; }

        /// <summary>
        /// allow custom value for select from list value
        /// </summary>
        public bool? sC { get; set; }
    }

    /// <summary>
    /// Common App values Text editing field type data storage
    /// </summary>
    [Serializable]
    public class Vdt
    {
        /// <summary>
        /// Current values data type guid
        /// </summary>
        public Guid VdtId { get; set; } = _.g;
        /// <summary>
        /// Text editing field type
        /// </summary>
        public St T { get; set; }

        /// <summary>
        /// Rows no (height)
        /// </summary>
        public int? R { get; set; }
        /// <summary>
        /// Columns no (width)
        /// </summary>
        public int? C { get; set; }

        /// <summary>
        /// Max text length
        /// </summary>
        public int? L { get; set; }

        /// <summary>
        /// allow custom value for select from list value
        /// </summary>
        public bool? sC { get; set; }
    }
    #endregion

    /// <summary>
    /// Common selectors Items (select data by selector)
    /// </summary>
    [Serializable]
    public class Cs
    {
        /// <summary>
        /// Item Key Guid
        /// </summary>
        public Guid CsId { get; set; } = _.g;
        /// <summary>
        /// Item int ID selector
        /// </summary>
        public int I { get; set; }
        /// <summary>
        /// Item Guid selector
        /// </summary>
        public Guid? G { get; set; }
        /// <summary>
        /// Item Key selector
        /// </summary>
        public string K { get; set; }
        /// <summary>
        /// Item Title selector
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// Item Value selector
        /// </summary>
        public string V { get; set; }
        /// <summary>
        /// Item Description selector
        /// </summary>
        public string D { get; set; }
    }

    #region enums

    /// <summary>
    /// select from List string field type
    /// </summary>
    public enum Sl
    {
        /// <summary>
        /// select dropdown list
        /// </summary>
        S,
        /// <summary>
        /// radio buttons
        /// </summary>
        R,
        /// <summary>
        /// combobox
        /// </summary>
        C,
        /// <summary>
        /// flags (multiple values)
        /// </summary>
        F
    }

    /// <summary>
    /// Text editing field type
    /// </summary>
    public enum St
    {
        /// <summary>
        /// plain text only
        /// </summary>
        T,
        /// <summary>
        /// Rich text format (RTF)
        /// </summary>
        R,
        /// <summary>
        /// Html text format (simple)
        /// </summary>
        Hs,
        /// <summary>
        /// Html text format (full)
        /// </summary>
        Hf,
        /// <summary>
        /// XML text format (full)
        /// </summary>
        X
    }

    /// <summary>
    /// Common form field value type
    /// </summary>
    public enum Fv
    {
        /// <summary>
        /// Int value type
        /// </summary>
        I = 0,
        /// <summary>
        /// Float value type
        /// </summary>
        F = 1,
        /// <summary>
        /// Bool value type
        /// </summary>
        B = 2,
        /// <summary>
        /// Double value type
        /// </summary>
        Db = 3,
        /// <summary>
        /// Datetime value type
        /// </summary>
        D = 4,
        /// <summary>
        /// Date only value type
        /// </summary>
        Do = 5,
        /// <summary>
        /// Guid value type
        /// </summary>
        G = 6,
        /// <summary>
        /// string (short, single line) value type
        /// </summary>
        S = 7,
        /// <summary>
        /// string (medium) value type
        /// </summary>
        Sm = 8,
        /// <summary>
        /// string (large) value type
        /// </summary>
        Sl = 9,
        /// <summary>
        /// Time span value type
        /// </summary>
        T = 10,
        /// <summary>
        /// Time span value type
        /// </summary>
        U = 11,
        /// <summary>
        /// Time span value type
        /// </summary>
        Um = 11,
        /// <summary>
        /// Time span value type
        /// </summary>
        L = 11
    }
    
    #endregion
}
