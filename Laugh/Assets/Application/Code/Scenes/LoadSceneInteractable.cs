using Laugh.SceneObject;
using UnityEngine;

namespace Laugh
{
    public class LoadSceneInteractable : ObjectInteractionBase
    {
        [SerializeField]
        private string sceneName = default;

        public override void ExecuteAction(Transform item)
        {
            LaughSceneManager.LoadScene(sceneName);
        }
    }
}