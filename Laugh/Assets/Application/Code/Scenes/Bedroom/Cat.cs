using Laugh.SceneObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh
{
    public class Cat : ObjectInteractionBase
    {
        [SerializeField]
        private AudioSource catSurprise = default;

        [SerializeField]
        private Animator catKeyAnimator = default;

        [SerializeField]
        private InteractableSceneObject key = default;

        [SerializeField]
        private AudioSource potSound = default;

        private void Start()
        {
            key.IsInteractionEnabled = false;
        }

        public override void ExecuteAction(Transform item)
        {
            InteractableSceneObject interactableSceneObject = GetComponent<InteractableSceneObject>();
            interactableSceneObject.IsInteractionEnabled = false;

            catSurprise.Play();

            catKeyAnimator.SetTrigger("Start");

            StartCoroutine(PlayPotSounds());
        }

        private IEnumerator PlayPotSounds()
        {
            yield return new WaitForSeconds(1.25f);

            potSound.Play();

            yield return new WaitForSeconds(.15f);

            potSound.Play();

            yield return new WaitForSeconds(.05f);

            potSound.Play();

            yield return new WaitForSeconds(1f);

            key.IsInteractionEnabled = true;
            catKeyAnimator.enabled = false;
        }
    }
}