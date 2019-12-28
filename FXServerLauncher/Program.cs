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
			Application.ThreadException += OnThreadException;
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		static void OnThreadException(object sender, ThreadExceptionEventArgs e)
		{
			var ex = e.Exception;

			if(ex is AggregateException aggregateException)
			{
				if (aggregateException.InnerExceptions.Any(x => x is ObjectDisposedException))
					return;
			}

			while (ex is AggregateException)
				ex = ex.InnerException;

			if (ex is ObjectDisposedException)
				return;

			MessageBox.Show(ex.ToString(), ex.Message,
				MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
