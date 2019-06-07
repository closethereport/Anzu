#region copyright
// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)
#endregion
namespace AnzuW.Properties
{


	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.0.0.0")]
	internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
	{

		private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

		public static Settings Default
		{
			get {
				return defaultInstance;
			}
		}

		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("")]
		public string MainBackupFolder
		{
			get {
				return ((string)(this["MainBackupFolder"]));
			}
			set {
				this["MainBackupFolder"] = value;
			}
		}
	}
}
