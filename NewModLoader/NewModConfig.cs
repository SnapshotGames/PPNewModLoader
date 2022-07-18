using PhoenixPoint.Modding;
using System.Collections.Generic;
using System.Linq;

namespace NewModLoader
{
	/// <summary>
	/// Example of data-driven mod config.
	/// </summary>
	public class NewModConfig : ModConfig
	{
		public readonly Dictionary<string, object> Fields = new Dictionary<string, object>();

		public override List<ModConfigField> GetConfigFields() {
			return Fields.Select(f => new ModConfigField(f.Key, f.Value.GetType())
			{
				GetValue = () => f.Value,
				SetValue = (o) => Fields[f.Key] = o,
				GetDescription = () => "<<custom description>>"
			}).ToList();
		}
	}
}
