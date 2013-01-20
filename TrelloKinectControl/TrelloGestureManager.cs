using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TrelloKinectControl.Keyboard;
using TrelloKinectControl.Mouse;

namespace TrelloKinectControl.Gestures
{
    class TrelloGestureManager
    {
        private static readonly int INITIAL_X = 200;
        private static readonly int INITIAL_Y = 195;
        private static readonly int CARD_HEIGHT = 32;
        private static readonly int COLUMN_WIDTH = 120;

        private Point currentPoint = new System.Drawing.Point(INITIAL_X, INITIAL_Y);
        private LinkedList<int> rowYList;
        private LinkedList<int> columnXList;

        public TrelloGestureManager()
        {
            InitializePositionArrays();
        }

        internal void processGesture(Gesture gesture)
        {
            switch (gesture)
            {
                case Gesture.PickUp:
                    MouseInterop.LeftDown();
                    System.Threading.Thread.Sleep(500);
                    MouseInterop.Jiggle();
                    break;
                case Gesture.PutDown:
                    MouseInterop.LeftUp();
                    ResetCursorPosition();
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
                    KeyboardInterop.Enter();
                    break;
                case Gesture.ToggleAssign:
                    KeyboardInterop.Space();
                    break;
                default:
                    break;
            }
        }

        private int GetNextRightX()
        {
            LinkedListNode<int> xNode = columnXList.Find(currentPoint.X);
            LinkedListNode<int> rightNode = xNode.Next;
            if (rightNode != null)
            {
                int change = rightNode.Value - currentPoint.X;
                currentPoint = new Point(rightNode.Value, currentPoint.Y);
                return change;
            }
            else
            {
                return 0;
            }
        }

        private int GetNextLeftX()
        {

            LinkedListNode<int> xNode = columnXList.Find(currentPoint.X);
            LinkedListNode<int> leftNode = xNode.Previous;
            if (leftNode != null)
            {
                int change = leftNode.Value - currentPoint.X;
                currentPoint = new Point(leftNode.Value, currentPoint.Y);
                return change;
            }
            else
            {
                return 0;
            }
        }

        private int GetNextDownY()
        {
            LinkedListNode<int> yNode = rowYList.Find(currentPoint.Y);
            LinkedListNode<int> lowerNode = yNode.Next;
            if (lowerNode != null)
            {
                int change = lowerNode.Value - currentPoint.Y;
                currentPoint = new Point(currentPoint.X, lowerNode.Value);
                return change;
            }
            else
            {
                return 0;
            }
        }

        private int GetNextUpY()
        {

            LinkedListNode<int> yNode = rowYList.Find(currentPoint.Y);
            LinkedListNode<int> higherNode = yNode.Previous;
            if (higherNode != null)
            {
                int change = higherNode.Value - currentPoint.Y;
                currentPoint = new Point(currentPoint.X, higherNode.Value);
                return change;
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



        private void InitializePositionArrays()
        {
            int[] yArray = new int[8];
            int[] xArray = new int[3];
            int y = INITIAL_Y;
            for (int i = 0; i < 8; i++)
            {
                yArray[i] = y;
                y += CARD_HEIGHT;
            }
            int x = INITIAL_X;
            for (int i = 0; i < 3; i++)
            {
                xArray[i] = x;
                x += COLUMN_WIDTH;
            }
            rowYList = new LinkedList<int>(yArray);
            columnXList = new LinkedList<int>(xArray);
        }

    }
}
