﻿namespace TOCTest.forms
{
    partial class User
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
            this.textBoxUserSernum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBPwd = new System.Windows.Forms.TextBox();
            this.tBUserName = new System.Windows.Forms.TextBox();
            this.m_btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUserSernum
            // 
            this.textBoxUserSernum.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.textBoxUserSernum.HideSelection = false;
            this.textBoxUserSernum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxUserSernum.Location = new System.Drawing.Point(133, 194);
            this.textBoxUserSernum.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUserSernum.Name = "textBoxUserSernum";
            this.textBoxUserSernum.Size = new System.Drawing.Size(186, 27);
            this.textBoxUserSernum.TabIndex = 81;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.label4.Location = new System.Drawing.Point(23, 197);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 23);
            this.label4.TabIndex = 80;
            this.label4.Tag = "";
            this.label4.Text = "user NO.：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.label2.Location = new System.Drawing.Point(17, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 76;
            this.label2.Tag = "";
            this.label2.Text = "user name：";
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBoxLevel.Location = new System.Drawing.Point(133, 143);
            this.comboBoxLevel.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(186, 25);
            this.comboBoxLevel.TabIndex = 79;
            this.comboBoxLevel.Text = "4";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.label1.Location = new System.Drawing.Point(28, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 23);
            this.label1.TabIndex = 74;
            this.label1.Tag = "";
            this.label1.Text = "password：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.label3.Location = new System.Drawing.Point(54, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 78;
            this.label3.Tag = "";
            this.label3.Text = "level：";
            // 
            // tBPwd
            // 
            this.tBPwd.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.tBPwd.HideSelection = false;
            this.tBPwd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tBPwd.Location = new System.Drawing.Point(133, 90);
            this.tBPwd.Margin = new System.Windows.Forms.Padding(4);
            this.tBPwd.Name = "tBPwd";
            this.tBPwd.PasswordChar = '*';
            this.tBPwd.Size = new System.Drawing.Size(186, 27);
            this.tBPwd.TabIndex = 75;
            // 
            // tBUserName
            // 
            this.tBUserName.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.tBUserName.HideSelection = false;
            this.tBUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tBUserName.Location = new System.Drawing.Point(133, 37);
            this.tBUserName.Margin = new System.Windows.Forms.Padding(4);
            this.tBUserName.Name = "tBUserName";
            this.tBUserName.Size = new System.Drawing.Size(186, 27);
            this.tBUserName.TabIndex = 77;
            // 
            // m_btnConfirm
            // 
            this.m_btnConfirm.Location = new System.Drawing.Point(40, 242);
            this.m_btnConfirm.Name = "m_btnConfirm";
            this.m_btnConfirm.Size = new System.Drawing.Size(86, 25);
            this.m_btnConfirm.TabIndex = 82;
            this.m_btnConfirm.Text = "confirm";
            this.m_btnConfirm.UseVisualStyleBackColor = true;
            this.m_btnConfirm.Click += new System.EventHandler(this.m_btnConfirm_Click);
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 298);
            this.Controls.Add(this.m_btnConfirm);
            this.Controls.Add(this.textBoxUserSernum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBPwd);
            this.Controls.Add(this.tBUserName);
            this.Name = "User";
            this.Text = "User";
            this.Load += new System.EventHandler(this.User_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUserSernum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBPwd;
        private System.Windows.Forms.TextBox tBUserName;
        private System.Windows.Forms.Button m_btnConfirm;
    }
}