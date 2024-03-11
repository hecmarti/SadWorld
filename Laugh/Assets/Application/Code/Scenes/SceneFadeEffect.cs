using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Laugh
{
    public class SceneFadeEffect : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sprite;

        private void Awake()
        {
            sprite.enabled = true;
        }

        internal async Task FadeOut()
        {
            await sprite.DOFade(0, 1).AsyncWaitForCompletion();
        }

        internal async Task FadeIn()
        {
            await sprite.DOFade(1, 1).AsyncWaitForCompletion();
        }
    }
}