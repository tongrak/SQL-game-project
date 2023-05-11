using ConsoleGenerals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMaster : MonoBehaviour
{
    //Player's controller
    private PlMovement _movementControl;
    private PlInterection _interactionControl;
    //Gameplay related
    private ConsolesMaster _consolesControl;
    private PuzzleMaster _currPM;



    // Start is called before the first frame update
    void Start()
    {
        _movementControl = GameObject.FindAnyObjectByType<PlMovement>();
        _interactionControl = GameObject.FindAnyObjectByType<PlInterection>();
        _consolesControl = GameObject.FindAnyObjectByType<ConsolesMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
