using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightNoteCheck : MonoBehaviour
{
    protected static NoteManager noteManager;

    void Start()
    {
        noteManager = GameObject.Find("Node").GetComponent<NoteManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!noteManager.NotePlay())
        {
            if (collision.CompareTag("RightNote"))
            {
                RightNote temp = collision.GetComponent<RightNote>();
                if (temp.isChecked == false)
                    temp.isChecked = true;
                NoteNoteIn();
            }
            else if(collision.CompareTag("RightLongNote"))
            {
                RightLongNote temp = collision.GetComponent<RightLongNote>();
                if (temp.isChecked == false)
                    temp.isChecked = true;
                NoteNoteIn();
            }
        }
    }

    private void NoteNoteIn()
    {
        noteManager.RightNoteIn();
    }

    private void NoteNoteOut()
    {
        noteManager.RightNoteOut();
    }

}
