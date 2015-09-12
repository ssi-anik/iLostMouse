using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Security;
using iLostMouse_Windows;
using System.IO;
using System.Threading;

namespace iLostMouse
{
    public partial class Form1 : Form
    {
        Process process = new Process();
        Boolean showPasswordInPlainText = false;
        Boolean wlanStarted = false;
        private int[] hashes = {10, 1, 9, 15, 7, 4, 11, 3, 8, 2, 5};
        
        private String textBox_name_hint = "Enter your SSID name";
        private String textBox_password_hint = "Password, atleast 8 characters";
        private String ssid_name = "";
        private String password = "";
        private Boolean start_automatically = false;
        private Queue<String> logQueue = new Queue<string>();

        private Server server = null;
        
        public Form1()
        {
            InitializeComponent();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "netsh";
            FormClosing += Form1_FormClosing;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.stop_server();
            this.stop_hosted_network();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeView();
            readSettings();
            hide_notification_icon_from_tray();
        }

        private void initializeView()
        {
            textBox_name.Text = textBox_name_hint;
            textBox_password.Text = textBox_password_hint;

            textBox_name.GotFocus += new EventHandler(this.nameBox_got_focus);
            textBox_name.LostFocus += new EventHandler(this.nameBox_lost_focus);

            textBox_password.GotFocus += new EventHandler(this.passswordBox_got_focus);
            textBox_password.LostFocus += new EventHandler(this.passswordBox_lost_focus);
        }

        private void readSettings()
        {
            try
            {
                ssid_name = ConfigurationManager.AppSettings["ssid"];
                password = ConfigurationManager.AppSettings["password"];
                start_automatically = ConfigurationManager.AppSettings["start_automatically"] == "0" ? false : true;

                if (null == ssid_name || "" == ssid_name)
                {
                    generateSSID();
                    generatePassword();
                }
                else
                {
                    textBox_name.Text = ssid_name;
                    textBox_name.ForeColor = Color.Black;

                    textBox_password.Text = decryptPassword(password);
                    textBox_password.ForeColor = Color.Black;

                    if (start_automatically)
                    {
                        checkBox_start_automatically.Checked = true;
                        button_start_stop.PerformClick();
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                label_message_text.Text = "Error occurred";
            }
        }

        private void generateSSID()
        {
            string user_name = Environment.UserName;
            if (!user_name.Contains("PC") || !user_name.Contains("pc"))
            {
                user_name += "-PC";
            }
            textBox_name.Text = user_name;
            textBox_name.ForeColor = Color.Black;
        }

        private void generatePassword()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            String generatedPassword = "";

            Random random = new Random();
            generatedPassword = new String(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            textBox_password.Text = generatedPassword;
            textBox_password.ForeColor = Color.Black;
        }

        private String encryptPassword(String password)
        {
            String encrypted = "";
            int i = 0;
            foreach (char c in password)
            {
                encrypted += (char)((int)c + hashes[i]);
                ++i;
                if (i == hashes.Length)
                {
                    i = 0;
                }
            }
            return encrypted;
        }

        private String decryptPassword(String password)
        {
            String decrypted = "";
            int i = 0;
            foreach (char c in password)
            {
                decrypted += (char)((int)c - hashes[i]);
                ++i;
                if (i == hashes.Length)
                {
                    i = 0;
                }
            }
            return decrypted;
        }

        private void button_show_password_Click(object sender, EventArgs e)
        {
            showPasswordInPlainText = !showPasswordInPlainText;
            if (showPasswordInPlainText)
            {
                button_show_password.Text = "Hide";
            }
            else
            {
                button_show_password.Text = "Show";
            }
            togglePasswordText();
        }

        private void togglePasswordText()
        {
            if (textBox_password.Text != textBox_password_hint)
            {
                if (showPasswordInPlainText)
                {
                    textBox_password.PasswordChar = '\0';
                }
                else
                {
                    textBox_password.PasswordChar = '*';
                }
            }
            else
            {
                textBox_password.PasswordChar = '\0';
            }
        }
        
        private void button_start_stop_Click(object sender, EventArgs e)
        {
            label_message_text.Text = "";
            label_message_text.Visible = false;

            if (!wlanStarted)
            {
                if (textBox_name.Text == textBox_name_hint || textBox_name.Text == "")
                {
                    label_message_text.Text = AppHelper.APP_SSID_ERROR_MESSAGE;
                    label_message_text.Visible = true;
                    return;
                }

                if (textBox_password.Text == textBox_password_hint || textBox_password.Text.Length < 8)
                {
                    label_message_text.Text = AppHelper.APP_PASSWORD_ERROR_MESSAGE;
                    label_message_text.Visible = true;
                    return;
                }
                ssid_name = textBox_name.Text;
                password = textBox_password.Text;

                textBox_name.Enabled = false;
                textBox_password.Enabled = false;
                checkBox_start_automatically.Enabled = false;
                label_message_text.Visible = true;

                label_message_text.Text = AppHelper.APP_WLAN_CONNECTED;
                button_start_stop.Text = "Stop";
                notifyIcon.Text = String.Format("{0} - Running", AppHelper.APP_NAME);
                wlanStarted = true;
                try
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                    config.AppSettings.Settings["ssid"].Value = ssid_name;
                    config.AppSettings.Settings["password"].Value = this.encryptPassword(password);
                    config.AppSettings.Settings["start_automatically"].Value = checkBox_start_automatically.Checked ? "1" : "0";
                    config.Save(ConfigurationSaveMode.Modified);
                }
                catch (ConfigurationErrorsException)
                {
                    label_message_text.Text = label_message_text.Text + " / Can't save the settings";
                }

                start_hosted_network();
            }
            else
            {
                textBox_name.Enabled = true;
                textBox_password.Enabled = true;
                label_message_text.Visible = true;
                checkBox_start_automatically.Enabled = true;

                label_message_text.Text = AppHelper.APP_WLAN_DISCONNECTED;
                notifyIcon.Text = String.Format("{0} - Stopped", AppHelper.APP_NAME);
                button_start_stop.Text = "Start";
                wlanStarted = false;
                stop_hosted_network();
            }
        }

        private void nameBox_got_focus(Object sender, EventArgs evt)
        {
            if (textBox_name.Text == textBox_name_hint)
            {
                textBox_name.Text = "";
                textBox_name.ForeColor = Color.Black;
            }
        }

        private void nameBox_lost_focus(Object send, EventArgs evt)
        {
            if (textBox_name.Text == "")
            {
                textBox_name.Text = textBox_name_hint;
                textBox_name.ForeColor = Color.LightGray;
            }
        }

        private void passswordBox_got_focus(Object sender, EventArgs evt)
        {
            if (textBox_password.Text == textBox_password_hint)
            {
                textBox_password.Text = "";
                textBox_password.ForeColor = Color.Black;
            }
        }

        private void passswordBox_lost_focus(Object send, EventArgs evt)
        {
            if (textBox_password.Text == "")
            {
                textBox_password.Text = textBox_password_hint;
                textBox_password.ForeColor = Color.LightGray;
            }
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            togglePasswordText();
        }

        private void start_hosted_network()
        {
            stop_hosted_network();
            using (Process exeProcess = Process.Start(process.StartInfo))
            {
                exeProcess.WaitForExit();
                create_new_network();
            }
            start_server();
        }

        public void stop_hosted_network()
        {
            if (null != server)
            {
                stop_server();
            }

            process.StartInfo.Arguments = "wlan stop hostednetwork";
            using (Process exeProcess = Process.Start(process.StartInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        private void start_server()
        {
            server = new Server(this);
            server.start_listening();
        }

        private void stop_server()
        {
            if (null != server)
            {
                server.stop_listening();
                server = null;
            }
        }

        private void create_new_network()
        {
            process.StartInfo.Arguments = "wlan set hostednetwork mode=allow ssid=" + ssid_name + " key=" + password;
            using (Process executeProcess = Process.Start(process.StartInfo))
            {
                executeProcess.WaitForExit();
                start_created_network();
            }
        }

        private void start_created_network()
        {
            process.StartInfo.Arguments = "wlan start hostednetwork";
            using (Process exeProcess = Process.Start(process.StartInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool cursorNotInBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);
            if (FormWindowState.Minimized == WindowState && cursorNotInBar)
            {
                Hide();
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            hide_notification_icon_from_tray();
            WindowState = FormWindowState.Normal;
        }

        public void hide_notification_icon_from_tray()
        {
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void restore_context_menu_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void close_context_menu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void add_log(String log)
        {
            logQueue.Enqueue(log);

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += add_item_to_listBox_ui;
            backgroundWorker.RunWorkerAsync();
        }

        private delegate void UpdateUIDelegate();
        
        void add_item_to_listBox_ui(object sender, DoWorkEventArgs e)
        {
            listBox_logs.Invoke(new UpdateUIDelegate(updateListBox));
        }

        private void updateListBox()
        {
            while (logQueue.Count() > 0)
            {
                String log = logQueue.Dequeue();
                listBox_logs.Items.Add(log);
            }
                    
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            listBox_logs.Items.Clear();
        }

        private void textBox_name_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button_start_stop_Click(sender, e);
            }
        }

        private void textBox_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button_start_stop_Click(sender, e);
            }
        }

    }
}
