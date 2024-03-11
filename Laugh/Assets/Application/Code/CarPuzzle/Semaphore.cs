using System.Threading.Tasks;
using UnityEngine;

public class Semaphore : MonoBehaviour
{
    [SerializeField]
    private CarLogic carLogic;
    [SerializeField]
    private NPC npc;
    [SerializeField]
    private SpriteRenderer cables;
    [SerializeField]
    private ParticleSystem particles;

    public async void FixSemaphore()
    {
        npc.ChangeNPCMood(NPCStatus.Happy);
        cables.enabled = false;
        particles.Stop();

        await Task.Delay(1000);

        carLogic.SetCarMovement();
    }
}
