using UnityEngine;

public class RightNoteOutCheck : RightNoteCheck
{
    private void OnTriggerExit2D(Collider2D collision)//풀링으로 변경 할 것
    {
        if (collision.CompareTag("RightNote"))
        {
            noteManager.RightNoteDestroy(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("RightLongNote"))
        {
            noteManager.RightLongNoteDestroy(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
