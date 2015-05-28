﻿namespace GZBServer
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.checkOnlineUserTimer = new System.Windows.Forms.Timer(this.components);
            this.ClearUserOnlineInfoButton = new System.Windows.Forms.Button();
            this.secondOnlineUserTextBox = new System.Windows.Forms.TextBox();
            this.ClearAllUserOnlineInfoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkOnlineUserTimer
            // 
            this.checkOnlineUserTimer.Interval = 5000;
            this.checkOnlineUserTimer.Tick += new System.EventHandler(this.checkOnlineUserTimer_Tick);
            // 
            // ClearUserOnlineInfoButton
            // 
            this.ClearUserOnlineInfoButton.ForeColor = System.Drawing.Color.Black;
            this.ClearUserOnlineInfoButton.Location = new System.Drawing.Point(48, 123);
            this.ClearUserOnlineInfoButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ClearUserOnlineInfoButton.Name = "ClearUserOnlineInfoButton";
            this.ClearUserOnlineInfoButton.Size = new System.Drawing.Size(129, 36);
            this.ClearUserOnlineInfoButton.TabIndex = 18;
            this.ClearUserOnlineInfoButton.Text = "立即清理";
            this.ClearUserOnlineInfoButton.UseVisualStyleBackColor = true;
            this.ClearUserOnlineInfoButton.Click += new System.EventHandler(this.ClearUserOnlineInfoButton_Click);
            // 
            // secondOnlineUserTextBox
            // 
            this.secondOnlineUserTextBox.Location = new System.Drawing.Point(119, 70);
            this.secondOnlineUserTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.secondOnlineUserTextBox.Name = "secondOnlineUserTextBox";
            this.secondOnlineUserTextBox.Size = new System.Drawing.Size(58, 25);
            this.secondOnlineUserTextBox.TabIndex = 17;
            this.secondOnlineUserTextBox.Text = "120";
            this.secondOnlineUserTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ClearAllUserOnlineInfoButton
            // 
            this.ClearAllUserOnlineInfoButton.ForeColor = System.Drawing.Color.Red;
            this.ClearAllUserOnlineInfoButton.Location = new System.Drawing.Point(48, 183);
            this.ClearAllUserOnlineInfoButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ClearAllUserOnlineInfoButton.Name = "ClearAllUserOnlineInfoButton";
            this.ClearAllUserOnlineInfoButton.Size = new System.Drawing.Size(129, 36);
            this.ClearAllUserOnlineInfoButton.TabIndex = 19;
            this.ClearAllUserOnlineInfoButton.Text = "清理所有在线";
            this.ClearAllUserOnlineInfoButton.UseVisualStyleBackColor = true;
            this.ClearAllUserOnlineInfoButton.Click += new System.EventHandler(this.ClearAllUserOnlineInfoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "间隔时间:";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.LogTextBox.Location = new System.Drawing.Point(217, 12);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(350, 281);
            this.LogTextBox.TabIndex = 21;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 305);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClearAllUserOnlineInfoButton);
            this.Controls.Add(this.ClearUserOnlineInfoButton);
            this.Controls.Add(this.secondOnlineUserTextBox);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "管账宝服务端";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer checkOnlineUserTimer;
        private System.Windows.Forms.Button ClearUserOnlineInfoButton;
        private System.Windows.Forms.TextBox secondOnlineUserTextBox;
        private System.Windows.Forms.Button ClearAllUserOnlineInfoButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LogTextBox;
    }
}
