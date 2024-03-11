using System.Collections;
using UnityEngine;

namespace Laugh.Player
{
    public class Player : MonoBehaviour
    {
        private static Player instance;

        public static Player Instance => instance;

        private CharacterController characterController;
        private CharacterMovement characterMovement;
        public CharacterGraber Graber { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            characterController = GetComponent<CharacterController>();
            characterMovement = GetComponent<CharacterMovement>();
            Graber = GetComponentInChildren<CharacterGraber>();
        }

        public void MoveTo(Vector3 position)
        {
            characterController.enabled = false;
            characterMovement.enabled = false;

            transform.position = position;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(EnableMovementAfterFrame());
            }
            else
            {
                characterController.enabled = true;
                characterMovement.enabled = true;
            }
        }

        private IEnumerator EnableMovementAfterFrame()
        {
            yield return null;
            yield return null;
            characterController.enabled = true;
            characterMovement.enabled = true;
        }

    }
}