using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoverCoche : MonoBehaviour
{
    [SerializeField]
    private float moveUpOffset = default;

    [SerializeField]
    private float moveForwardOffset = default;

    [SerializeField]
    private float animateCarUpAndDownSpeed = default;

    [SerializeField]
    private float animateCarForwardSpeed = default;

    [SerializeField]
    private Transform child = default;

    private bool animateCarUpAndDown;

    private bool animateCarForward;

    private Vector3 carStartPosition;

    private Coroutine carCoroutine;

    private void Awake()
    {
        animateCarUpAndDown = true;

        carStartPosition = child.localPosition;
    }

    private void OnEnable()
    {
        carCoroutine = StartCoroutine(MoveCarUpAddDown());
    }

    private void OnDisable()
    {
        if (carCoroutine != null)
        {
            StopCoroutine(carCoroutine);
        }
    }

    IEnumerator MoveCarUpAddDown()
    {
        while (animateCarUpAndDown)
        {
            child.localPosition = new Vector3(child.localPosition.x, carStartPosition.y + moveUpOffset, child.localPosition.z);
            yield return new WaitForSeconds(animateCarUpAndDownSpeed);
            child.localPosition = new Vector3(child.localPosition.x, carStartPosition.y, child.localPosition.z);
            yield return new WaitForSeconds(animateCarUpAndDownSpeed);
        }
    }

    [ContextMenu("MoveCarForward")]
    public void MoveCarForward()
    {
        animateCarForward = true;

        StartCoroutine(MoveCarForwardCoroutine());
    }

    IEnumerator MoveCarForwardCoroutine()
    {
        while (animateCarForward)
        {
            transform.position = new Vector3(transform.position.x + moveForwardOffset, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(animateCarForwardSpeed);
        }
    }
}
