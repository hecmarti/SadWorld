using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    private NPC npc;

    [SerializeField]
    private GameObject sad;
    [SerializeField]
    private GameObject happy;

    private void Awake()
    {
        SetDialogBox(npc.Status);
        npc.OnStatusChnaged += Npc_OnStatusChnaged;
    }

    private void Npc_OnStatusChnaged(object sender, NPCStatus e)
    {
        SetDialogBox(e);
    }

    private void SetDialogBox(NPCStatus nPCStatus)
    {
        if(nPCStatus == NPCStatus.Happy)
        {
            sad.SetActive(false);
            happy.SetActive(true);
        }
        else
        {
            sad.SetActive(true);
            happy.SetActive(false);
        }
    }
}
