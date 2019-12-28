using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FXServerLauncher
{
	public static class Extensions
	{
		public static void Call<TControl>(this TControl control, Action callback) where TControl : Control
		{
			if (control == null || control.IsDisposed == true)
				return;

			control.Invoke(new Action(() => callback()));
		}
	}
}
