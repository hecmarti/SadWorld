using DG.Tweening;
using Laugh;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroNpcController : MonoBehaviour
{
    [SerializeField]
    private GameObject npcPrefab = default;

    [SerializeField]
    private float npcMoveSpeed = default;

    [SerializeField]
    private float spawnSpeed = default;

    [SerializeField]
    private int spawnCount = default;

    [SerializeField]
    private Transform leftSpawnPosition = default;

    [SerializeField]
    private Transform rightSpawnPosition = default;

    [SerializeField]
    private Transform centerPosition = default;

    [SerializeField]
    private Transform doorPoint;

    [SerializeField]
    private SpriteRenderer factorySpriteRenderer = default;

    private bool isLeftSide;
    private int spawnedCount;

    [ContextMenu("StartSequence")]
    public void StartSequence()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        while (spawnedCount < spawnCount)
        {
            isLeftSide = !isLeftSide;

            GameObject npc;

            if (isLeftSide)
            {
                npc = Instantiate(npcPrefab, leftSpawnPosition);
            }
            else
            {

                npc = Instantiate(npcPrefab, rightSpawnPosition);
                npc.transform.localScale = new Vector3(-npc.transform.localScale.x, npc.transform.localScale.y, npc.transform.localScale.z);
            }

            npc.transform.DOMoveX(centerPosition.position.x, npcMoveSpeed).SetSpeedBased().OnComplete(() =>
            {
                npc.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
                npc.transform.DOScale(Vector3.zero, 1);
                npc.transform.DOMove(doorPoint.position, 1).OnComplete(() =>
                {
                    npc.gameObject.SetActive(false);
                });
            });

            Material factoryMaterial = factorySpriteRenderer.material;
            Material npcMaterial = npc.GetComponentInChildren<SpriteRenderer>().material;

            float fadeValue = factoryMaterial.GetFloat("_FadeValue") - 1 / (float)spawnCount;

            npcMaterial.SetFloat("_FadeValue", fadeValue);
            factoryMaterial.SetFloat("_FadeValue", fadeValue);

            spawnedCount++;

            yield return new WaitForSeconds(spawnSpeed);
        }

        LaughSceneManager.LoadScene("Bedroom");
    }
}
