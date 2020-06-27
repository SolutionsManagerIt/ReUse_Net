using ReUse_Std.AppDataModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.Base;

namespace ReUse_Std.AppDataModels.Feats
{
    class TextStorages
    {
    }

	/// <summary>
	/// Common Text Storages Element
	/// </summary>
	[Serializable]
	public class Ts
	{
		/// <summary>
		/// Current date item with details on added updated guid
		/// </summary>
		public Guid TsId { get; set; } = _.g;

		/// <summary>
		/// Words data
		/// </summary>
		public Tr W { get; set; }

		/// <summary>
		/// Sentences data
		/// </summary>
		public Tr S { get; set; }

		/// <summary>
		/// Checked
		/// </summary>
		public bool? U { get; set; }
		///// <summary>
		///// Checked
		///// </summary>
		//public bool Us { get; set; }
	}

	/// <summary>
	/// Common Text Records Element
	/// </summary>
	[Serializable]
	public class Tr
	{
		/// <summary>
		/// Current date item with details on added updated guid
		/// </summary>
		public Guid TrId { get; set; } = _.g;

		/// <summary>
		/// Text data
		/// </summary>
		public string D { get; set; }

		/// <summary>
		/// Records Delimiter
		/// </summary>
		public string Dr { get; set; }

		/// <summary>
		/// SubRecords Delimiter
		/// </summary>
		public string Ds { get; set; }

		/// <summary>
		/// Used
		/// </summary>
		public bool? U { get; set; }
		/// <summary>
		/// New line as delimiter
		/// </summary>
		public bool? N { get; set; }

		/// <summary>
		/// Values Lists
		/// </summary>
		public List<Ci> L { get; set; }
	}
}
