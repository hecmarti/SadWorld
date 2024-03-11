using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Laugh
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public List<SpriteRenderer> car;
        public List<SpriteRenderer> sadBoy;
        public List<SpriteRenderer> remaining;

        [SerializeField]
        private float time = 0;
        [SerializeField]
        private float minDelay = 0;
        [SerializeField]
        private float maxDelay = 1;

        private void Awake()
        {
            Instance = this;
            SetFade(0);
        }

        public void PaintAroundCar()
        {
            Play(1, car);
        }

        public void PaintAroundSadBoy()
        {
            Play(1, sadBoy);
        }

        public void PaintRemaining()
        {
            Play(1, remaining);
        }

        public void SetFade(float value)
        {
            foreach (SpriteRenderer s in car.OrderBy(s => Guid.NewGuid()))
            {
                s.material.SetFloat("_FadeValue", value);
            }

            foreach (SpriteRenderer s in sadBoy.OrderBy(s => Guid.NewGuid()))
            {
                s.material.SetFloat("_FadeValue", value);
            }

            foreach (SpriteRenderer s in remaining.OrderBy(s => Guid.NewGuid()))
            {
                s.material.SetFloat("_FadeValue", value);
            }
        }

        public void Play(float to, List<SpriteRenderer> targetSprites)
        {
            float currentTime = 0;

            foreach (SpriteRenderer s in targetSprites.OrderBy(s => Guid.NewGuid()))
            {
                float fadeValue = s.material.GetFloat("_FadeValue");

                DOTween
                    .To(
                        () => s.material.GetFloat("_FadeValue"),
                        value => s.material.SetFloat("_FadeValue", value),
                        to,
                        time)
                    .SetDelay(currentTime);

                currentTime += UnityEngine.Random.Range(minDelay, maxDelay);
            }
        }
    }
}