namespace Anzu
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
			this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
			this.metroButton1 = new MetroFramework.Controls.MetroButton();
			this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
			this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
			this.metroTabPage6 = new MetroFramework.Controls.MetroTabPage();
			this.metroTabPage7 = new MetroFramework.Controls.MetroTabPage();
			this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.metroTabControl1.SuspendLayout();
			this.metroTabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// metroTabControl1
			// 
			resources.ApplyResources(this.metroTabControl1, "metroTabControl1");
			this.metroTabControl1.Controls.Add(this.metroTabPage1);
			this.metroTabControl1.Controls.Add(this.metroTabPage4);
			this.metroTabControl1.Controls.Add(this.metroTabPage5);
			this.metroTabControl1.Controls.Add(this.metroTabPage6);
			this.metroTabControl1.Controls.Add(this.metroTabPage7);
			this.metroTabControl1.Multiline = true;
			this.metroTabControl1.Name = "metroTabControl1";
			this.metroTabControl1.SelectedIndex = 0;
			// 
			// metroTabPage1
			// 
			this.metroTabPage1.Controls.Add(this.metroLabel1);
			this.metroTabPage1.Controls.Add(this.metroTextBox1);
			this.metroTabPage1.Controls.Add(this.metroButton1);
			this.metroTabPage1.HorizontalScrollbarBarColor = true;
			resources.ApplyResources(this.metroTabPage1, "metroTabPage1");
			this.metroTabPage1.Name = "metroTabPage1";
			this.metroTabPage1.VerticalScrollbarBarColor = true;
			// 
			// metroButton1
			// 
			resources.ApplyResources(this.metroButton1, "metroButton1");
			this.metroButton1.Name = "metroButton1";
			// 
			// metroTabPage4
			// 
			this.metroTabPage4.HorizontalScrollbarBarColor = true;
			resources.ApplyResources(this.metroTabPage4, "metroTabPage4");
			this.metroTabPage4.Name = "metroTabPage4";
			this.metroTabPage4.VerticalScrollbarBarColor = true;
			// 
			// metroTabPage5
			// 
			this.metroTabPage5.HorizontalScrollbarBarColor = true;
			resources.ApplyResources(this.metroTabPage5, "metroTabPage5");
			this.metroTabPage5.Name = "metroTabPage5";
			this.metroTabPage5.VerticalScrollbarBarColor = true;
			// 
			// metroTabPage6
			// 
			this.metroTabPage6.HorizontalScrollbarBarColor = true;
			resources.ApplyResources(this.metroTabPage6, "metroTabPage6");
			this.metroTabPage6.Name = "metroTabPage6";
			this.metroTabPage6.VerticalScrollbarBarColor = true;
			// 
			// metroTabPage7
			// 
			this.metroTabPage7.HorizontalScrollbarBarColor = true;
			resources.ApplyResources(this.metroTabPage7, "metroTabPage7");
			this.metroTabPage7.Name = "metroTabPage7";
			this.metroTabPage7.VerticalScrollbarBarColor = true;
			// 
			// metroTextBox1
			// 
			resources.ApplyResources(this.metroTextBox1, "metroTextBox1");
			this.metroTextBox1.Name = "metroTextBox1";
			this.metroTextBox1.PromptText = "Backup folder";
			// 
			// metroLabel1
			// 
			resources.ApplyResources(this.metroLabel1, "metroLabel1");
			this.metroLabel1.Name = "metroLabel1";
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AllowDrop = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.metroTabControl1);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.Style = MetroFramework.MetroColorStyle.White;
			this.TextAlign = System.Windows.Forms.VisualStyles.HorizontalAlign.Center;
			this.Theme = MetroFramework.MetroThemeStyle.Light;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.metroTabControl1.ResumeLayout(false);
			this.metroTabPage1.ResumeLayout(false);
			this.metroTabPage1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private MetroFramework.Controls.MetroTabControl metroTabControl1;
		private MetroFramework.Controls.MetroTabPage metroTabPage1;
		private MetroFramework.Controls.MetroTabPage metroTabPage4;
		private MetroFramework.Controls.MetroTabPage metroTabPage5;
		private MetroFramework.Controls.MetroTabPage metroTabPage6;
		private MetroFramework.Controls.MetroTabPage metroTabPage7;
		private MetroFramework.Controls.MetroButton metroButton1;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroTextBox metroTextBox1;
	}
}

