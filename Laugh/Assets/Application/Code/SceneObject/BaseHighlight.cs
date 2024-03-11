using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.SceneObject
{
    public abstract class BaseHighlight : MonoBehaviour
    {
        public abstract void SwitchHighlight(bool switchOn);
    }
}