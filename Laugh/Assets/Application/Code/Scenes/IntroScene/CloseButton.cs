using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Laugh.Scenes.IntroScene
{
    public class CloseButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(CloseGame);
        }

        private void CloseGame()
        {
            Application.Quit();
        }
    }
}
