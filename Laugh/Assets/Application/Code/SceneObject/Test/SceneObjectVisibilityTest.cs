using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.SceneObject.Test
{
    public class SceneObjectVisibilityTest : MonoBehaviour
    {
        [SerializeField]
        private SceneObjectVisibility backObject;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                bool isVisible = backObject.IsVisible();
                Debug.Log($"{backObject.name} is visible {isVisible}");
            }
        }
    }
}