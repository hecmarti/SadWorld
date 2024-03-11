using Laugh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.Scenes.StreetScene
{
    public class StreetScene : Scene
    {
        protected override void Start()
        {
            base.Start();
            BackgroundMusic.AddTrackOnNewCompass();
        }
    }
}