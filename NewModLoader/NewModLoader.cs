using PhoenixPoint.Modding;
using System.Collections.Generic;
using System.Reflection;

namespace NewModLoader
{
	/// <summary>
	/// Mod loaders are used by ModManager to allow discovery and loading of non traditional mods.
	/// To be used from ModManager, mod loader have to derive from PhoenixPoint.Modding.ModLoader.
	/// Mod loader assembly can have multiple mod loaders, if nessesary.
	/// </summary>
	public class NewModLoader : ModLoader
	{
		public override string UniqueName => "<change to friendly mod loader name>";

		/// <summary>
		/// Called by ModManager to get list of mods that this loader can use.
		/// Mod loader must return list with discovered mods or throw an exception
		/// </summary>
		/// <param name="context">General context used for accessing various modding resources.</param>
		/// <param name="loaderDir">Directory in which loaders assembly is.</param>
		/// <returns>List with mods usabe by this loader or null.</returns>
		public override List<ModEntry> DiscoverMods(ModSDKContext context, string loaderDir) {
			List<ModEntry> result = new List<ModEntry>();

			/// Metadata is general info for the mod, in standard mods this is meta.json file.
			/// In multilangual fields, if game language is missing, either English or first entry will be used.
			ModMeta metadata = new ModMeta()
			{
				/// Unique mod ID
				ID = "<mod.id>",
				/// Multilangual name of the mod.
				Name = new ModMeta.LocalizationEntry[] { new ModMeta.LocalizationEntry("English", "<mod name>") },
				/// Multilangual mod author name. (optional)
				Author = new ModMeta.LocalizationEntry[] { new ModMeta.LocalizationEntry("English", "<mod author>") },
				/// Multilangual mod description. (optional)
				Description = new ModMeta.LocalizationEntry[] { new ModMeta.LocalizationEntry("English", "<mod description>") },
				/// Version of the mod.
				Version = new System.Version("1.0"),
				/// Mod dependencies, each specifies a mod ID. (optional)
				Dependencies = new string[0],
			};

			ModEntry entry = new ModEntry("<absolute dir where mod is>", metadata);

			/// Tag can be used pass loader-related data for the mod with ModEntry
			entry.Tag = "<some custom object for easy ref>";

			result.Add(entry);

			/// Can log information, warnings and errors.
			/// Log can be checked from the in-game console and Unity log file.
			Logger.LogInfo("<log something>");

			return result;
		}

		/// <summary>
		/// Called my ModManager when given mod entry is going to be loaded.
		/// Mod loader must return ModInstance if mod is loaded successfully, or throw an exception.
		/// </summary>
		/// <param name="context">General context used for accessing various modding resources.</param>
		/// <param name="modEntry">Mod to be loaded.</param>
		/// <returns>ModInstance, representing current mod</returns>
		public override ModInstance LoadMod(ModSDKContext context, ModEntry modEntry) {

			/// Can log information, warnings and errors.
			/// Log can be checked from the in-game console and Unity log file.
			Logger.LogInfo("<log something>");

			/// Mod loaders can load modding assemblies on demand.
			/// This will load both .net assembly and debug symbols (if any).
			/// Result is either the loaded assembly, null or exception on failure.
			Assembly assembly = context.LoadModdingAssembly("<absolute path to code assembly if any>");

			NewModConfig config = new NewModConfig();
			config.Fields.Add("<field name>", 10);

			return new ModInstance(modEntry)
			{
				/// ModMain. This is the entry point to mod. Without it, mod will not have code to execute.
				Main = new NewModMain(),
				/// Custom config (optional). Used by the mod for user-friendly configuration.
				Config = config,
				/// GeoscapeMod (optional). Callback to create mod-specific instance that will modify Geoscape.
				CreateGeoscapeMod = () => null,
				/// TacticalMod (optional).  Callback to create mod-specific instance that will modify Tactical.
				CreateTacticalMod = () => null,
				/// Can mod be safely unloaded, or game will need to be restarted.
				CanBeUnloaded = true
			};
		}

		/// <summary>
		/// Called my ModManager when given mod entry is going to be unloaded.
		/// Mod loader must return if mod was unloaded successfully or throw an exception.
		/// </summary>
		/// <param name="context">General context used for accessing various modding resources.</param>
		/// <param name="modEntry">Mod to be unloaded</param>
		/// <returns>True if mod was successfully unloaded.</returns>
		public override bool UnloadMod(ModSDKContext context, ModEntry modEntry) {
			/// Can log information, warnings and errors.
			/// Log can be checked from the in-game console and Unity log file.
			Logger.LogInfo("<log something>");
			return true;
		}
	}
}
