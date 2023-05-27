using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameHelper
{
    public class SceneLoadingHelper : MonoBehaviour
    {
        //Dynamic fields
        private Scene _sceneToUnload;
        //marked loaded scene and its action
        private string _sceneNameToCheckOnLoad;
        private Action _actOnCheckOnLoad;
        //marked unloaded scene and its action
        private string _sceneNameToCheckOnUnload;
        private Action _actOnCheckOnUnload;
        //loaded and unloaded mark and on swap action.
        private bool _isSceneLoaded;
        private bool _isSceneUnloaded;
        private Action _actOnSwap;

        #region Unload scene
        public void UnloadScene(Scene scene)
        {
            _sceneToUnload = scene;
            if (_sceneToUnload.IsValid()) SceneManager.sceneLoaded += ActivatorAndUnloader;
            else throw new System.Exception("Scene: " + scene.name + "cann't be unloaded; Due to scene is invalid");
        }
        /// <summary>
        /// Unload given scene name asyncially
        /// </summary>
        /// <param name="scene">name of the scene to be unloaded</param>
        public void UnloadScene(string scene) 
        {
            UnloadScene(SceneManager.GetSceneByName(scene));
        }
        private void ActOnSceneUnloaded(Scene s)
        {
            if (s.name.Equals(_sceneNameToCheckOnUnload))
            {
                SceneManager.sceneUnloaded -= ActOnSceneUnloaded;
                _actOnCheckOnUnload();
            }
        }
        #endregion

        #region Load scene
        /// <summary>
        /// Load scene of given name asycially
        /// </summary>
        /// <param name="scene">name of the name to be loaded</param>
        /// <exception cref="System.Exception"></exception>
        public void LoadSceneAdditively(string scene)
        {
            if (!SceneManager.GetSceneByName(scene).IsValid()) SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            else throw new System.Exception(string.Format("Scene({0}) already be loaded;", scene));
        }
        /// <summary>
        /// Unload and load scene accordingly from the given name
        /// </summary>
        /// <param name="toUnload">name of the scene to be unloaded</param>
        /// <param name="toLoad">name of the scene to be loaded</param>
        /// <exception cref="System.Exception">threw due to compication</exception>
        public void SwapScene(string toUnload, string toLoad)
        {
            try
            {
                this.UnloadScene(toUnload);
                this.LoadSceneAdditively(toLoad);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format("Cann't swap scenes due to: {0}", ex.Message));
            }
        }
        /// <summary>
        /// Enfroce given action if given scene loaded
        /// </summary>
        /// <param name="scene">name of the scene to check on loaded</param>
        /// <param name="onComplete">action to act</param>
        public void ActOnSceneLoaded(string scene, Action onComplete)
        {
            ActOnSceneEvent(true, scene, onComplete);
        }
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
        /// <summary>
        /// Enfroce given action if given scenes detect loaded and unloaded respectivelly
        /// </summary>
        /// <param name="unloadScene">name of the scene unloaded</param>
        /// <param name="loadScene">name of the scene loaded</param>
        /// <param name="onSwap">action to be act</param>
        public void ActOnSceneSwaped(string unloadScene, string loadScene, Action onSwap)
        {
            ActOnSceneEvent(false, unloadScene, () => { this._isSceneUnloaded = true; });
            ActOnSceneEvent(true, loadScene, () => { this._isSceneLoaded = true; });
            this._actOnSwap = onSwap;
        }
        #endregion

        #region Misc Function
        private void ActivatorAndUnloader(Scene s, LoadSceneMode m)
        {
            SceneManager.sceneLoaded -= ActivatorAndUnloader;
            SceneManager.SetActiveScene(s);
            SceneManager.UnloadSceneAsync(_sceneToUnload);
        }
        #endregion

        private void Update()
        {
            //if marked scenes detected loaded and unload respectively act as require.
            if(_isSceneLoaded && _isSceneUnloaded)
            {
                this._actOnSwap();
                this._isSceneUnloaded = this._isSceneLoaded = false;
                this._actOnSwap = null;
            }
        }
    }
}

