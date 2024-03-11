using Laugh.Music;
using System;
using System.Collections;
using UnityEngine;

namespace Laugh
{
    public class BedroomScene : Scene
    {
        [SerializeField]
        private bool skipIntroInEditor = default;

        [SerializeField]
        private RandomColorFade randomColorFade = default;

        [SerializeField]
        private Door door = default;

        [SerializeField]
        private GameObject gamePlayPlayer = default;

        [SerializeField]
        private Animator introAnimator = default;

        public override void StartScene()
        {
            base.StartScene();

            randomColorFade.SetFade(0);

            door.OnOpened += OnDoorOpened;

            if (Application.isEditor && skipIntroInEditor)
            {
                introAnimator.enabled = false;
                gamePlayPlayer.SetActive(true);
            }
            else
            {
                StartCoroutine(WaitForMusicStart());
            }
        }

        private IEnumerator WaitForMusicStart()
        {
            float animationTime = 13f;
            float timeCount = 0f;
            while(timeCount < animationTime)
            {
                yield return null;
                timeCount += Time.deltaTime;
            }
            BackgroundMusic.Instance.Play();
        }

        private void OnDoorOpened()
        {
            randomColorFade.Play(1);

            Invoke(nameof(SceneCompleted), 5);
        }
    }
}