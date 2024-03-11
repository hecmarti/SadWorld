using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.SceneObject
{
    public abstract class ObjectInteractionBase : MonoBehaviour
    {
        public abstract void ExecuteAction(Transform item);
    }
}
