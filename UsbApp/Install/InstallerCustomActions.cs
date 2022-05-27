using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace kppApp.Install
{
    [RunInstaller(true)]
    public partial class InstallerCustomActions : System.Configuration.Install.Installer
    {
        public InstallerCustomActions()
        {
            InitializeComponent();
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            // Get the custom parameters from the install context.
            CustomParameters customParameters = new CustomParameters(this.Context);

            SaveCustomParametersInStateSaverDictionary(stateSaver, customParameters);

            //PrintMessage("The application is being installed.", customParameters);

            base.Install(stateSaver);
        }

		private void SaveCustomParametersInStateSaverDictionary(System.Collections.IDictionary stateSaver, CustomParameters customParameters)
		{
			var SpecialDataFolder = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)).FullName, "PSISoftware", "AppKPP");
			var xFile = Path.Combine(SpecialDataFolder, "presettings.json");
			var text1 = $"{{\"restapi_path\":\"{customParameters.url}\", \"passage_direction\":\"{customParameters.direction}\", \"reader_id\":\"{customParameters.reader}\"}}";
			File.WriteAllText(xFile, text1);
		}

	}
}
