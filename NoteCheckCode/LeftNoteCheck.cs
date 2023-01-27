using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteCheck : MonoBehaviour
{
    protected static NoteManager noteManager;

    private void Start()
    {
        noteManager = GameObject.Find("Node").GetComponent<NoteManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!noteManager.NotePlay())
        {
            if (collision.CompareTag("LeftNote"))
            {
                LeftNote temp = collision.GetComponent<LeftNote>();
                if (temp.isChecked == false)
                    temp.isChecked = true;
                NoteNoteIn();
            }
            else if(collision.CompareTag("LeftLongNote"))
            {
                LeftLongNote temp = collision.GetComponent<LeftLongNote>();
                if (temp.isChecked == false)
                    temp.isChecked = true;
                NoteNoteIn();
            }
        }
    }

    private void NoteNoteIn()
    {
        noteManager.LeftNoteIn();
    }

    private void NoteNoteOut()
    {
        noteManager.LeftNoteOut();
    }
}
