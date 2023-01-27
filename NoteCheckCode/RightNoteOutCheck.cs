using UnityEngine;

public class RightNoteOutCheck : RightNoteCheck
{
    private void OnTriggerExit2D(Collider2D collision)//Ǯ������ ���� �� ��
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
