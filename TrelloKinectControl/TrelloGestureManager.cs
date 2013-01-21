using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using TrelloKinectControl.Keyboard;
using TrelloKinectControl.Mouse;

namespace TrelloKinectControl.Gestures
{
    class TrelloGestureManager
    {
        private static readonly int INITIAL_X = 200;
        private static readonly int INITIAL_Y = 190;

        private Point currentPoint = new System.Drawing.Point(INITIAL_X, INITIAL_Y);
        private System.Timers.Timer mouseTimer;
        private System.Timers.Timer keyboardTimer;
        private double MOUSE_DELAY = 10.0;
        private double KEYBOARD_DELAY = 100.0;

        public TrelloGestureManager()
        {
            InitializeCallbacks();
        }

        private void InitializeCallbacks()
        {
            mouseTimer = new System.Timers.Timer(MOUSE_DELAY);
            mouseTimer.AutoReset = true;
            mouseTimer.Elapsed += new ElapsedEventHandler(_post_Processing_mouse);

            keyboardTimer = new System.Timers.Timer(KEYBOARD_DELAY);
            keyboardTimer.AutoReset = true;
            keyboardTimer.Elapsed += new ElapsedEventHandler(_post_Processing_keyboard);
        }


        internal void ProcessGesture(Gesture gesture)
        {
            switch (gesture)
            {
                case Gesture.PickUp:
                    MouseInterop.LeftDown();            
                    mouseTimer.Start();
                    break;
                case Gesture.PutDown:
                    MouseInterop.LeftUp();
                    break;
                case Gesture.HandUp:
                    MouseInterop.Move(0, GetNextUpY());                    
                    break;
                case Gesture.HandDown:
                    MouseInterop.Move(0, GetNextDownY());
                    break;
                case Gesture.HandLeft:
                    MouseInterop.Move(GetNextLeftX(), 0);                    
                    break;
                case Gesture.HandRight:
                    MouseInterop.Move(GetNextRightX(), 0);
                    break;
                case Gesture.Cancel:
                    KeyboardInterop.Cancel();
                    break;
                case Gesture.View:
                    KeyboardInterop.Cancel();
                    MouseInterop.LeftUp();
                    keyboardTimer.Start();
                    break;
                case Gesture.ToggleAssign:
                    KeyboardInterop.Cancel();
                    System.Threading.Thread.Sleep(100);
                    MouseInterop.LeftUp();
                    System.Threading.Thread.Sleep(100);
                    KeyboardInterop.Space();
                    break;
                default:
                    break;

            }
        }

        private void _post_Processing_mouse(object sender, ElapsedEventArgs e)
        {
            MouseInterop.Jiggle();
            mouseTimer.Stop();
        }

        private void _post_Processing_keyboard(object sender, ElapsedEventArgs e)
        {
            KeyboardInterop.Enter();
            keyboardTimer.Stop();
        }
        
        private int GetNextRightX()
        {
            return 6;
        }

        private int GetNextLeftX()
        {
            if (System.Windows.Forms.Cursor.Position.X > INITIAL_X-50)
            {
                return -6;
            }
            else
            {
                return 0;
            }
        }

        private int GetNextDownY()
        {
            return 3;
        }

        private int GetNextUpY()
        {
            
            if (System.Windows.Forms.Cursor.Position.Y > INITIAL_Y -10)
            {
                return -3;
            }
            else
            {
                return 0;
            }
        }
        
        internal void ResetCursorPosition()
        {
            System.Windows.Forms.Cursor.Position = currentPoint;
        }
    }
}
