using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.Music.Test
{
    public class MusicTest : MonoBehaviour
    {
        private BackgroundMusic backgroundMusic;

        private void Start()
        {
            backgroundMusic = BackgroundMusic.Instance;
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) { 
                backgroundMusic.Play();
            }else if(Input.GetKeyDown(KeyCode.A)) {
                backgroundMusic.AddTrackOnNewCompass();
            }
        }
    }
}