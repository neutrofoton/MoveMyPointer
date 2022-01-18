using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MoveMyPointer.Screen
{
    public class ScreenHelper
    {
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);

        public static ScreenSize GetScreenSize()
        {
            return new ScreenSize(GetSystemMetrics(0), GetSystemMetrics(1));
        }
    }
}
