using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.AppDataModels.Common;

namespace ReUse_Std.AppDataModels.Feats
{
    class DataCollections
    {
    }

	/// <summary>
	/// Common ListView Element
	/// </summary>
	[Serializable]
	public class El
	{
		/// <summary>
		/// Current date item with details on added updated guid
		/// </summary>
		public Guid ElId { get; set; }

		/// <summary>
		/// navigation item
		/// </summary>
		public Ni N { get; set; }

		/// <summary>
		/// Common App user item
		/// </summary>
		public Ci C { get; set; }

		/// <summary>
		/// details on added updated
		/// </summary>
		public Di D { get; set; }

		/// <summary>
		/// Additional data
		/// </summary>
		public string A { get; set; }

		/// <summary>
		/// Element Type (category)
		/// </summary>
		public string Et { get; set; }

		/// <summary>
		/// Checked
		/// </summary>
		public bool Ch { get; set; }
	}
}
