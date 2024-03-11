using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Laugh.Music
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField]
        private List<MusicTrack> tracks = new List<MusicTrack>();
        private List<AudioSource> audioSources = new List<AudioSource>();

        private int currentTrack;
        private bool waitingTrack;

        public static BackgroundMusic Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void Play()
        {
            currentTrack = 0;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = tracks[currentTrack].TrackClip;
            audioSource.volume = tracks[currentTrack].Volume;
            audioSource.Play();

            audioSources.Add(audioSource);

            StartCoroutine(TracksAdder());

        }

        private IEnumerator TracksAdder()
        {
            float currentTime = 0f;
            float lastTime = 0f;
            while (true)
            {
                lastTime = currentTime;
                yield return true;
                currentTime += Time.deltaTime;

                if (waitingTrack)
                {

                    if (Mathf.Floor(currentTime) % 2 == 0 && Mathf.Ceil(lastTime) % 2 == 0)
                    {
                        AddTrack();
                        waitingTrack = false;
                    }
                }
            }
        }

        public void AddTrackOnNewCompass()
        {
            waitingTrack = true;
        }

        private void AddTrack()
        {
            waitingTrack = false;
            currentTrack++;
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            
            if (tracks[currentTrack].IntroClip != null)
            {
                PlayWithIntro(audioSource);
            }
            else
            {
                PlayTrack(audioSource);
            }

            audioSources.Add(audioSource);
        }

        private void PlayTrack(AudioSource audioSource)
        {
            audioSource.loop = true;
            audioSource.clip = tracks[currentTrack].TrackClip;
            audioSource.volume = tracks[currentTrack].Volume;
            audioSource.Play();
        }

        private void PlayWithIntro(AudioSource audioSource)
        {
            audioSource.loop = false;
            audioSource.clip = tracks[currentTrack].IntroClip;
            audioSource.volume = tracks[currentTrack].Volume;
            audioSource.Play();

            StartCoroutine(WaitFinishIntro(audioSource));
        }

        private IEnumerator WaitFinishIntro(AudioSource audioSource)
        {
            while(audioSource.isPlaying)
            {
                yield return null;
            }
            PlayTrack(audioSource);
        }

        private AudioSource AddAudiosource()
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f;

            return audioSource;
        }
    }



    [Serializable]
    public class MusicTrack
    {
        [SerializeField]
        private AudioClip introClip;
        //[SerializeField]
        private int introCompassDuration;
        [SerializeField]
        private AudioClip trackClip;
        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 5f;

        public AudioClip IntroClip => introClip;
        public int IntroCompassDuration => introCompassDuration;
        public AudioClip TrackClip => trackClip;
        public float Volume => volume;
    }
}