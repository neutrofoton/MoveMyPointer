using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoveMyPointer.Controls.Extensions;

namespace MoveMyPointer
{
    internal static class Program
    {
        static Starter starter;
        static bool isApplicationExit = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            isApplicationExit = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Starter());

            starter = new Starter();
            DefaultSize = starter.Size;

            using (NotifyIcon icon = new NotifyIcon())
            {
                icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

                icon.ContextMenuStrip = new ContextMenuStrip();
                icon.ContextMenuStrip.Items.Add("Show", null, (s, e) => { starter.Show(); starter.ShowWindowAtTop(); });
                icon.ContextMenuStrip.Items.Add("Hide", null, (s, e) => { starter.Hide(); });
                icon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => { isApplicationExit = true; Application.Exit(); });

                icon.Visible = true;

                Application.Run();
                icon.Visible = false;
            }
        }

        public static bool IsApplicationExit
        {
            get { return isApplicationExit; }
        }

        public static Size DefaultSize
        {
            get;
            private set;
        }
    }
}
