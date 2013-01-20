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
        private double TIMER_DELAY = 800;

        private GestureFinder gestureFinder;
        private bool suspended = false;

        public KinectControl()
        {
            gestureFinder = new GestureFinder();
        }

        public void Start()
        {
            InitializeKinect();
            InitializeTimer();
            InitializeTrello();
        }
        
        public void Stop()
        {
            StopTimer();
            // Give he last timer a chance before stopping the kinect
            System.Threading.Thread.Sleep(700);
            StopKinect();
        }


        private void _poll_Frame(object sender, ElapsedEventArgs e)
        {
            Skeleton skeleton = FindSkeleton();
            if (skeleton != null && !suspended)
            {
                ProcessSkeleton(skeleton);
            }
        }


        private void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (!suspended)
            {
                SkeletonFrame frame = e.OpenSkeletonFrame();
                if (frame == null)
                {
                    return;
                }
                if (frame.SkeletonArrayLength == 0)
                {
                    return;
                }
                Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];
                frame.CopySkeletonDataTo(skeletons);
                Skeleton skeleton = skeletons[0];
                if (skeleton != null)
                {
                    ProcessSkeleton(skeleton);
                };
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
            DelayProcessingNextGesture();
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
                    kinectSensor.SkeletonStream.Enable(SmoothingParams());
                    kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
                    kinectSensor.Start();
                    kinectSensor.ElevationAngle = -5;
                    break;
                }
            }
        }


        private static TransformSmoothParameters SmoothingParams()
        {
            TransformSmoothParameters verySmoothParam = new TransformSmoothParameters();
            {
                verySmoothParam.Smoothing = 0.7f;
                verySmoothParam.Correction = 0.3f;
                verySmoothParam.Prediction = 1.0f;
                verySmoothParam.JitterRadius = 1.0f;
                verySmoothParam.MaxDeviationRadius = 1.0f;
            };

            TransformSmoothParameters smoothParam = new TransformSmoothParameters();
            {
                smoothParam.Smoothing = 0.5f;
                smoothParam.Correction = 0.1f;
                smoothParam.Prediction = 0.5f;
                smoothParam.JitterRadius = 0.1f;
                smoothParam.MaxDeviationRadius = 0.1f;
            };

            TransformSmoothParameters fastSmoothingParam = new TransformSmoothParameters();
            {
                fastSmoothingParam.Smoothing = 0.5f;
                fastSmoothingParam.Correction = 0.5f;
                fastSmoothingParam.Prediction = 0.5f;
                fastSmoothingParam.JitterRadius = 0.05f;
                fastSmoothingParam.MaxDeviationRadius = 0.04f;
            };

            return verySmoothParam;
        }


        private void InitializeTrello()
        {
            PositionCursor();
        }

        private void PositionCursor()
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(200, 190);
        }

        private void InitializeTimer()
        {
            timer = new System.Timers.Timer(TIMER_DELAY);
            timer.AutoReset = false;
            timer.Elapsed += new ElapsedEventHandler(_enableProcessing);
        }


        private void DelayProcessingNextGesture()
        {
            System.Diagnostics.Debug.WriteLine("$$ suspending");
            this.suspended = true;
            timer.Start();
        }

        private void _enableProcessing(object sender, ElapsedEventArgs e)
        {
            this.suspended = false;
            System.Diagnostics.Debug.WriteLine("$$ unsuspending");
        }


        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }

        private void StopKinect()
        {
            if (kinectSensor != null)
            {
                kinectSensor.Stop();
                kinectSensor = null;
            }
        }
    }
}
