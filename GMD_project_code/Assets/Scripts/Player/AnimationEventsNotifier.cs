using UnityEngine;

namespace GMDProject
{
    public class AnimationEventsNotifier : MonoBehaviour
    {
        public void NotifyAnimationEnd(Object handler)
        {
            ((IAnimationEventHandler)handler).AnimationEnd();
        }
    }
}
