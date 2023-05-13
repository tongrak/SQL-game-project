using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlInterection : MonoBehaviour
{
    private PuzzleMaster _interectedPM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Collision");
        if (collision.gameObject.tag == "I-Point")
        {
            _interectedPM = collision.gameObject.GetComponent<PuzzleMaster>();
            if (_interectedPM != null) Debug.Log("Puzzle master received!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
