using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void InteractionHandler(PuzzleMaster pm);

public class PlInterection : MonoBehaviour
{
    private PuzzleMaster _interectedPM;

    public event InteractionHandler InteractionCalled;

    private bool _interactionCall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "I-Point")
        {
            _interectedPM = collision.gameObject.GetComponent<PuzzleMaster>();
            if (_interectedPM != null)
            {
                if (_interectedPM.Dialog == null) Debug.Log("Current PM have no dialogs");
                Debug.Log("Puzzle master received!");
            }
        }
    }

    private void InputUpdate()
    {
        _interactionCall = Input.GetButtonDown("Interact");
    }

    private void InputEnforcer()
    {
        if (_interactionCall)
        {
            Debug.Log("Interacted!!");
            _interactionCall = false;
            if(_interectedPM != null) PassPM();
        }
    }

    public virtual void PassPM()
    {
        InteractionCalled?.Invoke(_interectedPM);
    }

    // Update is called once per frame
    void Update()
    {
        InputUpdate();

        InputEnforcer();
    }
}
