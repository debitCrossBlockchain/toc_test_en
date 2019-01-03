using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TOCTest.forms
{
    public partial class User : Form
    {
        string m_strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\UserInfo.mdb";
        private OleDbConnection mConnection;
        public User()
        {
            InitializeComponent();
        }

        public void Insert()
        {
            try
            {
                if (CheckAuthority())
                {
                    MessageBox.Show("There is a problem with user permissions. Please check user permissions carefully！");
                    return;
                }

                if (tBUserName.Text.ToString() == "" || tBPwd.Text == "" || textBoxUserSernum.Text == "")
                {
                    MessageBox.Show("Information such as user name or password is not empty！");
                    return;
                }

                bool bExist = db.dbMrg.QueryExistDB(m_strConn, "Select * From t_UserInfo where [userName]='" + tBUserName.Text + "'");
                if (bExist)
                {
                    MessageBox.Show("User already exists and cannot be added！");
                    return;
                }

                UserInfo objUserInfo = UserInfo.Instance;
                db.dbMrg.AddUserInfo(tBUserName.Text, tBPwd.Text, (comboBoxLevel.SelectedIndex + 1).ToString(), textBoxUserSernum.Text);
                db.dbMrg.AddAlertInfo("01", "01", objUserInfo.getUserName(), DateTime.Now.ToString(), "01", "user management add user name is：" + tBUserName.Text);
                utils.loghelp.log.Debug("add user is：" + tBUserName.Text+",time is:"+DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
        }

        public void Alert(string strIndex)
        {
            try
            {
                UserInfo objUserInfo = UserInfo.Instance;
                int nLevel = Convert.ToInt32(objUserInfo.getUserLevel());
                int ncbLevel = comboBoxLevel.SelectedIndex + 1;
                if (nLevel > ncbLevel)
                {
                    MessageBox.Show("There is a problem with user permissions. Please check user permissions carefully！");
                    return;
                }

                if (tBUserName.Text.ToString() == "" || tBPwd.Text == "" || textBoxUserSernum.Text == "")
                {
                    MessageBox.Show("Information such as user name or password is not empty！");
                    return;
                }
                string strQuery = "update {0} set [userName]='" + "{1}" + "',[userPwd]='" + "{2}" + "',[userLevel]='" + "{3}" + "',[userSernum]='" + "{4}" + "'where [userName]='" + tBUserName.Text.Trim() + "'";
                strQuery = string.Format(strQuery, "t_UserInfo", tBUserName.Text.ToString(), tBPwd.Text, (comboBoxLevel.SelectedIndex + 1).ToString(), textBoxUserSernum.Text);
                bool bExist = db.dbMrg.QueryExistDB(m_strConn, "Select * From t_UserInfo where [userName]='" + strIndex + "'");
                if (bExist)
                {
                    db.dbMrg.AlterDB(m_strConn, strQuery);
                    db.dbMrg.AddAlertInfo("01", "01", objUserInfo.getUserName(), DateTime.Now.ToString(), "01", "The user name is modified：" + tBUserName.Text);
                    utils.loghelp.log.Debug("The user name is modified：" + tBUserName.Text + ",time为:" + DateTime.Now.ToString());
                }
                else
                {
                    utils.loghelp.log.Debug("User does not exist！");
                    MessageBox.Show("User does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
        }

        private bool CheckAuthority()
        {
            bool bIsFlag = false;
            UserInfo objUserInfo = UserInfo.Instance;
            int nLevel = Convert.ToInt32(objUserInfo.getUserLevel());
            int ncbLevel = comboBoxLevel.SelectedIndex + 1;
            if ((nLevel >= ncbLevel) || (ncbLevel > 4))
            {
                bIsFlag = true;
            }
            return bIsFlag;
        }


        private void m_btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_Load(object sender, EventArgs e)
        {
            try
            {
                string strIndex = UserMrg.ms_strUserInfoIndex;
                if (!string.IsNullOrEmpty(strIndex))
                {
                    string mQuery = "Select [userName],[userLevel],[userSernum] From {0} where [userName]='" + strIndex + "'";
                    mQuery = string.Format(mQuery, "t_UserInfo");
                    mConnection = new OleDbConnection(m_strConn);
                    mConnection.Open();
                    OleDbCommand cmd = new OleDbCommand(mQuery, mConnection);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tBUserName.Text = reader[0].ToString();
                        tBPwd.Text = reader[1].ToString();
                        textBoxUserSernum.Text = reader[2].ToString();
                    }
                    reader.Close();
                    mConnection.Close();

                }
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
            }
        }
    }
}
