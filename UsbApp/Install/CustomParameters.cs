using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Install;
using System.Collections;

namespace kppApp.Install
{
	public class CustomParameters
	{
		/// <summary>
		/// This inner class maintains the key names for the parameter values that may be passed on the 
		/// command line.
		/// </summary>
		public class Keys
		{
			public const string url = "url";
			public const string reader = "reader";
			public const string direction = "direction";
		}

		private string _url = null;
		public string url
		{
			get { return _url; }
		}

		private string _reader = null;
		public string reader
		{
			get { return _reader; }
		}

		private string _direction = null;
		public string direction
		{
			get { return _direction; }
		}

		/// <summary>
		/// This constructor is invoked by Install class methods that have an Install Context built from 
		/// parameters specified in the command line. Rollback, Install, Commit, and intermediate methods like
		/// OnAfterInstall will all be able to use this constructor.
		/// </summary>
		/// <param name="installContext">The install context containing the command line parameters to set the strong types variables to.</param>
		public CustomParameters(InstallContext installContext)
		{
			this._reader = installContext.Parameters[Keys.reader];
			this._url = installContext.Parameters[Keys.url];
			this._direction = installContext.Parameters[Keys.direction];
		}

		/// <summary>
		/// This constructor is used by the Install class methods that don't have an Install Context built
		/// from the command line. This method is primarily used by the Uninstall method.
		/// </summary>
		/// <param name="savedState">An IDictionary object that contains the parameters that were saved from a prior installation.</param>
		public CustomParameters(IDictionary savedState)
		{
			if (savedState.Contains(Keys.url) == true)
				this._url = (string)savedState[Keys.url];

			if (savedState.Contains(Keys.reader) == true)
				this._reader = (string)savedState[Keys.reader];
			if (savedState.Contains(Keys.direction) == true)
				this._direction = (string)savedState[Keys.direction];
		}
	}
}
