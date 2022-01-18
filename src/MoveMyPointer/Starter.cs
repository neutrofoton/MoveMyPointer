using MoveMyPointer.Screen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MoveMyPointer.Controls.Extensions;
using MoveMyPointer.Mouse;

namespace MoveMyPointer
{
    public partial class Starter : Form
    {
        string msgScreenSize;
        string msgCursorPosition;
        string msgTime;
        string msgDEBUG;
        public Starter()
        {
            InitializeComponent();

            ScreenSize ssz = ScreenHelper.GetScreenSize();
            msgScreenSize = String.Format("Screen size = {0} x {1}", ssz.Length, ssz.Width);

            lblMessage.Text = msgScreenSize;

            this.ShowInTaskbar = false;
            this.MaximizeBox = false;

            StartThread();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        private void StartThread()
        {
            Random random = new Random();

            Thread th = new Thread(new ThreadStart(() =>
            {
                Point lastPoint = MouseHelper.GetCursorPosition();
                DateTime lastTime = DateTime.Now;

                while (!Program.IsApplicationExit)
                {
                    msgTime = DateTime.Now.ToLongTimeString();

                    Point point = MouseHelper.GetCursorPosition();
                    msgCursorPosition = string.Format("Position [X={0} , Y={1}]", point.X, point.Y);

                    msgDEBUG = new StringBuilder()
                        .AppendLine("DEBUG: ")
                        .AppendFormat("lastPoint [{0} $ {1}]", lastPoint.X, lastPoint.Y).AppendLine()
                        .AppendFormat("newPoint  [{0} $ {1}]", point.X, point.Y)
                        .ToString();

                    if (point.X == lastPoint.X && point.Y == lastPoint.Y)
                    {
                        TimeSpan ts = DateTime.Now - lastTime;

                        if (ts.TotalSeconds > 5)
                        {
                            MouseHelper.MoveMouse(new Point(lastPoint.X + random.Next(-9, 9), lastPoint.Y + random.Next(-5, 5)));

                            lastPoint = MouseHelper.GetCursorPosition();
                            lastTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        lastPoint = point;
                    }

                    this.InvokeEx(x =>
                    {
                        this.lblMessage.Text = new StringBuilder()
                            .AppendLine(msgTime)
                            .AppendLine(msgScreenSize)
                            .AppendLine(msgCursorPosition)
                            .AppendLine()
                            .AppendLine(msgDEBUG)
                            .ToString();
                    });

                    Thread.Sleep(1000);
                }

            }));

            th.Start();
        }

        private void Starter_Resize(object sender, EventArgs e)
        {
            if(WindowState== FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        
    }
}
