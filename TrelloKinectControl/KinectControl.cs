using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrelloKinectControl.Gestures;

namespace TrelloKinectControl.Kinect
{
    class KinectControl
    {

        System.Timers.Timer timer;
        KinectSensor kinectSensor;
        private double TIMER_DELAY = 700;

        private GestureFinder gestureFinder;

        public void Initialize()
        {
            InitializeTrello();
            InitializeKinect();
            InitializeTimer();
            gestureFinder = new GestureFinder();
        }

        private void _poll_Frame(object sender, ElapsedEventArgs e)
        {
            Skeleton skeleton = FindSkeleton();
            if (skeleton != null)
            {
                ProcessSkeleton(skeleton);
            }
        }

        private Skeleton FindSkeleton()
        {
            var skeletonData = kinectSensor.SkeletonStream.OpenNextFrame(200);
            if (skeletonData != null)
            {
                Skeleton[] skeletons = new Skeleton[skeletonData.SkeletonArrayLength];
                skeletonData.CopySkeletonDataTo(skeletons);
                return skeletons.First();
            }
            return null;
        }

        private void ProcessSkeleton(Skeleton skeleton)
        {
            Gesture gesture = gestureFinder.GetGesture(skeleton);
            gesture.MouseAction(); ;
        }

        private void InitializeKinect()
        {
            foreach (KinectSensor kinect in KinectSensor.KinectSensors)
            {
                if (kinect.Status == KinectStatus.Connected)
                {
                    kinectSensor = kinect;
                    kinectSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    kinectSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                    kinectSensor.SkeletonStream.Enable(new TransformSmoothParameters
                    {
                        Smoothing = 0.5f,
                        Correction = 0.5f,
                        Prediction = 0.5f,
                        JitterRadius = 0.05f,
                        MaxDeviationRadius = 0.04f
                    });
                    kinectSensor.Start();
                    break;
                }
            }
        }


        private void InitializeTrello()
        {
            PositionCursor();
        }

        private void PositionCursor()
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(200, 250);
        }

        private void InitializeTimer()
        {
            timer = new System.Timers.Timer(TIMER_DELAY);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(_poll_Frame);
            timer.Start();
        }


    }
}
