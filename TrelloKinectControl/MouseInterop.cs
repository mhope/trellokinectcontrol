﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TrelloKinectControl.Mouse
{
    [Flags]
    internal enum MouseEventDataXButtons : uint
    {
        Nothing = 0x00000000,
        XBUTTON1 = 0x00000001,
        XBUTTON2 = 0x00000002
    }

    [Flags]
    internal enum MOUSEEVENTF : uint
    {
        ABSOLUTE = 0x8000,
        HWHEEL = 0x02000,
        MOVE = 0x0001,
        MOVE_NOCOALESCE = 0x2000,
        LEFTDOWN = 0x0002,
        LEFTUP = 0x0004,
        RIGHTDOWN = 0x0008,
        RIGHTUP = 0x0020,
        MIDDLEDOWN = 0x0020,
        MIDDLEUP = 0x0040,
        VIRTUALDESK = 0x4000,
        WHEEL = 0x0800,
        XDOWN = 0x0080,
        XUP = 0x0200
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        internal int dx;
        internal int dy;
        internal MouseEventDataXButtons mouseData;
        internal MOUSEEVENTF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct INPUT
    {
        [FieldOffset(0)]
        internal int type;
        [FieldOffset(4)]
        internal MOUSEINPUT mi;

        public static int Size
        {
            get { return Marshal.SizeOf(typeof(INPUT)); }
        }
    }

    public static class MouseInterop
    {
        [DllImport("user32.dll")]
        private static extern uint SendInput(int nInputs, INPUT[] inputs, int size);

        static bool leftClickDown;

        public static void ControlMouse(int dx, int dy, bool leftClick)
        {

            INPUT[] inputs = new INPUT[2];

            inputs[0] = new INPUT();
            inputs[0].type = 0;
            inputs[0].mi.dx = dx;
            inputs[0].mi.dy = dy;
            inputs[0].mi.dwFlags = MOUSEEVENTF.MOVE;

            if (!leftClickDown && leftClick)
            {
                inputs[1] = new INPUT();
                inputs[1].type = 0;
                inputs[1].mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
                leftClickDown = true;
            }
            else if (leftClickDown && !leftClick)
            {
                inputs[1] = new INPUT();
                inputs[1].type = 0;
                inputs[1].mi.dwFlags = MOUSEEVENTF.LEFTUP;
                leftClickDown = false;
            }

            SendInput(inputs.Length, inputs, INPUT.Size);
        }

        public static void DragMouse(int dx, int dy)
        {
            LeftDown();
            System.Threading.Thread.Sleep(20);
            Move(0, 3);
            System.Threading.Thread.Sleep(20);
            Move(0, 3);
            System.Threading.Thread.Sleep(20);
            Move(0, 3);
            System.Threading.Thread.Sleep(20);
            Move(0, 3);
            System.Threading.Thread.Sleep(1000);

            Move(0,10);
            System.Threading.Thread.Sleep(20);
            Move(0,10);
            System.Threading.Thread.Sleep(20);
            Move(0,10);
            System.Threading.Thread.Sleep(20);
            Move(0,10);
            System.Threading.Thread.Sleep(20);
            Move(0, 20);
            System.Threading.Thread.Sleep(1000);

            LeftUp();
        }

        public static void Move(int x, int y)
        {

            var dx = x * (65536 / System.Windows.SystemParameters.PrimaryScreenWidth);
            var dy = y * (65536 / System.Windows.SystemParameters.PrimaryScreenHeight);

            INPUT[] inputs = new INPUT[1];

            inputs[0] = new INPUT();
            inputs[0].type = 0;
            inputs[0].mi.dx = x;
            inputs[0].mi.dy = y;
            inputs[0].mi.dwFlags = MOUSEEVENTF.MOVE;

            SendInput(inputs.Length, inputs, INPUT.Size);
        }

        public static void LeftDown()
        {    
            INPUT[] inputs = new INPUT[1];
            inputs[0] = new INPUT();
            inputs[0].type = 0;
            inputs[0].mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
            SendInput(inputs.Length, inputs, INPUT.Size);
        }

        public static void LeftUp()
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0] = new INPUT();
            inputs[0].type = 0;
            inputs[0].mi.dwFlags = MOUSEEVENTF.LEFTUP;
            SendInput(inputs.Length, inputs, INPUT.Size);
        }

        public static void Jiggle()
        {
            int loopCount = 5;
            for (int i = 0; i < loopCount; i++)
            {
                System.Threading.Thread.Sleep(10);
                Move(0, 3);
            }
            for (int j = loopCount; j > 0; j--)
            {
                System.Threading.Thread.Sleep(10);
                Move(0, -3);
            }
        }
    }
}
