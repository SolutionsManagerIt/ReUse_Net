using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.AppDataModels.Feats;
using ReUse_Std.AppDataModels.Common;
using ReUse_Std.Base;

namespace ReUse_Std.AppDataModels.Utils
{
    /// <summary>
    /// common App Data Models Utils
    /// </summary>
    public static class ModelsUtils
    {
        #region Common Storage Items
        /// <summary>
        /// Create new common item with Title, ToolTip, Description, MarkDeleted
        /// </summary>
        public static Ci Nc(this string Title, string ToolTip = null, string Description = null, bool? MarkDeleted = null)
        {
            return new Ci() { T = Title, Tt = ToolTip, D = Description, Dl = MarkDeleted };
        }

        /// <summary>
        /// Create new common item with Title, ToolTip, Description, MarkDeleted
        /// </summary>
        public static Di Ndi()
        {
            return new Di() { Ab = _.u, At = _.D };
        }

        /// <summary>
        /// Update CommonItem with udated by at
        /// </summary>
        public static Di U(this Di CommonItem)
        {
            CommonItem.Ub = _.u;
            CommonItem.Ut = _.D;
            return CommonItem;
        }

        public static El Ne(this Ci CommonItem, int ControlNo, string Type, string AdditionalData, bool Checked = false)
        {
            var n = new Ni() { Cn = ControlNo };
            return new El() { D = Ndi(), C = CommonItem, A = AdditionalData, Ch = Checked, Et = Type, N = n };
        }
        #endregion
    }
}
