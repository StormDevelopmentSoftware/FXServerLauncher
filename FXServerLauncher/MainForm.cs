using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FXServerLauncher
{
	public partial class MainForm : Form
	{
		private Task _taskOutput;
		private Task _taskError;
		private CancellationTokenSource _cts;
		private volatile bool _closing = false;
		private volatile bool _shutdown = false;
		private volatile bool _shutdownSignal = false;

		public MainForm()
		{
			InitializeComponent();
		}

		protected void OnProcessOutput(object sender, DataReceivedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Data) && string.IsNullOrWhiteSpace(e.Data))
				return;

			var message = e.Data;

			if (message.ToLowerInvariant().Contains("authenticating with nucleus"))
				return;

			Log(message);
		}

		protected void OnProcessExit(object sender, EventArgs e)
		{
			if (!_cts.IsCancellationRequested)
				_cts.Cancel();

			txtOutput.Call(() => txtOutput.Enabled = false);
			txtInput.Call(() => txtInput.Enabled = false);

			UseWaitCursor = true;

			_ = Task.Run(async () =>
			{
				if (_taskError != null && _taskOutput != null)
				{

					while (_taskError.IsCompleted == false && _taskOutput.IsCompleted == false)
						await Task.Delay(100);
				}

				_shutdown = true;
				_closing = true;
				this.Call(() => Close());
			});
		}

		protected void Log(string message)
		{
			if (txtOutput.InvokeRequired)
				txtOutput.Invoke(new Action<string>(Log), new object[] { message });
			else
			{
				message = Regex.Replace(message, @"\[[0-9]+m", string.Empty);

				txtOutput.AppendText($"[{DateTime.Now:HH:mm:ss}]: {message}{Environment.NewLine}");
			}
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			Icon = _notify.Icon = Properties.Resources.fivem;

			_cts = new CancellationTokenSource();

			var filename = Path.Combine(Directory.GetCurrentDirectory(), "FXServer.exe");
			goto validate;

			validate:
			{
				if (!File.Exists(filename))
				{
					using (var ofd = new OpenFileDialog())
					{
						ofd.Filter = "FiveM Dedicated Server|FXServer.exe";

						if (ofd.ShowDialog(this) != DialogResult.OK)
						{
							if (MessageBox.Show(this, "Cannot initialize FX Server!", "FXServer.exe not found in current directory!",
									MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
								Environment.Exit(1);
							else
								goto validate;
						}

						filename = ofd.FileName;
					}
				}
				goto startserver;
			}

			startserver:
			{
				var path = Path.GetDirectoryName(filename);

				if (!Directory.Exists(Path.Combine(path, "citizen")))
				{
					if (MessageBox.Show(this, "Invalid FiveM dedicated server installation directory!",
							"Citizen not found!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Cancel)
						goto validate;
					else
						Environment.Exit(2);
				}

				_process.StartInfo = new ProcessStartInfo();
				_process.StartInfo.RedirectStandardError =
					_process.StartInfo.RedirectStandardInput =
					_process.StartInfo.RedirectStandardOutput = true;

				_process.StartInfo.UseShellExecute = false;
				_process.StartInfo.CreateNoWindow = true;

				_process.StartInfo.FileName = filename;
				_process.StartInfo.WorkingDirectory = path;
				_process.StartInfo.Arguments = string.Join(" ", new string[]
				{
					string.Concat("+set citizen_dir ", @"""" + $@"{path}\citizen" + @""""),
					string.Concat("+exec ", @"""" + Path.Combine(path, "server.cfg") + @""""),
					string.Join(" ", Environment.GetCommandLineArgs().Skip(1))
				});

				_process.Start();

				_taskOutput = Task.Factory.StartNew(() => _process.BeginOutputReadLine(), _cts.Token);
				_taskError = Task.Factory.StartNew(() => _process.BeginErrorReadLine(), _cts.Token);
			}
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (!_shutdown)
				{
					if (!_shutdownSignal)
					{
						_process.StandardInput.WriteLine("quit");
						_shutdownSignal = true;
					}

					_shutdown = true;
				}

				e.Cancel = true;
			}

			if (_closing)
				e.Cancel = false;
			else
				e.Cancel = true;
		}

		private void OnOutputTextChanged(object sender, EventArgs e)
		{
			txtOutput.ScrollToCaret();
		}

		private void OnInputKeyUp(object sender, KeyEventArgs e)
		{
			if (!txtInput.Enabled)
				return;

			if (e.KeyData != Keys.Enter)
				return;

			if (!txtInput.ContainsFocus)
				return;

			var raw = txtInput.Text;
			txtInput.Clear();

			if (raw.StartsWith("#"))
			{
				raw = raw.Substring(1);

				switch (raw)
				{
					case "clear":
					txtOutput.Clear();
					break;

					case "uptime":
					Log($"Process Uptime: {_process.StartTime.Subtract(DateTime.Now):hh\\:mm\\:ss}");
					break;

					case "threads":
					Log($"Process Threads: #{_process.Threads?.Count ?? -1}");
					break;

					case "priority":
					Log($"Process Priority: {_process.PriorityClass.ToString()}");
					break;

					case "pid":
					Log($"Process Identifier: {_process.Id}");
					break;

					default:
					goto forward;
				}

				return;
			}

			forward:
			{

				if (raw.Equals("quit", StringComparison.InvariantCultureIgnoreCase))
					_shutdown = true;

				if (!_cts.IsCancellationRequested)
					_process.StandardInput.WriteLine(raw);
			}
		}

		private volatile bool _isHidden = false;

		private void OnNotifyIconClick(object sender, EventArgs e)
		{
			if (_isHidden)
			{
				ShowInTaskbar = true;
				Opacity = 1f;
				_isHidden = false;
			}
			else
			{
				_isHidden = true;
				ShowInTaskbar = false;
				Opacity = 0f;
			}
		}
	}
}