using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MoveMyPointer.Mouse
{
    public class MouseHelper
    {
        #region API

        //mouse event function
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //mouse move function
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern int SetCursorPos(int x, int y);

        //keyboard event function
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, KeyEventFlag dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        #endregion

        #region Event

        public static Point GetCursorPosition()
        {
            Point cursorPos;
            GetCursorPos(out cursorPos);

            return cursorPos;
        }

        public static void MoveMouse(Point p)
        {
            SetCursorPos(p.X, p.Y);
        }

        public static void FireMouseEvent(MouseButtons button, EventType type = EventType.Click, bool doubleClick = false)
        {
            MouseEventFlag flagUp = new MouseEventFlag();
            MouseEventFlag flagDown = new MouseEventFlag();

            switch (button)
            {
                case MouseButtons.Left:
                    flagUp = MouseEventFlag.LeftUp;
                    flagDown = MouseEventFlag.LeftDown;
                    break;

                case MouseButtons.Middle:
                    flagUp = MouseEventFlag.MiddleUp;
                    flagDown = MouseEventFlag.MiddleDown;
                    break;

                case MouseButtons.Right:
                    flagUp = MouseEventFlag.RightUp;
                    flagDown = MouseEventFlag.RightDown;
                    break;

                default://defaul left button
                    flagUp = MouseEventFlag.LeftUp;
                    flagDown = MouseEventFlag.LeftDown;
                    break;
            }

            if (type == EventType.Click)
            {
                int count = doubleClick == false ? 1 : 2;//check
                for (int i = 0; i < count; i++)
                {
                    mouse_event(flagUp, 0, 0, 0, 0);
                    mouse_event(flagDown, 0, 0, 0, 0);
                }
            }
            else
            {
                if (type == EventType.Down)
                    mouse_event(flagDown, 0, 0, 0, 0);

                else if (type == EventType.Up)
                    mouse_event(flagUp, 0, 0, 0, 0);
            }
        }

        public static void FireKeyboardEvent(byte key, KeyEventFlag _dwFlags)
        {
            keybd_event(key, 0, _dwFlags, 0);
        }

        //keyboard action combo keys
        public void KeyboardComm(byte[] combo)
        {
            if (combo.Length >= 2)
            {
                //all keys down
                foreach (byte __bVk in combo)
                {
                    FireKeyboardEvent(__bVk, KeyEventFlag.Down);
                }

                //Reverse keys
                //combo = (byte[])combo.Reverse().ToArray();
                Array.Reverse(combo);
                
               //all keys up
                foreach (byte key in combo)
                {
                    FireKeyboardEvent(key, KeyEventFlag.Up);
                }
            }
        }

        //get keycode,return key to byte
        public static byte GetKeys(Keys key)
        {
            return (byte)key;
        }
        #endregion
    }
}
