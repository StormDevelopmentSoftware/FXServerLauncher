namespace FXServerLauncher
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.txtInput = new System.Windows.Forms.TextBox();
			this._process = new System.Diagnostics.Process();
			this._notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// txtOutput
			// 
			this.txtOutput.BackColor = System.Drawing.SystemColors.WindowText;
			this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtOutput.DetectUrls = false;
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOutput.ForeColor = System.Drawing.SystemColors.Window;
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(0, 0);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(800, 430);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			this.txtOutput.WordWrap = false;
			this.txtOutput.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// txtInput
			// 
			this.txtInput.BackColor = System.Drawing.SystemColors.Window;
			this.txtInput.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtInput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtInput.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInput.Location = new System.Drawing.Point(0, 430);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(800, 20);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "cmdlist";
			this.txtInput.WordWrap = false;
			this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			// 
			// _process
			// 
			this._process.EnableRaisingEvents = true;
			this._process.StartInfo.CreateNoWindow = true;
			this._process.StartInfo.Domain = "";
			this._process.StartInfo.LoadUserProfile = false;
			this._process.StartInfo.Password = null;
			this._process.StartInfo.RedirectStandardError = true;
			this._process.StartInfo.RedirectStandardInput = true;
			this._process.StartInfo.RedirectStandardOutput = true;
			this._process.StartInfo.StandardErrorEncoding = null;
			this._process.StartInfo.StandardOutputEncoding = null;
			this._process.StartInfo.UserName = "";
			this._process.StartInfo.UseShellExecute = false;
			this._process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			this._process.SynchronizingObject = this;
			this._process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.NotifyOutputDataReceived);
			this._process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.NotifyOutputDataReceived);
			this._process.Exited += new System.EventHandler(this.NotifyExit);
			// 
			// _notify
			// 
			this._notify.Text = "FiveM: Dedicated Server";
			this._notify.Visible = true;
			this._notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._notify_MouseDoubleClick);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.txtInput);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FiveM: Dedicated Server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotifyFormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtOutput;
		private System.Windows.Forms.TextBox txtInput;
		private System.Diagnostics.Process _process;
		private System.Windows.Forms.NotifyIcon _notify;
	}
}

