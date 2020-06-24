using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.Base;

namespace ReUse_Std.AppDataModels.Common
{
    /// <summary>
    /// Common App encoding schema settings storage
    /// </summary>
    [Serializable]
    public class En
    {
        /// <summary>
        /// Current encoded content guid
        /// </summary>
        public Guid EnId { get; set; } = _.g;

    }

    /// <summary>
    /// Symmetric cryptography Secret-key encryption algorithms 
    /// </summary>
    public enum Ets
    {
        /// <summary>
        /// Aes Managed 
        /// </summary>
        A,
        /// <summary>
        /// DES Crypto Service Provider
        /// </summary>
        D,
        /// <summary>
        /// HMACSHA1
        /// </summary>
        H,
        /// <summary>
        /// RC2 Crypto Service Provider
        /// </summary>
        Rc,
        /// <summary>
        /// Rijndael Managed
        /// </summary>
        Rj,
        /// <summary>
        /// Triple DES Crypto Service Provider
        /// </summary>
        Td
    }

    /// <summary>
    /// Asymmetric cryptography Public-key encryption algorithms 
    /// </summary>
    public enum Etp
    {
        /// <summary>
        /// DSA Crypto Service Provider 
        /// </summary>
        D,
        /// <summary>
        /// RSA Crypto Service Provider
        /// </summary>
        R
    }
}
