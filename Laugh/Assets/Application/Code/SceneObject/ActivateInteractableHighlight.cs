using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.SceneObject
{

    public class ActivateInteractableHighlight : BaseHighlight
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public override void SwitchHighlight(bool switchOn)
        {
            gameObject.SetActive(switchOn);
        }
    }

}