using Laugh;
using UnityEngine;

public class FallingBadGuyLogic : MonoBehaviour
{
    [SerializeField]
    private Animator walkAnimator;
    [SerializeField]
    private Animator fallAnimator;
    [SerializeField]
    private NPC sadNpc;
    [SerializeField]
    private NPCWalk npcWalk;
    [SerializeField]
    private Transform rotatingSprite;
    [SerializeField]
    private AudioSource audioSource;

    private bool falling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (falling) return;

        if(collision.gameObject.name == "CascaraPlatano")
        {
            Fall();
        }
    }

    private void Fall()
    {
        npcWalk.StopMoving();
        falling = true;
        walkAnimator.enabled = false;

        if (npcWalk.MovingRight)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 0));
        }

        rotatingSprite.localRotation = Quaternion.Euler(0, 0, 0);

        audioSource.Play();
        fallAnimator.enabled = true;
    }

    public void EndAnimationLogic()
    {
        sadNpc.ChangeNPCMood(NPCStatus.Happy);

        GameManager.Instance.PaintAroundSadBoy();
        GameManager.Instance.PaintRemaining();
    }
}
