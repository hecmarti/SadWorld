using Laugh;
using Laugh.SceneObject;
using UnityEngine;

public class CharacterGraber : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement characterMovement;

    [SerializeField]
    private Transform grabPosition;

    [SerializeField]
    private AudioSource grabAudio;

    private Rigidbody2D grabbedObjectRgbd;

    public bool itemOnHand = false;
    private SpriteRenderer grabbedItemSprite;
    private int grabbedItemSortingLayer;
    private int grabbedItemSortingOrder;

    public Transform item => grabbedObjectRgbd.transform;

    public bool Grab(Transform item)
    {
        if (itemOnHand) return false;
        itemOnHand = true;

        grabAudio.Play();

        grabbedObjectRgbd = item.GetComponent<Rigidbody2D>();

        item.SetParent(transform);
        item.position = grabPosition.position;
        item.localRotation = Quaternion.identity;

        grabbedItemSprite = item.GetComponentInChildren<SpriteRenderer>(true);

        if (grabbedItemSprite != null)
        {
            grabbedItemSortingLayer = grabbedItemSprite.sortingLayerID;
            grabbedItemSortingOrder = grabbedItemSprite.sortingOrder;

            grabbedItemSprite.sortingLayerName = "Playground";
            grabbedItemSprite.sortingOrder = 10;
        }

        grabbedObjectRgbd.simulated = false;
        grabbedObjectRgbd.gravityScale = 0;

        return true;
    }

    public void UnGrab()
    {
        itemOnHand = false;

        grabbedObjectRgbd.GetComponent<GrabableObjectInteraction>().UnGrabObject();

        var scene = FindObjectOfType<Scene>();

        var itemTransform = grabbedObjectRgbd.transform;
        itemTransform.SetParent(scene.transform);
        itemTransform.rotation = Quaternion.Euler(itemTransform.rotation.x, 0, itemTransform.rotation.y);
        itemTransform.position = new Vector3(itemTransform.position.x, itemTransform.position.y, 0);

        grabbedObjectRgbd.simulated = true;
        grabbedObjectRgbd.gravityScale = 1;

        if (grabbedItemSprite != null)
        {
            grabbedItemSprite.sortingLayerID = grabbedItemSortingLayer;
            grabbedItemSprite.sortingOrder = grabbedItemSortingOrder;
        }

        grabbedObjectRgbd = null;
    }
}
