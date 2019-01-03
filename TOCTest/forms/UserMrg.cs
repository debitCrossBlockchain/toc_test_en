using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using TOCTest.utils;
using TOCTest.forms;
using TOCTest.db;

namespace TOCTest.forms
{
    public partial class UserMrg : Form
    {
        private OleDbConnection mConnection;
        string m_strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\UserInfo.mdb";
        private DataSet ds = new DataSet();// data库操作
        public static string ms_strUserInfoIndex;
        public UserMrg()
        {
            InitializeComponent();
        }

        private void buttonHistoricalAdd_Click(object sender, EventArgs e)
        {
            ms_strUserInfoIndex = dataGridViewUserInfo.Rows[dataGridViewUserInfo.CurrentRow.Index].Cells[0].Value.ToString();
            User objUser = new User();
            objUser.ShowDialog();
            objUser.Insert();
        }

        private void buttonHistoricalEdit_Click(object sender, EventArgs e)
        {
            ms_strUserInfoIndex = dataGridViewUserInfo.Rows[dataGridViewUserInfo.CurrentRow.Index].Cells[0].Value.ToString();
            User objUser = new User();
            objUser.ShowDialog();
            objUser.Alert(ms_strUserInfoIndex);  
        }

        private void buttonHistoricalDelete_Click(object sender, EventArgs e)
        {
            if (CheckAuthority())
            {
                MessageBox.Show("There is a problem with user permissions. Please check user permissions carefully！");
                return;
            }

            if (dataGridViewUserInfo.SelectedRows.Count <= 0)
            {
                return;
            }
            string mDeleteUserInfoIndex = dataGridViewUserInfo.Rows[dataGridViewUserInfo.CurrentRow.Index].Cells[0].Value.ToString();
            string mQuery = "delete * From {0} where [userName]='" + mDeleteUserInfoIndex + "'";
            mQuery = string.Format(mQuery, "t_UserInfo");
            mConnection = new OleDbConnection(m_strConn);
            OleDbCommand da = new OleDbCommand(mQuery, mConnection);
            try
            {
                mConnection.Open();
                if ((MessageBox.Show("Do you confirmdelete user now?", "warning！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
                {
                    //delete data
                    //首先delete data集 中的该条记录
                    int i = dataGridViewUserInfo.CurrentCell.RowIndex;//得到当前记录号
                    if (i >= 0)
                    {
                        ds.Tables[0].Rows[i].Delete();
                    }
                }
                da.ExecuteNonQuery();
                db.dbMrg.AddAlertInfo("01", "01", mDeleteUserInfoIndex, DateTime.Now.ToString(), "01", "user managementdelete user name is：" + mDeleteUserInfoIndex);
                utils.loghelp.log.Debug("delete user name is：" + mDeleteUserInfoIndex + ",time is:" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
            finally
            {
                mConnection.Close();
            }
        }

        private void buttonSearchALLHis_Click(object sender, EventArgs e)
        {
            Query();
        }

        private bool CheckAuthority()
        {
            bool bIsFlag = false;
            UserInfo objUserInfo = UserInfo.Instance;
            int nLevel = Convert.ToInt32(objUserInfo.getUserLevel());
            int nDBLevel = Convert.ToInt32(dataGridViewUserInfo.Rows[dataGridViewUserInfo.CurrentRow.Index].Cells[1].Value.ToString());
            if (nLevel > nDBLevel)
            {
                bIsFlag = true;
            }
            return bIsFlag;
        }

        private void UserMrg_Load(object sender, EventArgs e)
        {
            UserInfo objUserInfo = UserInfo.Instance;
            int nLevel = Convert.ToInt32(objUserInfo.getUserLevel());
            if (nLevel >= 4)
            {
                this.Enabled = false;
                MessageBox.Show("User insufficient authority!");
                return;
            }
            Query();
        }

        private void Query()
        {
            string mQuery = "Select [userName] as userName,[userLevel] as userlevel,[userSernum] as userNum From {0}";
            mQuery = string.Format(mQuery, "t_UserInfo");
            mConnection = new OleDbConnection(m_strConn);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(mQuery, mConnection);
            //如果需要再次query，需cleardataset里面的 data  
            try
            {
                if (mConnection.State != ConnectionState.Open) { mConnection.Open(); }
                dataAdapter.Fill(ds);
                ds.Tables[0].Clear();
                dataAdapter.Fill(ds);
                dataGridViewUserInfo.DataSource = ds.Tables[0].DefaultView;
                dataGridViewUserInfo.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
                utils.loghelp.log.Error(ex.Message, ex);
            }
            finally
            {
                mConnection.Close();
            }
        }

        private void dataGridViewUserInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ms_strUserInfoIndex = dataGridViewUserInfo.Rows[dataGridViewUserInfo.CurrentRow.Index].Cells[0].Value.ToString();
            User objUser = new User();
            objUser.ShowDialog();
            objUser.Alert(ms_strUserInfoIndex);  
        }
    }
}
