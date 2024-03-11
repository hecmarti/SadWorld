using Laugh;
using Laugh.SceneObject;
using System.Threading.Tasks;
using UnityEngine;

public class CarLogic : MonoBehaviour
{
    [SerializeField]
    private InteractableSceneObject binInteractableSceneObject;
    [SerializeField]
    private NPC npc;
    [SerializeField]
    private MoverCoche coche;
    [SerializeField]
    private AudioSource audioSource;

    public async void SetCarMovement()
    {
        npc.ChangeNPCMood(NPCStatus.Happy);
        await Task.Delay(1000);
        GameManager.Instance.PaintAroundCar();

        audioSource.Play();
        coche.MoveCarForward();
        binInteractableSceneObject.IsInteractionEnabled = true;
    }
}
