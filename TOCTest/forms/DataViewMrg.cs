﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ADOX;
using System.IO;
using System.Data.OleDb;
using System.Collections;
using TOCTest.comm;
using TOCTest.utils;

namespace TOCTest.forms
{
    public  struct OnlineResultEx
    {
        public string m_strInterval;
        public string m_strTOC;
        public string m_strIC;
        public string m_strTestNum;
        public string m_strTestDate;
        public string m_strDeviceName;
        public string m_strTestOperate;
        public string m_strChannelSum;
        public string m_strTestTimes;
    }

    public struct OfflineResultEx
    {
        public string m_strInterval;
        public string m_strCodeName;
        public string m_strCodeType;
        public string m_strSampleWay;
        public string m_strDeviceName;
        public string m_strTester;
        public string m_strTestTime;
        public string m_strTestTimes;
        public string m_strTestSumTimes;
        public string m_strStartBottle;
        public string m_strSamples;
        public string m_strTOC;
        public string m_strIC;
        public string m_strCON;
        public string m_strAveTOC;
        public string m_strAveIC;
        public string m_strAveCon;
    }

    public struct ConResultEx
    {
        public string m_strInterval;
        public string m_strCodeName;
        public string m_strCodeType;
        public string m_strSampleWay;
        public string m_strDeviceName;
        public string m_strTester;
        public string m_strTestTime;
        public string m_strTestTimes;
        public string m_strTestSumTimes;
        public string m_strStartBottle;
        public string m_strSamples;
        public string m_strRwTOC;  //pure waterTOC
        public string m_strRw;  //pure waterTOC
        public string m_strRs;  //sucroseTOC
        public string m_strRss; //BenzochinoneTOC
        public string m_strResponseRate; //响应率

    }



    public partial class DataViewMrg : Form
    {
        private DataSet ds = new DataSet();// data库操作
        public static OnlineResultEx ms_objOnlineResultEx = new OnlineResultEx();
        public static OfflineResultEx ms_objOfflineResultEx = new OfflineResultEx();
        public static ConResultEx ms_objConResultEx = new ConResultEx();
     
        public DataViewMrg()
        {
            InitializeComponent();
        }


        private void DataView_Load(object sender, EventArgs e)
        {
            if (TOCTest.forms.UserInfo.Instance.getUserLevel() != "1")
            {
                m_btnDel.Visible = false;
                m_btnRead.Visible = false;
                m_btnWrite.Visible = false;
                m_btnExport.Visible = false;
                m_btnImport.Visible = false;
            }
            string strTreeView = Main.ms_strDeviceName.ToString();
            string strQuery = "Select CodeType as type,Test_StartDt as dtTest,Test_OprationName as Name From {0}";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
            strQuery = string.Format(strQuery, strTreeView);
            OleDbConnection conn = new OleDbConnection(strConn);
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(strQuery, conn);
            try
            {
                dataAdapter.Fill(ds);
                ds.Tables[0].Clear();
                dataAdapter.Fill(ds);
                m_dataGridViewList.DataSource = ds.Tables[0].DefaultView;
                m_dataGridViewList.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
            finally
            {
                conn.Close();
            }
        }

        private void query()
        {
            string strTreeView = Main.ms_strDeviceName.ToString();
            string strQuery = "Select CodeType as type,Test_StartDt as dtTest,Test_OprationName as Name From {0}";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";

       
                string strTestMode = "";
                switch (m_cbTestMode.SelectedIndex)
                {
                    case 0:
                        strTestMode = "2";
                        break;
                    case 1:
                        strTestMode = "1";
                        break;
                    case 2:
                        strTestMode = "3";
                        break;
                    case 3:
                        strTestMode = "4";
                        break;
                    default:
                        break;

                }
                strQuery = "Select CodeType as type,Test_StartDt as dtTest,Test_OprationName as Name From {0} " + "where [CodeType] ='" + strTestMode + "'";
            
            DataTable mDataTable = new DataTable("ds2");

            strQuery = string.Format(strQuery, strTreeView);
            OleDbConnection conn = new OleDbConnection(strConn);
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(strQuery, conn);
            try
            {
                dataAdapter.Fill(ds);
                ds.Tables[0].Clear();
                dataAdapter.Fill(ds);
                m_dataGridViewList.DataSource = ds.Tables[0].DefaultView;
                m_dataGridViewList.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// querymode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            query();
        }

        private void m_dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (m_dataGridViewList.SelectedRows.Count > 0)
                {
                    string mTreeView = Main.ms_strDeviceName.ToString();
                    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
                    string strQuery = "Select * From {0} where [Test_StartDt]='{1}'";
                    strQuery = string.Format(strQuery, mTreeView, m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[1].Value.ToString());
                    OleDbConnection conn = new OleDbConnection(strConn);
                   
                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "2")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ms_objOnlineResultEx.m_strInterval = reader[6].ToString();
                            ms_objOnlineResultEx.m_strTOC = reader[19].ToString();
                            ms_objOnlineResultEx.m_strIC = reader[20].ToString();
                            ms_objOnlineResultEx.m_strTestNum = reader[5].ToString();
                            ms_objOnlineResultEx.m_strTestDate=reader[6].ToString();
                            ms_objOnlineResultEx.m_strDeviceName = Info.getDeviceName();
                            ms_objOnlineResultEx.m_strTestOperate = reader[7].ToString();
                            ms_objOnlineResultEx.m_strChannelSum=reader[8].ToString();
                            ms_objOnlineResultEx.m_strTestTimes = reader[15].ToString();
                        }
                        online objonline = new online();
                        objonline.Show();
                        conn.Close();
                    }
                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "1")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ms_objOfflineResultEx.m_strInterval = reader[6].ToString();
                            ms_objOfflineResultEx.m_strCodeName = reader[5].ToString();
                            ms_objOfflineResultEx.m_strCodeType = reader[4].ToString();
                            ms_objOfflineResultEx.m_strSampleWay = reader[17].ToString();
                            ms_objOfflineResultEx.m_strAveTOC = reader[11].ToString();
                            ms_objOfflineResultEx.m_strAveIC = reader[12].ToString();
                            ms_objOfflineResultEx.m_strTOC = reader[19].ToString();
                            ms_objOfflineResultEx.m_strIC = reader[20].ToString();
                        }
                        OfflineResult objOfflineResult = new OfflineResult();
                        objOfflineResult.Show();
                        conn.Close();
                    }

                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "3")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string strTOC = reader[19].ToString();
                            string strIC = reader[20].ToString();
                            string strCon = reader[21].ToString();
                            ms_objConResultEx.m_strRwTOC = strTOC.Split(',')[0].ToString();
                            ms_objConResultEx.m_strRw = strTOC.Split(',')[6].ToString();
                            ms_objConResultEx.m_strRs = strTOC.Split(',')[13].ToString();
                            ms_objConResultEx.m_strRss = strTOC.Split(',')[20].ToString();
                            ms_objConResultEx.m_strResponseRate = strIC.Split(',')[0].ToString();
                        
                        }
                        ViewConResult objViewConResult = new ViewConResult();
                        objViewConResult.Show();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
        }

        

        private void m_btnFresh_Click(object sender, EventArgs e)
        {
            query();
        }

        private void m_btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void m_btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                //read历史纪录
                TOCTest.comm.CommPort objCommPort = comm.CommPort.Instance;
                string strCommand = "FF" +Convert.ToInt64(TOCTest.db.dbMrg.QueryDeviceNum(Main.ms_strDeviceName)).ToString("X2") +"0x00"+"0x07" + "0x52"+"0x48" + "00";
                byte[] byCommand = TOCTest.utils.help.getCheckSum(strCommand);
                objCommPort.Send(byCommand);
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
            }
        }

        private void m_btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                TOCTest.comm.CommPort objCommPort = comm.CommPort.Instance;
                string Hisdatasum = m_dataGridViewList.Rows.Count.ToString("X4");
                int nSum = m_dataGridViewList.Rows.Count;
                int histNum = 0;

                for (int i = 0; i < nSum; i++)
                {
                    histNum++;
                    string Hisdatacurnum = histNum.ToString("X4");
                    string mTreeView = Main.ms_strDeviceName.ToString();
                    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
                    string strQuery = "Select * From {0} where [id]={1}";
                    strQuery = string.Format(strQuery, mTreeView,m_dataGridViewList.Rows[i].Cells[0].Value.ToString());
                    OleDbConnection conn = new OleDbConnection(strConn);
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string strInfo = reader[9].ToString();
                        Int64 nNum = Convert.ToInt64(strInfo);
                        Int64 nLength = 900 + 64;
                        string strCom = "FF" + "01" + nLength.ToString("X4") +"0x57"+"0x48" + Hisdatasum + histNum.ToString("X4");
                        string strTest_type = TOCTest.utils.help.converStringHex(reader[1].ToString(), 1);
                        strCom = strCom + strTest_type;
                        string strTest_Psernum = TOCTest.utils.help.converStringHex(reader[2].ToString(), 16);
                        strCom = strCom + strTest_Psernum;
                        string strTest_OprationName = TOCTest.utils.help.converStringHex(reader[3].ToString(), 16);
                        strCom = strCom + strTest_OprationName;
                        string strTest_StartDt = TOCTest.utils.help.getDBDateTime(reader[4].ToString());
                        strCom = strCom + strTest_StartDt;
                        string strBott_Amount = Convert.ToInt64(reader[5].ToString()).ToString("X2");
                        strCom = strCom + strBott_Amount;
                        string strStart_Bott = Convert.ToInt64(reader[6].ToString()).ToString("X2");
                        strCom = strCom + strStart_Bott;
                        string strDilu_Mult = Convert.ToInt64(reader[7].ToString()).ToString("X8");
                        strCom = strCom + strDilu_Mult;
                        string strTest_EndDt = TOCTest.utils.help.getDBDateTime(reader[8].ToString());
                        strCom = strCom + strTest_EndDt;

                        string Htest_testimes = nNum.ToString("X8");
                        strCom = strCom + Htest_testimes;
                        strCom = strCom + getData(reader[10].ToString(),nNum);
                        strCom = strCom + getData(reader[11].ToString(),nNum);
                        strCom = strCom + getData(reader[12].ToString(),nNum);
                        strCom = strCom + "00";
                        byte[] byDate = TOCTest.utils.help.getCheckSum(strCom);
                        objCommPort.Send(byDate);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
            }
        }

        private string getData(string strInfo, Int64 nNum)
        {
            string strValue = "";
            try
            {
                strInfo = strInfo.Substring(0, strInfo.Length - 2);
                string[] strTOCList = strInfo.Split(',');

                if (strTOCList.Length != nNum)
                {
                    string strHex = string.Format("X{0}", nNum * 2);
                    strValue = (0).ToString(strHex);
                    return strValue;
                }

                for (int i = 0; i < strTOCList.Length; i++)
                {
                    strValue = strValue + Convert.ToInt64(strTOCList[i]).ToString("X4");
                }
            }
            catch (Exception)
            {
                string strHex = string.Format("X{0}", 300);
                strValue = (0).ToString(strHex);
                return strValue;
            }
            string strTempHex = string.Format("X{0}", 300 - nNum * 2);
            strValue = strValue + (0).ToString(strTempHex);
            return strValue;
        }

        private void m_btnDel_Click(object sender, EventArgs e)
        {
            if (m_dataGridViewList.SelectedRows.Count > 0)
            {
                string strQuery = "delete * From {0}";
                strQuery = string.Format(strQuery, Main.ms_strDeviceName);
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbCommand da = new OleDbCommand(strQuery, conn);

                try
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    if ((MessageBox.Show("Do you confirm all records of clear?", "warning！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
                    {
                        ds.Tables[0].Clear();
                        da.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    TOCTest.utils.loghelp.log.Error(ex.Message, ex);
                    MessageBox.Show("Exception：" + ex.ToString(), "Tips");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        void dealOnline(string sLine)
        {
            ConfigHis m_objConfigHis = new ConfigHis();
            char[] parsChar = { ',' };
            m_objConfigHis.m_strCodeType = sLine.Split(parsChar)[1].ToString();
            m_objConfigHis.m_strCodeName = sLine.Split(parsChar)[2].ToString();
            m_objConfigHis.m_strStartTime = sLine.Split(parsChar)[3].ToString();
            m_objConfigHis.m_strLsampleNum = sLine.Split(parsChar)[4].ToString();
            m_objConfigHis.m_strOperator = sLine.Split(parsChar)[5].ToString();

            int nTemp = Convert.ToInt32(sLine.Split(parsChar)[4].ToString());
            for (int i = 0; i < nTemp * 3; i = i + 3)
            {
                m_objConfigHis.m_strTOCValue = m_objConfigHis.m_strTOCValue + sLine.Split(parsChar)[(6 + i)].ToString() + ",";
                m_objConfigHis.m_strTCValue = m_objConfigHis.m_strTCValue + sLine.Split(parsChar)[6 + i + 1].ToString() + ",";
                m_objConfigHis.m_strConduct = m_objConfigHis.m_strConduct + sLine.Split(parsChar)[6 + i + 2].ToString() + ",";
            }

            string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
            string strDBInfo = String.Format("insert into {0} ([CodeType],[CodeName],[Test_StartDt],[Test_EndDt],[Test_OprationName],[Channlesum],[OnlinestFlag],[AveTOCValue],[AveTCValue],[AveConduct],[LsampleNum],[Htest_testimes],[Start_Bott],[SampleWay],[TOCValue],[TCValue],[Conduct]) values ('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                TOCTest.Main.ms_strDeviceName, m_objConfigHis.m_strCodeType,
                m_objConfigHis.m_strCodeName, m_objConfigHis.m_strStartTime,
                m_objConfigHis.m_strEndTime, m_objConfigHis.m_strOperator,
                m_objConfigHis.m_strChannlesum, m_objConfigHis.m_stronlinestflag,
                m_objConfigHis.m_strAveTOCValue, m_objConfigHis.m_strAveTCValue,
                m_objConfigHis.m_strAveConduct, m_objConfigHis.m_strLsampleNum,
                m_objConfigHis.m_strNum, m_objConfigHis.m_strStartBott,
                m_objConfigHis.m_strSampleWay, m_objConfigHis.m_strTOCValue,
                m_objConfigHis.m_strTCValue, m_objConfigHis.m_strConduct);
            TOCTest.db.dbMrg.AlterDB(strConnection, strDBInfo);
            Console.WriteLine(m_objConfigHis);
        }

        void dealOffline(string sLine)
        {
            ConfigHis m_objConfigHis = new ConfigHis();
            char[] parsChar = { ',' };
            m_objConfigHis.m_strCodeType = sLine.Split(parsChar)[1].ToString();
            m_objConfigHis.m_strCodeName = sLine.Split(parsChar)[2].ToString();
            m_objConfigHis.m_strStartTime = sLine.Split(parsChar)[3].ToString();
            m_objConfigHis.m_strLsampleNum = sLine.Split(parsChar)[4].ToString();
            m_objConfigHis.m_strNum = sLine.Split(parsChar)[5].ToString();
            m_objConfigHis.m_strStartBott = sLine.Split(parsChar)[6].ToString();
            m_objConfigHis.m_strSampleWay = sLine.Split(parsChar)[7].ToString();
            m_objConfigHis.m_strSum_Num = sLine.Split(parsChar)[8].ToString();
            m_objConfigHis.m_strOperator = sLine.Split(parsChar)[9].ToString();

            int nTemp = Convert.ToInt32(sLine.Split(parsChar)[4].ToString()) ;
            for (int i = 0; i < nTemp; i++)
            {

                m_objConfigHis.m_strTOCValue = m_objConfigHis.m_strTOCValue + sLine.Split(parsChar)[(10 + i * 21 + 0)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 3)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 6)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 9)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 12)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 15)].ToString() + ",";
                m_objConfigHis.m_strTCValue = m_objConfigHis.m_strTCValue + sLine.Split(parsChar)[(10 + i * 21 + 1)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 4)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 7)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 10)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 13)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 16)].ToString() + ",";
                m_objConfigHis.m_strConduct = m_objConfigHis.m_strConduct + sLine.Split(parsChar)[(10 + i * 21 + 2)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 5)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 8)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 11)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 14)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 17)].ToString() + ",";
                m_objConfigHis.m_strAveTOCValue = m_objConfigHis.m_strAveTOCValue + sLine.Split(parsChar)[28 + i * 21 + 0].ToString() + ",";
                m_objConfigHis.m_strAveTCValue = m_objConfigHis.m_strAveTCValue + sLine.Split(parsChar)[28 + i * 21 + 1].ToString() + ",";
                m_objConfigHis.m_strAveConduct = m_objConfigHis.m_strAveConduct + sLine.Split(parsChar)[28 + i * 21 + 2].ToString() + ",";
            }
           
           
        

            string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
            string strDBInfo = String.Format("insert into {0} ([CodeType],[CodeName],[Test_StartDt],[Test_EndDt],[Test_OprationName],[Channlesum],[OnlinestFlag],[AveTOCValue],[AveTCValue],[AveConduct],[LsampleNum],[Htest_testimes],[Start_Bott],[SampleWay],[TOCValue],[TCValue],[Conduct],[Htest_sum]) values ('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                TOCTest.Main.ms_strDeviceName, m_objConfigHis.m_strCodeType,
                m_objConfigHis.m_strCodeName, m_objConfigHis.m_strStartTime,
                m_objConfigHis.m_strEndTime, m_objConfigHis.m_strOperator,
                m_objConfigHis.m_strChannlesum, m_objConfigHis.m_stronlinestflag,
                m_objConfigHis.m_strAveTOCValue, m_objConfigHis.m_strAveTCValue,
                m_objConfigHis.m_strAveConduct, m_objConfigHis.m_strLsampleNum,
                m_objConfigHis.m_strNum, m_objConfigHis.m_strStartBott,
                m_objConfigHis.m_strSampleWay, m_objConfigHis.m_strTOCValue,
                m_objConfigHis.m_strTCValue, m_objConfigHis.m_strConduct,m_objConfigHis.m_strSum_Num);
            TOCTest.db.dbMrg.AlterDB(strConnection, strDBInfo);
            Console.WriteLine(m_objConfigHis);
        }

        void dealConduct(string sLine)
        {
            ConfigHis m_objConfigHis = new ConfigHis();
            char[] parsChar = { ',' };
            m_objConfigHis.m_strCodeType = sLine.Split(parsChar)[1].ToString();
            m_objConfigHis.m_strCodeName = sLine.Split(parsChar)[2].ToString();
            m_objConfigHis.m_strStartTime = sLine.Split(parsChar)[3].ToString();
            m_objConfigHis.m_strLsampleNum = sLine.Split(parsChar)[4].ToString();
            m_objConfigHis.m_strNum = sLine.Split(parsChar)[5].ToString();
            m_objConfigHis.m_strStartBott = sLine.Split(parsChar)[6].ToString();
            m_objConfigHis.m_strSampleWay = sLine.Split(parsChar)[7].ToString();
            m_objConfigHis.m_strSum_Num = sLine.Split(parsChar)[8].ToString();
            m_objConfigHis.m_strOperator = sLine.Split(parsChar)[9].ToString();

            int nTemp = Convert.ToInt32(sLine.Split(parsChar)[4].ToString());
            for (int i = 0; i < nTemp; i++)
            {
                m_objConfigHis.m_strTOCValue = m_objConfigHis.m_strTOCValue + sLine.Split(parsChar)[(10 + i * 21 + 0)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 3)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 6)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 9)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 12)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 15)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 18)].ToString() + ",";
                m_objConfigHis.m_strTCValue = m_objConfigHis.m_strTCValue + sLine.Split(parsChar)[(10 + i * 21 + 1)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 4)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 7)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 10)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 13)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 16)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 19)].ToString() + ",";
                m_objConfigHis.m_strConduct = m_objConfigHis.m_strConduct + sLine.Split(parsChar)[(10 + i * 21 + 2)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 5)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 8)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 11)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 14)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 17)].ToString() + "," + sLine.Split(parsChar)[(10 + i * 21 + 20)].ToString() + ",";
            }
            m_objConfigHis.m_strAveTOCValue = sLine.Split(parsChar)[10 + 18].ToString();
            m_objConfigHis.m_strAveTCValue = sLine.Split(parsChar)[10 +7+ 18].ToString();
            m_objConfigHis.m_strAveConduct = sLine.Split(parsChar)[10 +7*2+ 18].ToString();
            m_objConfigHis.m_strBott_Amount = sLine.Split(parsChar)[11].ToString();

            string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
            string strDBInfo = String.Format("insert into {0} ([CodeType],[CodeName],[Test_StartDt],[Test_EndDt],[Test_OprationName],[Channlesum],[OnlinestFlag],[AveTOCValue],[AveTCValue],[AveConduct],[Bott_Amount],[LsampleNum],[Htest_testimes],[Start_Bott],[SampleWay],[TOCValue],[TCValue],[Conduct],[Htest_sum]) values ('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",
                TOCTest.Main.ms_strDeviceName, m_objConfigHis.m_strCodeType,
                m_objConfigHis.m_strCodeName, m_objConfigHis.m_strStartTime,
                m_objConfigHis.m_strEndTime, m_objConfigHis.m_strOperator,
                m_objConfigHis.m_strChannlesum, m_objConfigHis.m_stronlinestflag,
                m_objConfigHis.m_strAveTOCValue, m_objConfigHis.m_strAveTCValue,
                m_objConfigHis.m_strAveConduct,m_objConfigHis.m_strBott_Amount,
                m_objConfigHis.m_strLsampleNum,
                m_objConfigHis.m_strNum, m_objConfigHis.m_strStartBott,
                m_objConfigHis.m_strSampleWay, m_objConfigHis.m_strTOCValue,
                m_objConfigHis.m_strTCValue, m_objConfigHis.m_strConduct,m_objConfigHis.m_strSum_Num);
            TOCTest.db.dbMrg.AlterDB(strConnection, strDBInfo);
            Console.WriteLine(m_objConfigHis);
        }


        private void m_btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo objUserInfo = UserInfo.Instance;
                db.dbMrg.AddAlertInfo("01", "01", objUserInfo.getUserName(), DateTime.Now.ToString(), "01", "export data!");
                TOCTest.data.ExportDataView objExport = new TOCTest.data.ExportDataView();
                objExport.ShowDialog();
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
        }

        private void m_btnImport_Click(object sender, EventArgs e)
        {
            openFileDialogtxt.InitialDirectory = @"../../";
            openFileDialogtxt.Filter = "file|*.*";
            openFileDialogtxt.FilterIndex = 2;
            openFileDialogtxt.RestoreDirectory = false;
            openFileDialogtxt.Title = "open";
            openFileDialogtxt.FileName = "";
            openFileDialogtxt.Multiselect = true;

            if (openFileDialogtxt.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialogtxt.FileName.ToString();
                StreamReader objReader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("GB2312"));
                char[] parsChar = { ',' };
                string sLine = "";
                objReader.ReadLine();
                string firstLine = objReader.ReadLine();
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null && !sLine.Equals(""))
                    {
                        try
                        {
                            string Test_type = sLine.Split(parsChar)[1].ToString();
                            switch (Test_type)
                            {
                                case "1":
                                    dealOffline(sLine);
                                    break;
                                case "2":
                                    dealOnline(sLine);
                                    break;
                                case "3":
                                    dealConduct(sLine);
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            utils.loghelp.log.Error(ex.Message, ex);
                        }
                    }

                }

                MessageBox.Show("Increase success！", "Tips");
                objReader.Close();
                query();
            }
        }

        private void m_btnDelS_Click(object sender, EventArgs e)
        {
            if (m_dataGridViewList.SelectedRows.Count > 0)
            {
                string strQuery = "delete * From {0} where [Test_StartDt]='{1}'";
                strQuery = string.Format(strQuery, Main.ms_strDeviceName, m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[1].Value.ToString());
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbCommand da = new OleDbCommand(strQuery, conn);

                try
                {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    if ((MessageBox.Show("Are you sure you want the delete record?", "warning！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
                    {
                        da.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    TOCTest.utils.loghelp.log.Error(ex.Message, ex);
                    MessageBox.Show("Exception：" + ex.ToString(), "Tips");
                }
                finally
                {
                    conn.Close();
                }
                query();
            }
        }

        private void m_btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dataGridViewList.SelectedRows.Count > 0)
                {
                    string mTreeView = Main.ms_strDeviceName.ToString();
                    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;  Data Source=..\\..\\mdb\\HistoryData.mdb";
                    string strQuery = "Select * From {0} where [Test_StartDt]='{1}'";
                    strQuery = string.Format(strQuery, mTreeView, m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[1].Value.ToString());
                    OleDbConnection conn = new OleDbConnection(strConn);

                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "2")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ms_objOnlineResultEx.m_strInterval = reader[6].ToString();
                            ms_objOnlineResultEx.m_strTOC = reader[19].ToString();
                            ms_objOnlineResultEx.m_strIC = reader[20].ToString();
                            ms_objOnlineResultEx.m_strTestNum = reader[5].ToString();
                            ms_objOnlineResultEx.m_strTestDate = reader[6].ToString();
                            ms_objOnlineResultEx.m_strDeviceName = Info.getDeviceName();
                            ms_objOnlineResultEx.m_strTestOperate = reader[8].ToString();
                            ms_objOnlineResultEx.m_strChannelSum = reader[9].ToString();
                            ms_objOnlineResultEx.m_strTestTimes = reader[15].ToString();
                        }
                        ViewOnlineReport objonline = new ViewOnlineReport();
                        objonline.Show();
                        conn.Close();
                    }
                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "1")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ms_objOfflineResultEx.m_strInterval = reader[6].ToString();
                            ms_objOfflineResultEx.m_strCodeName = reader[5].ToString();
                            ms_objOfflineResultEx.m_strCodeType = reader[4].ToString();
                            if (reader[17].ToString() == "1")
                            {
                                ms_objOfflineResultEx.m_strSampleWay = "auto";
                            }
                            else
                            {
                                ms_objOfflineResultEx.m_strSampleWay = "manual";
                            }
                            ms_objOfflineResultEx.m_strDeviceName = Info.getDeviceName();
                            ms_objOfflineResultEx.m_strSamples = reader[14].ToString();
                            ms_objOfflineResultEx.m_strTester = reader[8].ToString();
                            ms_objOfflineResultEx.m_strTestTimes = reader[15].ToString();
                            ms_objOfflineResultEx.m_strTestSumTimes = reader[18].ToString();
                            ms_objOfflineResultEx.m_strStartBottle = reader[16].ToString();
                            ms_objOfflineResultEx.m_strAveTOC = reader[11].ToString();
                            ms_objOfflineResultEx.m_strAveIC = reader[12].ToString();
                            ms_objOfflineResultEx.m_strAveCon = reader[13].ToString();
                            ms_objOfflineResultEx.m_strTOC = reader[19].ToString();
                            ms_objOfflineResultEx.m_strIC = reader[20].ToString();
                            ms_objOfflineResultEx.m_strCON = reader[7].ToString();
                        }
                        ViewOfflineReport objonline = new ViewOfflineReport();
                        objonline.Show();
                     
                        conn.Close();
                    }

                    if (m_dataGridViewList.Rows[m_dataGridViewList.CurrentRow.Index].Cells[0].Value.ToString() == "3")
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ms_objConResultEx.m_strInterval = reader[6].ToString();
                            ms_objConResultEx.m_strCodeName = reader[5].ToString();
                            ms_objConResultEx.m_strCodeType = reader[4].ToString();
                            if (reader[17].ToString() == "1")
                            {
                                ms_objConResultEx.m_strSampleWay = "auto";
                            }
                            else
                            {
                                ms_objConResultEx.m_strSampleWay = "manual";
                            }
                            ms_objConResultEx.m_strDeviceName = Info.getDeviceName();
                            ms_objConResultEx.m_strSamples = reader[14].ToString();
                            ms_objConResultEx.m_strTester = reader[8].ToString();
                            ms_objConResultEx.m_strTestTimes = reader[15].ToString();
                            ms_objConResultEx.m_strTestSumTimes = reader[18].ToString();

                            ms_objConResultEx.m_strStartBottle = reader[16].ToString();
                            string strTOC = reader[19].ToString();
                            string strIC = reader[20].ToString();
                            string strCon = reader[7].ToString();
                            ms_objConResultEx.m_strRwTOC = strTOC.Split(',')[0].ToString();
                            ms_objConResultEx.m_strRw = strTOC.Split(',')[6].ToString();
                            ms_objConResultEx.m_strRs = strTOC.Split(',')[13].ToString();
                            ms_objConResultEx.m_strRss = strTOC.Split(',')[20].ToString();
                            ms_objConResultEx.m_strResponseRate = strIC.Split(',')[0].ToString();
                        }
                        ViewConReport objViewConReport = new ViewConReport();
                        objViewConReport.Show();
                     
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
                MessageBox.Show("Exception：" + ex.ToString(), "Tips");
            }
        }

        private void m_dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < m_dataGridViewList.Rows.Count; i++)
            {
                m_dataGridViewList.Rows[i].HeaderCell.Value = (i).ToString();
            }
          
        }
    }
}
