using System;
using System.Collections.Generic;
using System.Text;

namespace MoveMyPointer.Screen
{
   
    public struct ScreenSize
    {
        public ScreenSize(int l, int w)
        {
            Length = l;
            Width = w;
        }

        public int Length { get; set; }
        public int Width { get; set; }
    }
}
