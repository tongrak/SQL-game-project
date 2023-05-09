using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{
    //A list of scene to load before the room is self. Like Consoles, Player?, Music?, other.
    [SerializeField] private string[] _passiveGameplayScenes;

    private GameObject _mastersObj;
    private string _startingScene;

    private void Awake()
    {
        _mastersObj = GameObject.Find("Masters");
        _startingScene = this.gameObject.scene.name;

        //Put Masters object to DontDestroyOnLoad Scene.
        if (_mastersObj != null) DontDestroyOnLoad(_mastersObj);
        else throw new MissingComponentException("Masters Object not detected");

        //Loading every passive gameplay scenes.
        foreach (var s in _passiveGameplayScenes)
        {
            try
            {
                SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
            }
            catch (System.Exception e) { Debug.LogWarning("Starting scene err: " + e); }
        }
    }

    void Start()
    {
        //Unloading starting scene
        SceneManager.sceneLoaded += ActivatorAndUnloader;
    }
    
    //Still do not know how it work...
    void ActivatorAndUnloader(Scene s, LoadSceneMode m)
    {
        SceneManager.sceneLoaded -= ActivatorAndUnloader;
        SceneManager.SetActiveScene(s);
        SceneManager.UnloadSceneAsync(_startingScene);
    }
}
