using DG.Tweening;
using System;
using UnityEngine;

public enum NPCStatus
{
    Happy, SAD
}

public class NPC : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite sadSprite;
    [SerializeField]
    private Sprite happySprite;
    [SerializeField]
    private bool singleSprite;
    [SerializeField]
    private NPCStatus status;
    public NPCStatus Status => status;

    [Header("Audios")]
    [SerializeField]
    private bool usesAudios;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip sadAudio;
    [SerializeField]
    private AudioClip happyAudio;

    public event EventHandler<NPCStatus> OnStatusChnaged;

    private Material material;

    [Header("Coloring")]
    [SerializeField]
    private float to = 1;
    [SerializeField]
    private float time = 0.5f;

    private void Start()
    {
        if(!singleSprite) spriteRenderer.sprite = status == NPCStatus.Happy ? happySprite : sadSprite;
        if (usesAudios)
        {
            audioSource.clip = status == NPCStatus.Happy ? happyAudio : sadAudio;
            audioSource.Play();
        }
        material = spriteRenderer.material;
    }

    public void ChangeNPCMood(NPCStatus status)
    {
        this.status = status;

        if (!singleSprite) spriteRenderer.sprite = status == NPCStatus.Happy ? happySprite : sadSprite;
        if (usesAudios)
        {
            audioSource.clip = status == NPCStatus.Happy ? happyAudio : sadAudio;
            audioSource.Play();
        }

        DOTween.To(() => material.GetFloat("_FadeValue"), value => material.SetFloat("_FadeValue", value), to, time);

        OnStatusChnaged?.Invoke(this, status);
    }
}
