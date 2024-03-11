using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Laugh
{
    public static class LaughSceneManager
    {
        private static IDictionary<string, Scene> loadedScenes = new Dictionary<string, Scene>();

        private static Scene currentScene;

        internal static void CheckIsFirstScene(Scene scene)
        {
            if (currentScene == null)
            {
                loadedScenes.Add(SceneManager.GetActiveScene().name, scene);
                currentScene = scene;
                currentScene.Initialize();
                currentScene.StartScene();
            }
        }

        public static async void LoadScene(string sceneName)
        {
            Scene newScene;

            if (currentScene != null)
            {
                await currentScene.SceneFadeOut();
                currentScene.Hide();
            }

            if (!loadedScenes.ContainsKey(sceneName))
            {
                newScene = await LoadSceneAsync(sceneName);

                loadedScenes[sceneName] = newScene;
            }
            else
            {
                newScene = loadedScenes[sceneName];
                newScene.Restore();
            }

            currentScene = newScene;

            currentScene.StartScene();
        }

        private static async Task<Scene> LoadSceneAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);

            UnityEngine.SceneManagement.Scene unityScene;

            do
            {
                await UniTask.Yield();

                unityScene = SceneManager.GetSceneByName(sceneName);
            } while (!unityScene.IsValid() || !unityScene.GetRootGameObjects().Any());

            Scene scene = unityScene.GetRootGameObjects().First().GetComponent<Scene>();

            scene.Initialize();

            return scene;
        }

    }
}