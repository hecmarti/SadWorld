using Laugh.SceneObject;
using System;
using UnityEngine;

namespace Laugh
{
    public class Door : ObjectInteractionBase
    {
        public event Action OnOpened;

        [SerializeField]
        private GrabableObjectInteraction key = default;

        private bool Locked => !key.isGrabbed;

        [SerializeField]
        private AudioSource lockedSound;

        [SerializeField]
        private AudioSource openSound;

        [SerializeField]
        private GameObject doorClosed;

        [SerializeField]
        private GameObject doorOpened;

        public override void ExecuteAction(Transform item)
        {
            if (Locked)
            {
                lockedSound.Play();
            }
            else
            {
                key.gameObject.SetActive(false);
                OnOpened?.Invoke();
                openSound.Play();

                doorClosed.SetActive(false);
                doorOpened.SetActive(true);

                FindObjectOfType<CharacterGraber>().UnGrab();
            }
        }
    }

}