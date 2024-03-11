using Laugh.SceneObject;
using UnityEngine;

namespace Laugh
{
    public class Cables : ObjectInteractionBase
    {
        [SerializeField]
        private string cintaItemName = "Cinta";

        [SerializeField]
        private InteractableSceneObject interactableSceneObject;

        private bool Locked;

        [SerializeField]
        private AudioSource chispazoSound;

        [SerializeField]
        private AudioSource fixedSound;

        [SerializeField]
        private Semaphore semaphore;

        public override void ExecuteAction(Transform item)
        {
            Locked = item == null || cintaItemName != item.name;

            if (Locked)
            {
                chispazoSound.Play();
            }
            else
            {
                interactableSceneObject.IsInteractionEnabled = false;
                item.gameObject.SetActive(false);
                fixedSound.Play();
                semaphore.FixSemaphore();

                FindObjectOfType<CharacterGraber>().UnGrab();
            }
        }
    }

}