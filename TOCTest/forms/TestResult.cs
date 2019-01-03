using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOCTest.forms
{
    public partial class TestResult : Form
    {
        public TestResult()
        {
            InitializeComponent();
        }

        // delegate used for Invoke
        internal delegate void ResultDelegate(TOCTest.comm.TestResult data);
        /// <summary>
        /// Update the connection status
        /// </summary>
        public void OnStatusChanged(TOCTest.comm.TestResult data)
        {
            try
            {
                //Handle multi-threading
                if (InvokeRequired)
                {
                    Invoke(new ResultDelegate(OnStatusChanged), new object[] { data });
                    return;
                }
                m_tbTestType.Text =  data.m_strTestType;         //test模态 (Z/L/S/C/)
                m_tbTestPsernum.Text = data.m_strTestPsernum;      //test序号
                m_tbTestOprationName.Text = data.m_strTestOprationName; //operator
                m_tbTestStartDt.Text = data.m_strTestStartDt;      //开始日期/time
                m_tbBottAmount.Text = data.m_strBottAmount;       //bottle amount 
                m_tbStartBott.Text = data.m_strStartBott;        //start bottle No.
                m_tbDiluMult.Text = data.m_strDiluMult;         //dilution multiple
                m_tbTestEndDt.Text = data.m_strTestEndDt;        //结束日期/time
                m_tbHTestTestimes.Text = data.m_strTestEndDt;    // curve的采样次数
                m_tbTOCValue.Text = data.m_strTOCValue;         //TOC值
                m_tbTCValue.Text = data.m_strTCValue;          //TC值
                m_tbConduct.Text = data.m_strConduct;          //电导率   
            }
            catch (Exception ex)
            {
                utils.loghelp.log.Error(ex.Message, ex);
            }
        }

    }
}
