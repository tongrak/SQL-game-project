using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameHelper
{
    public class SceneLoadingHelper : MonoBehaviour
    {
        //dynamic var
        private Scene _sceneToUnload;

        public void UnloadScene(Scene scene)
        {
            _sceneToUnload = scene;
            if (_sceneToUnload.IsValid()) SceneManager.sceneLoaded += ActivatorAndUnloader;
            else throw new System.Exception("Scene: " + scene.name + "cann't be unloaded; Due to scene is invalid");
        }

        public void UnloadScene(string scene) 
        {
            UnloadScene(SceneManager.GetSceneByName(scene));
        }


        public void LoadSceneAdditively(Scene scene)
        {
            LoadSceneAdditively(scene.name);
        }

        public void LoadSceneAdditively(string scene)
        {
            if (!SceneManager.GetSceneByName(scene).IsValid()) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            else throw new System.Exception("Scene: " + scene + "already be loaded;");
        }

        public void SwapScene(string toUnload, string toLoad)
        {
            try
            {
                this.UnloadScene(toUnload);
                this.LoadSceneAdditively(toLoad);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Cann't swap scenes due to " + ex.Message);
            }
        }

        private void ActivatorAndUnloader(Scene s, LoadSceneMode m)
        {
            SceneManager.sceneLoaded -= ActivatorAndUnloader;
            SceneManager.SetActiveScene(s);
            SceneManager.UnloadSceneAsync(_sceneToUnload);
        }
    }
}

