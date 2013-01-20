using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrelloKinectControl.Keyboard;
using TrelloKinectControl.Mouse;

namespace TrelloKinectControl.Gestures
{
    public enum Gesture
    {
        HandUp,
        HandDown,
        HandLeft,
        HandRight,
        None,
        NotInteracting,
        PickUp,
        PutDown,
        Cancel,
        View,
        ToggleAssign
    }
}
