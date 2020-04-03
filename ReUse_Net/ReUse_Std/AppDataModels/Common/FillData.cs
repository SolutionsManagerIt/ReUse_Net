using System;
using System.Collections.Generic;
using System.Text;

namespace ReUse_Std.AppDataModels.Common
{
    /// <summary>
    /// Common Tree View Items Data
    /// </summary>
    [Serializable]
    public class Tvi
    {
        public int ID { get; set; }
        public int ControlID { get; set; }
        public Guid? ControlGuid { get; set; }
        public Guid ItemGuid { get; set; }
        public string Title { get; set; }
        public string ToolTipText { get; set; }
        public string Description { get; set; }
        public bool? UseGlobal { get; set; }
        public bool? IsSelected { get; set; }
        public int? ItemPictureNo { get; set; }
        public int? SelectedPictureNo { get; set; }
        public string PicTypeDef { get; set; }
        public string SelPicTypeDef { get; set; }
        public double? OrderBy { get; set; }
        public Guid? ParentGuid { get; set; }
        public Guid? ConnectionGuid { get; set; }
        public Guid? ArrayGuid { get; set; }
        public Guid? SessionGuid { get; set; }
        public string BackColor { get; set; }
        public string ForeColor { get; set; }
        public string CustomFont { get; set; }
        public bool? IsExpanded { get; set; }
        public bool? IsExpandedAll { get; set; }
        public DateTime? UpdAt { get; set; }
        public DateTime? AddAt { get; set; }
        public string UpdBy { get; set; }
        public string AddBy { get; set; }
        public string Path { get; set; }
        public string SerializedData { get; set; }
        public Guid AppGuid { get; set; }
        public bool? Deleted { get; set; }
        public Guid? PicGuid { get; set; }
        public Guid? SelPicGuid { get; set; }
        public Guid? SerializedDataGuid { get; set; }
        public byte[] SerializedObjects { get; set; }
        public Guid? SerializedObjectGuid { get; set; }
        public Guid? LanguageGuid { get; set; }
        public bool? VisibleUser { get; set; }
        public bool? VisiblePC { get; set; }
        public bool? VisibleApp { get; set; }
        public int? DataInt { get; set; }
        public string DataString { get; set; }
        public Guid? DataGuid { get; set; }
    }

    
}
