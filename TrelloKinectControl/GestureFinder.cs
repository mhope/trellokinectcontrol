using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloKinectControl;

namespace TrelloKinectControl.Gestures
{
    public class GestureFinder
    {
        private bool hasBeenExtended = false;
        private  bool mousePressed = false;
        private int gestureCount = 0;

        public Gesture GetGesture(Skeleton skeleton)
        {
            SkeletonPoints points = GetPointsFrom(skeleton);

            System.Diagnostics.Debug.WriteLine("#Processing gesture" + ++gestureCount);

            DebugPoint("Head", points.Head);
            DebugPoint("Spine", points.Spine);
            DebugPoint("Shoulder right", points.ShoulderRight);
            DebugPoint("Elbow right", points.ElbowRight);
            DebugPoint("Wrist right", points.WristRight);
            DebugPoint("Hand right", points.HandRight);

            System.Diagnostics.Debug.Write("# S<->H: " + Math.Round(points.ShoulderRight.Y - points.HandRight.Y, 2));
            System.Diagnostics.Debug.Write(", H<->E: " + Math.Round(points.HandRight.Y - points.ElbowRight.Y, 2));
            System.Diagnostics.Debug.Write(", E<->H: " + Math.Round(points.ElbowRight.Y - points.HandRight.Y, 2));
            System.Diagnostics.Debug.Write("\n");

            // not interacting if hand not raise
            if (points.HandRight.Y == 0 && points.HandRight.Z == 0)
            {
                return Gesture.NotInteracting;
            }

            System.Diagnostics.Debug.Print("\t\tInteracting");
            if (IsArmHangingDown(skeleton)) {
                return Gesture.Cancel;
            }
            if (IsArmExtended(skeleton))
            {
                System.Diagnostics.Debug.Print("\t\t\t\tExtended");
                hasBeenExtended = true;
                return Gesture.None;
            }
            else if (IsHandNearHead(skeleton))
            {
                System.Diagnostics.Debug.Print("\t\t\t\tHand near head");
                if (mousePressed)
                {
                    mousePressed = false;
                    return Gesture.View;
                }
                return Gesture.None;
            }
            else if (IsHandNearSpine(skeleton))
            {
                System.Diagnostics.Debug.Print("\t\t\t\tHand near shoulder");
                if (mousePressed)
                {
                    return Gesture.ToggleAssign;
                }
                return Gesture.None;
            }

            else if (IsArmRetracted(skeleton))
            {
                if (hasBeenExtended)
                {
                    hasBeenExtended = false;
                    if (!mousePressed)
                    {
                        System.Diagnostics.Debug.Print("\t\t\t\tRetracted - picking up");
                        mousePressed = true;
                        return Gesture.PickUp;
                    }
                    else
                    {
                        System.Diagnostics.Debug.Print("\t\t\t\tRetracted - putting down");
                        mousePressed = false;
                        return Gesture.PutDown;
                    }
                }
                else
                {
                    return FindMovementGesture(points.ElbowRight, points.HandRight);
                }
            }
            else
            {
                return FindMovementGesture(points.ElbowRight, points.HandRight);
            }
        }


        private bool IsHandNearSpine(Skeleton skeleton)
        {

            SkeletonPoints points = GetPointsFrom(skeleton);
            return Math.Abs(points.Spine.Y - points.HandRight.Y) < 0.05 && Math.Abs(points.Spine.Z - points.HandRight.Z) < 0.2;
        }


        private bool IsHandNearHead(Skeleton skeleton)
        {
            SkeletonPoints points = GetPointsFrom(skeleton);
            return Math.Abs(points.Head.Y - points.HandRight.Y) < 0.05 && Math.Abs(points.Head.Z - points.HandRight.Z) < 0.2;
        }

        private bool IsArmHangingDown(Skeleton skeleton)
        {
            SkeletonPoints points = GetPointsFrom(skeleton);
            return points.ShoulderRight.Z - points.HandRight.Z < 0.1;
        }

        private bool IsArmExtended(Skeleton skeleton)
        {
            SkeletonPoints points = GetPointsFrom(skeleton);
            return points.HandRight.Z < 0.9;
        }


        private bool IsArmRetracted(Skeleton skeleton)
        {
            SkeletonPoints points = GetPointsFrom(skeleton);
            return points.HandRight.Z > 1.0;
        }

        private Gesture FindMovementGesture(SkeletonPoint rightElbow, SkeletonPoint rightHand)
        {
            if (rightHand.Y - rightElbow.Y > 0.1)
            {
                System.Diagnostics.Debug.Print("\t\t\tHand Up");
                return Gesture.HandUp;
            } else if (rightElbow.Y - rightHand.Y > 0.1)
            {
                System.Diagnostics.Debug.Print("\t\t\tHand Down");
                return Gesture.HandDown;
            }
            else if (rightElbow.X - rightHand.X > 0.2)
            {
                System.Diagnostics.Debug.Print("\t\t\tHand Left");
                return Gesture.HandLeft;
            }
            else if (rightHand.X - rightElbow.X > 0.2)
            {
                System.Diagnostics.Debug.Print("\t\t\tHand Right");
                return Gesture.HandRight;
            }
            return Gesture.None;
        }
        
        private SkeletonPoints GetPointsFrom(Skeleton skeleton)
        {
            return new SkeletonPoints(
                skeleton.Joints[JointType.Head].Position,
                skeleton.Joints[JointType.Spine].Position,
                skeleton.Joints[JointType.ShoulderRight].Position,
                skeleton.Joints[JointType.ElbowRight].Position, 
                skeleton.Joints[JointType.WristRight].Position,
                skeleton.Joints[JointType.HandRight].Position);
        }


        private void DebugPoint(String name, SkeletonPoint point)
        {
            System.Diagnostics.Debug.WriteLine(" - " + name + ": X=" + Math.Round(point.X, 2) + ", Y=" + Math.Round(point.Y, 2) + ", Z=" + Math.Round(point.Z, 2));
        }

    }
}
