using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrelloKinectControl
{
    class SkeletonPoints
    {
        private SkeletonPoint head;
        private SkeletonPoint spine;
        private SkeletonPoint shoulderRight;
        private SkeletonPoint elbowRight;
        private SkeletonPoint handRight;
        private SkeletonPoint wristRight;

        public SkeletonPoints(
            SkeletonPoint head, 
            SkeletonPoint spine, 
            SkeletonPoint shoulderRight, 
            SkeletonPoint elbowRight,
            SkeletonPoint wristRight, 
            SkeletonPoint handRight)
        {
            this.head = head;
            this.spine = spine;
            this.shoulderRight = shoulderRight;
            this.elbowRight = elbowRight;
            this.wristRight = wristRight;
            this.handRight = handRight;
        }

        public SkeletonPoint Head
        {
            get
            {
                return head;
            }
        }
        public SkeletonPoint Spine
        {
            get
            {
                return spine;
            }
        }
        public SkeletonPoint ShoulderRight
        {
            get
            {
                return shoulderRight;
            }
        }
        public SkeletonPoint ElbowRight
        {
            get
            {
                return elbowRight;
            }
        }
        public SkeletonPoint WristRight
        {
            get
            {
                return wristRight;
            }
        }
        public SkeletonPoint HandRight
        {
            get
            {
                return handRight;
            }
        }
        
    }
}
