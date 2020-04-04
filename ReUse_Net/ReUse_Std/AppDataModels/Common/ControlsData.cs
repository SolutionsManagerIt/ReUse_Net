using System;
using System.Collections.Generic;
using System.Text;

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
        public Guid VdId { get; set; }
        /// <summary>
        /// Int values
        /// </summary>
        public Vdn I { get; set; }
        /// <summary>
        /// float values
        /// </summary>
        public Vdf L { get; set; }
        /// <summary>
        /// additional types values
        /// </summary>
        public Vda A { get; set; }
        /// <summary>
        /// string value
        /// </summary>
        public string S { get; set; }
        /// <summary>
        /// bool value
        /// </summary>
        public bool? B { get; set; }
    }

    /// <summary>
    /// Common App values data int number type storage
    /// </summary>
    [Serializable]
    public class Vdn
    {
        /// <summary>
        /// Current values int data number type guid
        /// </summary>
        public Guid VdnId { get; set; }
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
        public Guid VdfId { get; set; }
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
    }

    /// <summary>
    /// Common App values data additional types (datetime, guid) storage
    /// </summary>
    [Serializable]
    public class Vda
    {
        /// <summary>
        /// Current values int data number type guid
        /// </summary>
        public Guid VddId { get; set; }
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
}
