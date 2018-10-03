namespace UnityEngine.ProBuilder
{
	/// <summary>
	/// Information about this build of ProBuilder.
	/// </summary>
	static class Version
	{
		internal static readonly SemVer currentInfo = new SemVer("4.0.0-preview.20", "2018/10/02");

		/// <summary>
		/// Get the current version.
		/// </summary>
		/// <returns>The current version string in semantic version format.</returns>
		public static string current
		{
            get { return currentInfo.ToString(); }
		}
	}
}
