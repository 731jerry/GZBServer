using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace GZBServer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int checkOnlineUserTimerCount = 0;
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //checkOnlineUserTimer_function();
            checkOnlineUserTimer.Enabled = true;
            addLog("服务端已启动...");
            addLog("已连接到" + DatabaseManager.Ins.ConnStr);
        }

        private void checkOnlineUserTimer_Tick(object sender, EventArgs e)
        {
            checkOnlineUserTimer_function();
            getAppVersion();
            checkOnlineUserTimerCount++;
        }

        private void checkOnlineUserTimer_function()
        {
            String sql = "UPDATE users SET GZB_isonline = 0 WHERE id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > @time AND GZB_isonline = 1)";
            List<MySqlParameter> Paramter = new List<MySqlParameter>();
            Paramter.Add(new MySqlParameter("@time", secondOnlineUserTextBox.Text));
            checkOnlineExpireUser(sql, Paramter);
            checkOnlineUser();
        }

        private void ClearUserOnlineInfoButton_Click(object sender, EventArgs e)
        {
            String sql = "UPDATE users SET GZB_isonline = 0 WHERE id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > @time AND GZB_isonline = 1)";
            List<MySqlParameter> Paramter = new List<MySqlParameter>();
            Paramter.Add(new MySqlParameter("@time", secondOnlineUserTextBox.Text));
            checkOnlineExpireUser(sql, Paramter);
        }

        private void ClearAllUserOnlineInfoButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认要清空所有在线用户?", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                String sql = "UPDATE users SET GZB_isonline = @value";
                List<MySqlParameter> Paramter = new List<MySqlParameter>();
                Paramter.Add(new MySqlParameter("@value", "0"));
                checkOnlineExpireUser(sql, Paramter);
            }
        }

        private void checkOnlineExpireUser(String sql, List<MySqlParameter> Paramter)
        {
            //UPDATE users SET GZB_isonline = 0 WHERE id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > 80 AND GZB_isonline = 1)
            try
            {
                //int affactedRows = DatabaseConnector.OnlineConnector().OnlineUpdateData("users", new List<String>() { "GZB_isonline" }, new List<String>() { "0" }, condition);

                /*
                string sql = @"select * from log_account where (createtime between @startTime and @endTime) and (serverinfo like @serverinfo)";
                List<MySqlParameter> Paramter = new List<MySqlParameter>();
                Paramter.Add(new MySqlParameter("@startTime", startTime));
                Paramter.Add(new MySqlParameter("@endTime", endTime));
                Paramter.Add(new MySqlParameter("@serverinfo", (ConfManager.Ins.currentConf.serverid + "-%")));
                DataTable data = DbManager.Ins.ExcuteDataTable(sql, Paramter.ToArray());  
                */

                int affactedRows = DatabaseManager.Ins.ExecuteNonquery(sql, Paramter.ToArray());
                if (affactedRows > 0)
                {
                    addLog("checkOnlineExpireUser影响用户 " + affactedRows + " 个");
                }
            }
            catch (Exception ex)
            {
                addLog("checkOnlineExpireUser出现错误 " + ex.Message);
            }
        }

        private void checkOnlineUser()
        {
            try
            {
                String onlineSql = @"select COUNT(*) from users where GZB_isonline = 1";
                String userSql = @"select COUNT(*) from users";

                int onlineResultList = Convert.ToInt32(DatabaseManager.Ins.ExecuteScalar(onlineSql, null));
                int userResultList = Convert.ToInt32(DatabaseManager.Ins.ExecuteScalar(userSql, null));

                String log = "在线用户:" + onlineResultList.ToString() + "/" + userResultList.ToString();
                if (checkOnlineUserTimerCount % 360 == 0) // 半小时
                {
                    checkOnlineUserTimerCount = 0;
                    addLog(log);
                }
                onlineUserLabel.Text = log;
            }
            catch (Exception ex)
            {
                addLog("checkOnlineUser出现错误 " + ex.Message);
            }
        }

        private String getAppVersion()
        {
            String versionResult = "";
            try
            {
                String Sql = @"select configValue from config where configKey = 'GZB_update_version'";

                versionResult = (String)(DatabaseManager.Ins.ExecuteScalar(Sql, null));

                String log = "当前版本:" + versionResult.ToString();
                if (checkOnlineUserTimerCount % 360 == 0) // 半小时
                {
                    addLog(log);
                }
                getAppVersionTextBox.Text = versionResult;
            }
            catch (Exception ex)
            {
                addLog("getAppVersion出现错误 " + ex.Message);
            }
            return versionResult;
        }

        private void setAppVersionButton_Click(object sender, EventArgs e)
        {
            try
            {
                String Sql = @"UPDATE config SET configValue = '" + getAppVersionTextBox.Text + "' where configKey = 'GZB_update_version'";

                DatabaseManager.Ins.ExecuteNonquery(Sql, null);

                String log = "已设置版本:" + getAppVersionTextBox.Text;
                addLog(log);
            }
            catch (Exception ex)
            {
                addLog("getAppVersion出现错误 " + ex.Message);
            }
        }

        private void addLog(String str)
        {
            String log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + str + "\r\n";
            LogTextBox.Text += log;
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
            RecordLog(log);
        }

        // 记录错误
        private void RecordLog(String logString)
        {
            //String log = preLog + "\r\n###异常消息Message: " + ex.Message + "\r\n###当前异常StackTrace: " + ex.StackTrace + "\r\n===================================================\r\n";
            String fileName = System.Environment.CurrentDirectory + @"\log.txt";
            String fileString = "";
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fszz = System.IO.File.OpenRead(fileName))
                {
                    byte[] bytes = new byte[fszz.Length];
                    fszz.Read(bytes, 0, bytes.Length);

                    fileString = Encoding.UTF8.GetString(bytes);
                }
            }
            using (System.IO.FileStream fs = System.IO.File.Create(fileName))
            {
                StringBuilder sb = new StringBuilder();
                byte[] info = new UTF8Encoding().GetBytes(fileString + logString);
                fs.Write(info, 0, info.Length);
            }
        }

    }
}
