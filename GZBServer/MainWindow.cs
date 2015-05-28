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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            checkOnlineUserTimer.Enabled = true;
            addLog("服务端已启动...");
        }

        private void checkOnlineUserTimer_Tick(object sender, EventArgs e)
        {
            checkOnlineExpireUser("id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > 80 AND GZB_isonline = 1)");
        }

        private void ClearUserOnlineInfoButton_Click(object sender, EventArgs e)
        {
            checkOnlineExpireUser("id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > 80 AND GZB_isonline = 1)");
        }

        private void ClearAllUserOnlineInfoButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认要清空所有在线用户?", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                checkOnlineExpireUser("");
            }
        }

        private void checkOnlineExpireUser(String condition)
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
                String sql = "UPDATE users SET GZB_isonline = 0 WHERE id IN (SELECT id FROM (SELECT * FROM users) AS online WHERE TIMESTAMPDIFF(SECOND,GZB_lastlogontime,NOW()) > @time AND GZB_isonline = 1)";
                List<MySqlParameter> Paramter = new List<MySqlParameter>();
                Paramter.Add(new MySqlParameter("@time", secondOnlineUserTextBox.Text));

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

        private void addLog(String str)
        {
            LogTextBox.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + str + "\r\n";
        }

    }
}
