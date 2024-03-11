using UnityEngine;

namespace Laugh.SceneObject.Item
{
    public class OscarBin : ObjectInteractionBase
    {
        [SerializeField]
        private InteractableSceneObject interactableSceneObject;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Transform objectToShow;
        [SerializeField]
        private Vector2 launchForce = new Vector3(1,1);

        [Header("Audios")]
        [SerializeField]
        private AudioSource litOpenAudio;
        [SerializeField]
        private AudioSource oscarOutAudio;
        [SerializeField]
        private AudioSource shakeAudio;

        private void Start()
        {
            animator.keepAnimatorStateOnDisable = true;
        }

        public override void ExecuteAction(Transform item)
        {
            shakeAudio.Stop();
            animator.SetTrigger("Show_Oscar");
            litOpenAudio.Play();

            interactableSceneObject.IsInteractionEnabled = false;
        }

        public void ThrowItem()
        {
            oscarOutAudio.Play();
            objectToShow.SetParent(null);

            objectToShow.GetComponent<SpriteRenderer>().sortingOrder = 0;

            var itemRigidbody = objectToShow.GetComponent<Rigidbody2D>();
            itemRigidbody.simulated = true;
            itemRigidbody.gravityScale = 1;
            itemRigidbody.AddForce(launchForce, ForceMode2D.Impulse);
        }
    }
}

