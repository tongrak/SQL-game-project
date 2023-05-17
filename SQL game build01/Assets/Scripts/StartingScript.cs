using GameHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScript : MonoBehaviour
{
    //A list of scene to load before the room is self. Like Consoles, Player?, Music?, other.
    [SerializeField] private string[] _passiveGameplayScenes;

    private SceneLoadingHelper _SLH;

    private GameObject _mastersObj;
    private string _startingScene;

    private void Awake()
    {
        _mastersObj = GameObject.Find("Masters");
        _startingScene = this.gameObject.scene.name;

        _SLH = FindAnyObjectByType<SceneLoadingHelper>();

        //Put Masters object to DontDestroyOnLoad Scene.
        if (_mastersObj != null) DontDestroyOnLoad(_mastersObj);
        else throw new MissingComponentException("Masters Object not detected");

        //Loading every passive gameplay scenes.
        foreach (string s in _passiveGameplayScenes)
        {
            try
            {
                Debug.Log("Trying to load scene: " + s);
                _SLH.LoadSceneAdditively(s);
            }
            catch (System.Exception e) { Debug.LogWarning("Starting scene err: " + e.Message); }
        }
    }

    void Start()
    {
        _SLH.UnloadScene(_startingScene);
    }
    
    
}
