namespace iLostMouse
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_name = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_show_password = new System.Windows.Forms.Button();
            this.button_start_stop = new System.Windows.Forms.Button();
            this.label_message_text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_start_automatically = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.context_menu_for_notification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restore_context_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.close_context_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_logs = new System.Windows.Forms.ListBox();
            this.label_logs = new System.Windows.Forms.Label();
            this.button_clear = new System.Windows.Forms.Button();
            this.context_menu_for_notification.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.Location = new System.Drawing.Point(48, 57);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(35, 15);
            this.label_name.TabIndex = 1004;
            this.label_name.Text = "Name";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_password.Location = new System.Drawing.Point(15, 87);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(63, 15);
            this.label_password.TabIndex = 1005;
            this.label_password.Text = "Password";
            // 
            // textBox_name
            // 
            this.textBox_name.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_name.ForeColor = System.Drawing.Color.LightGray;
            this.textBox_name.Location = new System.Drawing.Point(96, 51);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(331, 23);
            this.textBox_name.TabIndex = 1;
            this.textBox_name.Text = "Enter your SSID name";
            this.textBox_name.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_name_KeyUp);
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_password.ForeColor = System.Drawing.Color.LightGray;
            this.textBox_password.Location = new System.Drawing.Point(96, 84);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(331, 23);
            this.textBox_password.TabIndex = 2;
            this.textBox_password.Text = "Password, atleast 8 characters";
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            this.textBox_password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_password_KeyUp);
            // 
            // button_show_password
            // 
            this.button_show_password.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_show_password.Location = new System.Drawing.Point(440, 84);
            this.button_show_password.Name = "button_show_password";
            this.button_show_password.Size = new System.Drawing.Size(69, 23);
            this.button_show_password.TabIndex = 5;
            this.button_show_password.Text = "Show";
            this.button_show_password.UseVisualStyleBackColor = true;
            this.button_show_password.Click += new System.EventHandler(this.button_show_password_Click);
            // 
            // button_start_stop
            // 
            this.button_start_stop.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start_stop.Location = new System.Drawing.Point(440, 51);
            this.button_start_stop.Name = "button_start_stop";
            this.button_start_stop.Size = new System.Drawing.Size(69, 23);
            this.button_start_stop.TabIndex = 4;
            this.button_start_stop.Text = "Start";
            this.button_start_stop.UseVisualStyleBackColor = true;
            this.button_start_stop.Click += new System.EventHandler(this.button_start_stop_Click);
            // 
            // label_message_text
            // 
            this.label_message_text.AutoSize = true;
            this.label_message_text.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_message_text.Location = new System.Drawing.Point(92, 152);
            this.label_message_text.Name = "label_message_text";
            this.label_message_text.Size = new System.Drawing.Size(126, 15);
            this.label_message_text.TabIndex = 1002;
            this.label_message_text.Text = "wlan disconnected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 15);
            this.label1.TabIndex = 1001;
            this.label1.Text = "Your settings are always saved";
            // 
            // checkBox_start_automatically
            // 
            this.checkBox_start_automatically.AutoSize = true;
            this.checkBox_start_automatically.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_start_automatically.Location = new System.Drawing.Point(96, 119);
            this.checkBox_start_automatically.Name = "checkBox_start_automatically";
            this.checkBox_start_automatically.Size = new System.Drawing.Size(159, 19);
            this.checkBox_start_automatically.TabIndex = 3;
            this.checkBox_start_automatically.Text = "Start automatically";
            this.checkBox_start_automatically.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.context_menu_for_notification;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "i lost MOUSE";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // context_menu_for_notification
            // 
            this.context_menu_for_notification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.context_menu_for_notification.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.context_menu_for_notification.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restore_context_menu,
            this.close_context_menu});
            this.context_menu_for_notification.Name = "context_menu_for_notification";
            this.context_menu_for_notification.ShowImageMargin = false;
            this.context_menu_for_notification.Size = new System.Drawing.Size(99, 48);
            // 
            // restore_context_menu
            // 
            this.restore_context_menu.Name = "restore_context_menu";
            this.restore_context_menu.Size = new System.Drawing.Size(98, 22);
            this.restore_context_menu.Text = "Restore";
            this.restore_context_menu.Click += new System.EventHandler(this.restore_context_menu_Click);
            // 
            // close_context_menu
            // 
            this.close_context_menu.Name = "close_context_menu";
            this.close_context_menu.Size = new System.Drawing.Size(98, 22);
            this.close_context_menu.Text = "Close";
            this.close_context_menu.Click += new System.EventHandler(this.close_context_menu_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.restoreToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // listBox_logs
            // 
            this.listBox_logs.FormattingEnabled = true;
            this.listBox_logs.HorizontalScrollbar = true;
            this.listBox_logs.ItemHeight = 15;
            this.listBox_logs.Location = new System.Drawing.Point(96, 188);
            this.listBox_logs.Name = "listBox_logs";
            this.listBox_logs.Size = new System.Drawing.Size(331, 109);
            this.listBox_logs.TabIndex = 1003;
            // 
            // label_logs
            // 
            this.label_logs.AutoSize = true;
            this.label_logs.Location = new System.Drawing.Point(43, 188);
            this.label_logs.Name = "label_logs";
            this.label_logs.Size = new System.Drawing.Size(35, 15);
            this.label_logs.TabIndex = 1006;
            this.label_logs.Text = "Logs";
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(440, 188);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(69, 109);
            this.button_clear.TabIndex = 6;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 316);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.label_logs);
            this.Controls.Add(this.listBox_logs);
            this.Controls.Add(this.checkBox_start_automatically);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_message_text);
            this.Controls.Add(this.button_start_stop);
            this.Controls.Add(this.button_show_password);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_name);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "iLostMouse";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.context_menu_for_notification.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_show_password;
        private System.Windows.Forms.Button button_start_stop;
        private System.Windows.Forms.Label label_message_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_start_automatically;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip context_menu_for_notification;
        private System.Windows.Forms.ToolStripMenuItem restore_context_menu;
        private System.Windows.Forms.ToolStripMenuItem close_context_menu;
        private System.Windows.Forms.ListBox listBox_logs;
        private System.Windows.Forms.Label label_logs;
        private System.Windows.Forms.Button button_clear;
    }
}

