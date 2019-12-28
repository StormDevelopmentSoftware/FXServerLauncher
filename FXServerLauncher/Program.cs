using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FXServerLauncher
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			var mainform = new MainForm();

			using (mainform)
				mainform.ShowDialog();
		}
	}
}
