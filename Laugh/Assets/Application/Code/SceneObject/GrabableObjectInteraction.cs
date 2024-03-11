
using UnityEngine;

namespace Laugh.SceneObject
{
    public class GrabableObjectInteraction : ObjectInteractionBase
    {
        public bool isGrabbed = false;
        

        public override void ExecuteAction(Transform item)
        {
            if (!isGrabbed)
            {
                GrabObject();
            }
        }

        private void GrabObject()
        {
            isGrabbed = Player.Player.Instance.Graber.Grab(transform);
        }

        public void UnGrabObject()
        {
            isGrabbed = false;
        }
    }
}
