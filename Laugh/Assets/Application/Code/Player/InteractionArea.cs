using Laugh.SceneObject;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Laugh.Player
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;
        [SerializeField]
        private CharacterGraber characterGraber;

        List<InteractableSceneObject> interactables = new List<InteractableSceneObject>();

        private void Start()
        {
            playerInput.actions["Action"].performed += InteractionArea_performed;
        }

        private void InteractionArea_performed(InputAction.CallbackContext obj)
        {
            var item = interactables.FirstOrDefault(x => x.Interactable);

            if (item != null)
            {
                if (characterGraber.itemOnHand)
                {
                    item.GetComponent<ObjectInteractionBase>().ExecuteAction(characterGraber.item);
                }
                else
                {
                    item.GetComponent<ObjectInteractionBase>().ExecuteAction(null);
                }
            }
            else if (characterGraber.itemOnHand)
            {
                characterGraber.UnGrab();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int layer = LayerMask.NameToLayer("SceneObject");
            if (collision.gameObject.layer != layer) return;

            var sceneObject = collision.gameObject.GetComponent<InteractableSceneObject>();

            if (sceneObject.IsInteractionEnabled) interactables.Add(sceneObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            int layer = LayerMask.NameToLayer("SceneObject");
            if (collision.gameObject.layer != layer) return;

            interactables.Remove(collision.gameObject.GetComponent<InteractableSceneObject>());
        }
    }
}