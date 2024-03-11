
using UnityEngine;

namespace Laugh.SceneObject
{
    [RequireComponent(typeof(SceneObjectVisibility))]
    public class InteractableSceneObject : MonoBehaviour
    {
        private SceneObjectVisibility visibility;
        public bool Interactable { private set; get; }

        public bool IsInteractionEnabled
        {
            set
            {
                if (!value)
                {
                    GetComponentInChildren<BaseHighlight>(true).SwitchHighlight(false);
                }

                isInteractionEnabled = value;
            }
            get
            {
                return isInteractionEnabled;
            }
        }

        [SerializeField]
        private bool isInteractionEnabled = true;

        private void Awake()
        {
            visibility = GetComponent<SceneObjectVisibility>();
        }

        private void OnDisable()
        {
            SetInteractable(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsInteractionEnabled) return;

            if (!CheckLayer(collision))
            {
                return;
            }

            if (visibility.IsVisible())
            {
                SetInteractable(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!IsInteractionEnabled) return;

            if (!CheckLayer(collision))
            {
                return;
            }

            SetInteractable(false);
        }

        private bool CheckLayer(Collider2D other)
        {
            int layerMask = LayerMask.NameToLayer("Interaction");
            return other.gameObject.layer == layerMask;
        }

        private void SetInteractable(bool interactable)
        {
            this.Interactable = interactable;

            GetComponentInChildren<BaseHighlight>(true).SwitchHighlight(interactable);
        }
    }
}