using UnityEngine;
using Laugh.SceneObject;
using Laugh;

public class Robot : ObjectInteractionBase
{
    [SerializeField]
    private GameObject barrier;
    [SerializeField]
    private GameObject itemInHand;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private InteractableSceneObject interactableSceneObject;
    [SerializeField]
    private string itemName = "OilCan";
    [SerializeField]
    private AudioSource cantPass;
    [SerializeField]
    private AudioSource happyRobot;
    [SerializeField]
    private NPC npc;

    private bool Locked;

    private void Start()
    {
        animator.keepAnimatorStateOnDisable = true;
    }

    public override void ExecuteAction(Transform item)
    {
        Locked = item == null || itemName != item.name;

        if (Locked)
        {
            cantPass.Play();
        }
        else
        {
            interactableSceneObject.IsInteractionEnabled = false;
            item.gameObject.SetActive(false);

            itemInHand.SetActive(true);
            npc.ChangeNPCMood(NPCStatus.Happy);
            animator.SetTrigger("Happy");
            happyRobot.Play();
            barrier.SetActive(false);
            GetComponentInParent<Scene>().BackgroundMusic.AddTrackOnNewCompass();

            FindObjectOfType<CharacterGraber>().UnGrab();
        }
    }
}
