using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Laugh
{
    [RequireComponent(typeof(Camera))]
    public class StepCamera : MonoBehaviour
    {

        [SerializeField]
        private Transform target = default;

        [SerializeField]
        private float screenPositionMargins = 0.1f;

        [SerializeField]
        private float stepWidth = 2;

        [SerializeField]
        private int leftSteps = 1;

        [SerializeField]
        private int rightSteps = 1;

        [SerializeField]
        private float time = 1;

        private Camera cam;

        private int currentStep;
        private Vector3 initialCameraPosition;
        private bool isMoving;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            initialCameraPosition = transform.position;
        }

        private void Update()
        {
            if (isMoving)
            {
                return;
            }

            if (target == null)
            {
                target = Player.Player.Instance.transform;
            }

            Vector3 screenPosition = cam.WorldToViewportPoint(target.transform.position);

            if (screenPosition.x < screenPositionMargins)
            {
                Move(-1);
            }
            else if (screenPosition.x > (1 - screenPositionMargins))
            {
                Move(1);
            }
        }

        private async void Move(int moveValue)
        {
            int newStep = currentStep + moveValue;

            if (newStep < -leftSteps || newStep > rightSteps)
            {
                return;
            }

            currentStep = newStep;

            Time.timeScale = 0;

            isMoving = true;

            transform.DOMove(initialCameraPosition + Vector3.right * currentStep * stepWidth, time).SetUpdate(true);

            await Task.Delay(Mathf.RoundToInt(time * 1000));

            Time.timeScale = 1;

            isMoving = false;
        }
    }
}