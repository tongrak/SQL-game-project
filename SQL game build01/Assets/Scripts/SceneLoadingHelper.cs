using System;
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

        private string _sceneNameToCheckOnLoad;
        private Action _actOnCheckOnLoad;

        private string _sceneNameToCheckOnUnload;
        private Action _actOnCheckOnUnload;

        private bool _isSceneLoaded;
        private bool _isSceneUnloaded;
        private Action _actOnSwap;

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

        public void ActOnSceneLoaded(string scene, Action onComplete)
        {
            ActOnSceneEvent(true, scene, onComplete);
        }

        /*public void ActOnSceneUnloaded(string scene, Action onComplete)
        {
            ActOnSceneEvent(false, scene, onComplete);
        }*/

        private void ActOnSceneEvent(bool onLoad, string scene, Action onComplete)
        {
            if (onLoad)
            {
                _sceneNameToCheckOnLoad = scene;
                _actOnCheckOnLoad = onComplete;
                SceneManager.sceneLoaded += ActOnSceneLoaded;
            }
            else
            {
                _sceneNameToCheckOnUnload = scene;
                _actOnCheckOnUnload = onComplete;
                SceneManager.sceneUnloaded += ActOnSceneUnloaded;
            }
        }
        private void ActOnSceneLoaded(Scene s, LoadSceneMode m)
        {
            if (s.name.Equals(_sceneNameToCheckOnLoad) && m.Equals(LoadSceneMode.Additive) )
            {
                SceneManager.sceneLoaded -= ActOnSceneLoaded;
                _actOnCheckOnLoad();
            }
        }
        private void ActOnSceneUnloaded(Scene s)
        {
            if (s.name.Equals(_sceneNameToCheckOnUnload))
            {
                SceneManager.sceneUnloaded -= ActOnSceneUnloaded;
                _actOnCheckOnUnload();
            }
        }
        public void ActOnSceneSwaped(string unloadScene, string loadScene, Action onSwap)
        {
            ActOnSceneEvent(false, unloadScene, () => { this._isSceneUnloaded = true; });
            ActOnSceneEvent(true, loadScene, () => { this._isSceneLoaded = true; });
            this._actOnSwap = onSwap;
        }

        private void ActivatorAndUnloader(Scene s, LoadSceneMode m)
        {
            SceneManager.sceneLoaded -= ActivatorAndUnloader;
            SceneManager.SetActiveScene(s);
            SceneManager.UnloadSceneAsync(_sceneToUnload);
        }

        private void Update()
        {
            if(_isSceneLoaded && _isSceneUnloaded)
            {
                this._actOnSwap();
                this._isSceneUnloaded = this._isSceneLoaded = false;
                this._actOnSwap = null;
            }
        }
    }
}

