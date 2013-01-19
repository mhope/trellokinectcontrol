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
        private SkeletonPoint shoulderCentre;
        private SkeletonPoint shoulderRight;
        private SkeletonPoint elbowRight;
        private SkeletonPoint handRight;
        private SkeletonPoint wristRight;

        public SkeletonPoints(
            SkeletonPoint head, 
            SkeletonPoint shoulderCentre, 
            SkeletonPoint shoulderRight, 
            SkeletonPoint elbowRight,
            SkeletonPoint wristRight, 
            SkeletonPoint handRight)
        {
            this.head = head;
            this.shoulderCentre = shoulderCentre;
            this.shoulderRight = shoulderRight;
            this.elbowRight = elbowRight;
            this.wristRight = wristRight;
            this.handRight = handRight;
        }

        public SkeletonPoint Head()
        {
            return head;
        }
        public SkeletonPoint ShoulderCentre()
        {
            return shoulderCentre;
        }
        public SkeletonPoint ShoulderRight()
        {
            return shoulderRight;
        }
        public SkeletonPoint ElbowRight()
        {
            return elbowRight;
        }
        public SkeletonPoint WristRight()
        {
            return wristRight;
        }
        public SkeletonPoint HandRight()
        {
            return handRight;
        }
        
    }
}
