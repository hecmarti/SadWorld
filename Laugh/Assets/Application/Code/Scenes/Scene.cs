using Laugh.Music;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Laugh
{
    public class Scene : MonoBehaviour
    {
        [SerializeField]
        private Transform playerStartPosition;

        private Vector3 lastPlayerPosition;

        [SerializeField]
        private string nextSceneName;

        [SerializeField]
        private SceneFadeEffect sceneFadeEffect;

        [SerializeField]
        private BackgroundMusic backgroundMusicPrefab = default;

        public BackgroundMusic BackgroundMusic { get; private set; }

        private void Awake()
        {
            BackgroundMusic = GetBackgroundMusic();
        }

        protected virtual void Start()
        {
            gameObject.SetActive(false);
            LaughSceneManager.CheckIsFirstScene(this);
        }

        internal void Initialize()
        {
            DontDestroyOnLoad(gameObject);

            if (Player.Player.Instance != null)
            {
                Player.Player.Instance.transform.SetParent(playerStartPosition.parent);
                Player.Player.Instance.MoveTo(playerStartPosition.position);
            }
        }

        public virtual void Restore()
        {
            Player.Player.Instance.MoveTo(lastPlayerPosition);
        }

        public virtual void StartScene()
        {
            gameObject.SetActive(true);

            if (sceneFadeEffect != null)
            {
                SceneFadeIn();
            }
        }

        public async Task SceneFadeIn()
        {
            if (sceneFadeEffect != null)
            {
                await sceneFadeEffect.FadeOut();
            }
        }

        public async Task SceneFadeOut()
        {
            if (sceneFadeEffect != null)
            {
                await sceneFadeEffect.FadeIn();
            }
        }

        public virtual void SceneCompleted()
        {
            LoadNextScene();
        }

        internal void Hide()
        {
            if (Player.Player.Instance != null)
            {
                Player.Player.Instance.transform.SetParent(null);
                lastPlayerPosition = Player.Player.Instance.transform.position;
            }

            gameObject.SetActive(false);
        }

        private async void LoadNextScene()
        {
            if (sceneFadeEffect != null)
            {
                await sceneFadeEffect.FadeIn();
            }

            if (nextSceneName != null)
            {
                LaughSceneManager.LoadScene(nextSceneName);
            }
        }

        protected BackgroundMusic GetBackgroundMusic()
        {
            if (BackgroundMusic.Instance == null)
            {
                return Instantiate(backgroundMusicPrefab, null);
            }
            return BackgroundMusic.Instance;
        }
    }
}