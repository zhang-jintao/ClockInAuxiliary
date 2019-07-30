using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockInAuxiliary
{


    

    public partial class fmMain : Form
    {

        #region 常量和构造
        private string[] yesArray = {"确定打卡了吗？","真的打卡了吗？","肯定打卡了吗？"};

        private string[] noArray = { "你要上天啊？", "没打卡还在这坐着呢？", "快去打卡！","打完卡再回来！！！" };

        private string[] closeArray = { "要么打卡，要么关机！","没有第三条路！","快去打卡！" };

        public fmMain()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Opacity = 0;
        }
        #endregion

        #region 倒计时
        private bool countDownStart = false;
        private int countDownNumber = 180;
        private void tmCountDown_Tick(object sender, EventArgs e)
        {
            if (countDownStart)
            {

                this.lbCountDown.Text = "将在" + this.countDownNumber + "s后关机...";
                if (this.countDownNumber == 180)
                {
                    CMDManager.shutDown(180);
                }
                this.countDownNumber--;
                if (this.countDownNumber == 0)
                {
                    this.countDownNumber = 180;
                    this.tmCountDown.Enabled = false;
                    this.Hide();
                }
            }
            else
            {
                if (DateTime.Now.ToShortTimeString().ToString().Equals("08:45")|| DateTime.Now.ToShortTimeString().ToString().Equals("18:00"))
                {
                    this.countDownStart = true;
                    this.Show();
                }
            }
            
        }
        #endregion
        
        #region 是 点击事件
        private int yesIndex = 0;

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (this.yesIndex == this.yesArray.Length)
            {
                CMDManager.repealShutDown();
                System.Environment.Exit(0);
            }
            MessageBox.Show(this.yesArray[this.yesIndex], "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.yesIndex++;
        }
        #endregion

        #region 否 点击事件
        private void btnNo_Click(object sender, EventArgs e)
        {
            for (int i=0;i< this.noArray.Length;i++)
            {

                MessageBox.Show(this.noArray[i], "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                if (i == this.noArray.Length - 1)
                {
                    i = 0;
                }
            }
        }
        #endregion

        #region 关闭窗口事件
        private int closeIndex = 0;

        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (this.closeIndex == this.closeArray.Length)
            {
                this.closeIndex = 0;
            }
            MessageBox.Show(this.closeArray[this.closeIndex], "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.closeIndex++;
        }
        #endregion

        #region 第一次展示窗口事件
        private void fmMain_Shown(object sender, EventArgs e)
        {
            this.Hide();

            this.ShowInTaskbar = true;
            this.Opacity = 1;
        }
        #endregion
    }
}
