using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Laugh
{
    public class RandomColorFade : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer[] except = default;

        [SerializeField]
        private float time = 0;

        [SerializeField]
        private float minDelay = 0;
        [SerializeField]
        private float maxDelay = 1;

        private IEnumerable<SpriteRenderer> targetSprites;

        private void Awake()
        {
            targetSprites = GetComponentInParent<Scene>().GetComponentsInChildren<SpriteRenderer>(true)
                .Where(s => s.material.shader.name == "Shader Graphs/PaintEffect" && !except.Contains(s));
        }

        public void SetFade(float value)
        {
            foreach (SpriteRenderer s in targetSprites.OrderBy(s => Guid.NewGuid()))
            {
                s.material.SetFloat("_FadeValue", value);
            }
        }

        public void Play(float to)
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