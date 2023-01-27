using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TimingType
{
    Perfect,
    Nice,
    Good,
    Miss,
}


public class TimingManager : MonoBehaviour
{
    //===========================================Left
    internal List<GameObject> Left_NoteList  = new List<GameObject>();
    internal List<GameObject> Left_LongNoteList = new List<GameObject>();
    [SerializeField] private Transform Left_Center = null;
    [SerializeField] private RectTransform[] Left_TimingRect = null;
    private Vector2[] Left_TimingBox = null;
    private Vector2 Left_NoteParentPosition;
    private int Left_NoteNum = 0;
    private int Left_LongNoteNum = 0;
    //===========================================Left^^


    //===========================================Right
    internal List<GameObject> Right_NoteList = new List<GameObject>();
    internal List<GameObject> Right_LongNoteList = new List<GameObject>();
    [SerializeField] private Transform Right_Center = null;
    [SerializeField] private RectTransform[] Right_TimingRect = null;
    private Vector2[] Right_TimingBox = null;
    private Vector2 Right_NoteParentPosition;
    private int Right_NoteNum = 0;
    private int Right_LongNoteNum = 0;
    //===========================================Right^^

    //===========================================Effect
    EffectManager effectManager;
    //===========================================Effect^^

    private void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        LeftInit();
        RightInit();
    }


    internal void SetNoteParentPosition(Vector2 leftposition, Vector2 rightposition)//NoteManager에서 위치를 줌...
    {
        Left_NoteParentPosition = leftposition;
        Right_NoteParentPosition = rightposition;
    }

    #region ////////////////// Left
    private void LeftInit()
    {
        Left_TimingBox = new Vector2[Left_TimingRect.Length];

        for (int i = 0; i < Left_TimingRect.Length; i++)
        {
            Left_TimingBox[i].Set(Left_Center.localPosition.x - Left_TimingRect[i].rect.width / 2,
                                  Left_Center.localPosition.x + Left_TimingRect[i].rect.width / 2);
        }
    }

    internal void LeftCheckTiming()
    {
        if (Left_NoteList.Count <= 0)
            return;

        /*if (Left_NoteNum >= Left_NoteList.Count)
            return;*/



        for (int f_notenum = 0; f_notenum < Left_NoteList.Count; f_notenum++)
        {
            float t_notePosX = Left_NoteList[f_notenum].transform.localPosition.x + Left_NoteParentPosition.x;

            for (int f_checkareanum = 0; f_checkareanum < Left_TimingBox.Length; f_checkareanum++)
            {
                if (Left_TimingBox[f_checkareanum].x <= t_notePosX && t_notePosX <= Left_TimingBox[f_checkareanum].y)
                {
                    if (f_checkareanum < Left_TimingBox.Length - 1)
                        effectManager.LeftNoteHitEffect();
                    Left_NoteList[f_notenum].GetComponent<LeftNote>().HideNote();
                    Left_NoteList.RemoveAt(f_notenum);
                    return;
                }
            }
        }
    }

    internal void LeftCheckMissingCheck()
    {
        Left_NoteNum++;
    }
    internal void LeftCheckTiming_Long()
    {
        if (Left_LongNoteList.Count <= 0)
            return;

        if (Left_NoteNum > Left_LongNoteList.Count)
            return;

        float t_notePosX = Left_LongNoteList[Left_LongNoteNum].transform.localPosition.x + Left_NoteParentPosition.x;

        for (int f_checkareanum = 0; f_checkareanum < Left_TimingBox.Length; f_checkareanum++)
        {
            if (Left_TimingBox[f_checkareanum].x <= t_notePosX && t_notePosX <= Left_TimingBox[f_checkareanum].y)
            {
                if (f_checkareanum < Left_TimingBox.Length - 1)
                    effectManager.LeftNoteHitEffect();
                Left_LongNoteList[Left_LongNoteNum].GetComponent<LeftLongNote>().HideNote();
                return;
            }
        }
    }

    internal void LeftCheckNoteDelete_Long()
    {
        Left_LongNoteList.RemoveAt(Left_LongNoteNum);
        Left_LongNoteNum++;
    }
    #endregion /////////////////////////////// Left

    #region ///////////////////////////// Right
    private void RightInit()
    {
        Right_TimingBox = new Vector2[Right_TimingRect.Length];

        for (int i = 0; i < Right_TimingRect.Length; i++)
        {
            Right_TimingBox[i].Set(Right_Center.localPosition.x - Right_TimingRect[i].rect.width / 2,
                                   Right_Center.localPosition.x + Right_TimingRect[i].rect.width / 2);
        }
    }

    internal void RightCheckTiming_Short() //개선 필요
    {

        if (Right_NoteList.Count <= 0)
            return;

        /*if (Right_NoteNum >= Right_NoteList.Count)
            return;*/



        for (int f_notenum = 0; f_notenum < Right_NoteList.Count; f_notenum++)
        {
            float t_notePosX = Right_NoteList[f_notenum].transform.localPosition.x + Right_NoteParentPosition.x;

            for (int f_checkareanum = 0; f_checkareanum < Right_TimingBox.Length; f_checkareanum++)
            {
                if (Right_TimingBox[f_checkareanum].x <= t_notePosX && t_notePosX <= Right_TimingBox[f_checkareanum].y)
                {
                    if (f_checkareanum < Right_TimingBox.Length - 1)
                        effectManager.RightNoteHitEffect();
                    Right_NoteList[f_notenum].GetComponent<RightNote>().HideNote();
                    Right_NoteList.RemoveAt(f_notenum);
                    return;
                }
            }
        }
    }

    internal void RightCheckMissingCheck()
    {
        Right_NoteNum++;
    }

    internal void RightCheckTiming_Long()
    {
        if (Right_LongNoteList.Count <= 0)
            return;

        if (Right_NoteNum > Right_LongNoteList.Count)
            return;

        float t_notePosX = Right_LongNoteList[Right_LongNoteNum].transform.localPosition.x + Right_NoteParentPosition.x;

        for(int f_checkareanum = 0; f_checkareanum < Right_TimingBox.Length; f_checkareanum++)
        {
            if (Right_TimingBox[f_checkareanum].x <= t_notePosX && t_notePosX <= Right_TimingBox[f_checkareanum].y)
            {
                if (f_checkareanum < Right_TimingBox.Length - 1)
                    effectManager.RightNoteHitEffect();
                Right_LongNoteList[Right_LongNoteNum].GetComponent<RightLongNote>().HideNote();
                return;
            }
        }
    }

    internal void RightCheckNoteDelete_Long()
    {
        Right_LongNoteList.RemoveAt(Right_LongNoteNum);
        Right_LongNoteNum++;
    }

    #endregion ///////////////////////////////Right

}
