using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //==========================Record
    [SerializeField] private bool RecordCheck = false;

    private List<double> RightMusicRecordTime = new List<double>();
    private List<double> RightLongNoteStartTime = new List<double>();
    private List<double> RightLongNoteEndTime = new List<double>();

    private List<double> LeftMusicRecordTime = new List<double>();
    private List<double> LeftLongNoteStartTime = new List<double>();
    private List<double> LeftLongNoteEndTime = new List<double>();
    //==========================Record


    //==========================RightBPMValue
    private double currentTime_right = 0d;
    //==========================RightBPMValue^^


    //==========================LeftBPMValue
    private double currentTime_left = 0d;
    //==========================LeftBPMValue^^


    //==========================AudioValue
    private bool isMusicPlay = false;
    private AudioSource myAudio;
    //==========================AudioValue^^


    //==========================NoteCheckValue
    [SerializeField] Transform RightNoteAppear = null;
    [SerializeField] Transform LeftNoteAppear = null;

    [SerializeField] GameObject RightNote = null;
    [SerializeField] GameObject LeftNote = null;
    
    [SerializeField] GameObject RightLongNote = null;
    [SerializeField] GameObject LeftLongNote = null;
    //==========================NoteCheckValue^^

    //==========================Reference
    private TimingManager timingManager;
    //==========================Reference^^

    //==========================MusicRecord
    [SerializeField]  private MusicRecord musicrecord;
    //==========================MusicRecord^^


    private void Start()
    {
        myAudio         = this.gameObject.GetComponent<AudioSource>();
        timingManager   = this.gameObject.GetComponent<TimingManager>();

        /*RightMusicRecordTime = new List<double>();
        RightMusicLongNoteTime = new List<Vector2>();

        LeftMusicRecordTime = new List<double>();
        LeftMusicLongNoteTime = new List<Vector2>();*/
        timingManager.SetNoteParentPosition(LeftNoteAppear.localPosition, RightNoteAppear.localPosition);


        if (RecordCheck == true)
        {
            musicrecord.gameObject.SetActive(true);
            musicrecord.MusicRecordStart(true, myAudio);
        }
        else
        {
            musicrecord.gameObject.SetActive(true);
            GamePlay();
        }
    }

    private void GamePlay()//저장한 노트를 생성하는 위치
    {
        SaveData savedata = musicrecord.MusicLoadJson();
        if (musicrecord.LoadComplete == true)
        {
            StartCoroutine(NoteCreate(savedata));
            StartCoroutine(LongNoteCreate(savedata));
            musicrecord.gameObject.SetActive(false);
        }
    }

    #region ///////////////////////////////노트 제작
    private IEnumerator NoteCreate(SaveData t_savedata)
    {
        RightMusicRecordTime.AddRange(t_savedata.RecordShortNote_Right);
        LeftMusicRecordTime.AddRange(t_savedata.RecordShortNote_Left);
        int right_t_record = 0;
        int Left_t_record = 0;

        for (int i = 0; i < RightMusicRecordTime.Count; i++)
        {
            GameObject t_note_Right = 
                Instantiate(RightNote, new Vector2(RightNoteAppear.position.x + (float)RightMusicRecordTime[i], RightNoteAppear.position.y), Quaternion.identity, RightNoteAppear);

            timingManager.Right_NoteList.Add(t_note_Right);
            right_t_record++;
            yield return null;
        }

        for (int j = 0; j < LeftMusicRecordTime.Count; j++)
        {
            GameObject t_note_Left =
                Instantiate(LeftNote, new Vector2(LeftNoteAppear.position.x - (float)LeftMusicRecordTime[j], LeftNoteAppear.position.y), Quaternion.identity, LeftNoteAppear);

            timingManager.Left_NoteList.Add(t_note_Left);
            Left_t_record++;
            yield return null;
        }

        yield return null;
    }

    private IEnumerator LongNoteCreate(SaveData t_savedata)
    {
        RightLongNoteStartTime.AddRange(t_savedata.RecordLongNoteStart_Right);
        RightLongNoteEndTime.AddRange(t_savedata.RecordLongNoteEnd_Right);

        LeftLongNoteStartTime.AddRange(t_savedata.RecordLongNoteStart_Left);
        LeftLongNoteEndTime.AddRange(t_savedata.RecordLongNoteEnd_Left);

        int right_t_record = 0;
        int left_t_record = 0;
        float sizedeltaX = 0;

        for(int i = 0; i < RightLongNoteStartTime.Count; i++)
        {
            GameObject t_LongNote_Right =
                Instantiate(RightLongNote,
                RightLongNotePosition((float)RightLongNoteStartTime[i],(float)RightLongNoteEndTime[i] , RightNoteAppear),
                Quaternion.identity, RightNoteAppear);

            sizedeltaX = RightLongNoteSize((float)RightLongNoteStartTime[i], (float)RightLongNoteEndTime[i]);

            t_LongNote_Right.GetComponent<RectTransform>().sizeDelta = new Vector2(sizedeltaX, 100);
            t_LongNote_Right.GetComponent<RectTransform>().pivot = new Vector2(0.05f, 0.5f);

            RightLongNote right_long_note = t_LongNote_Right.GetComponent<RightLongNote>();
            right_long_note.intervalData(100f);

            timingManager.Right_LongNoteList.Add(t_LongNote_Right);
            right_t_record++;
            yield return null;
        }

        for (int i = 0; i < LeftLongNoteStartTime.Count; i++)
        {
            GameObject t_LongNote_Left =
                Instantiate(LeftLongNote,
                LeftLongNotePosition(-(float)LeftLongNoteStartTime[i], -(float)LeftLongNoteEndTime[i], LeftNoteAppear),
                Quaternion.identity, LeftNoteAppear);

            sizedeltaX = LeftLongNoteSize((float)LeftLongNoteStartTime[i], (float)LeftLongNoteEndTime[i]);

            t_LongNote_Left.GetComponent<RectTransform>().sizeDelta = new Vector2(sizedeltaX, 100);
            t_LongNote_Left.GetComponent<RectTransform>().pivot = new Vector2(0.95f, 0.5f);

            LeftLongNote Left_long_note = t_LongNote_Left.GetComponent<LeftLongNote>();
            Left_long_note.intervalData(100f);

            timingManager.Left_LongNoteList.Add(t_LongNote_Left);
            left_t_record++;
            yield return null;
        }
    }

    private Vector2 RightLongNotePosition(float starttime, float endtime, Transform appear)
    {
        Vector2 returnVector;
        float notepositionX;

        notepositionX = ((endtime - starttime) / 2) + starttime;

        returnVector = new Vector2(notepositionX + appear.position.x, appear.position.y);

        return returnVector;
    }

    private float RightLongNoteSize(float starttime, float endtime)
    {
        float notesize;

        notesize = (endtime - starttime);

        return notesize;
    }

    private Vector2 LeftLongNotePosition(float starttime, float endtime, Transform appear)
    {
        Vector2 returnVector;
        float notepositionX;

        notepositionX = ((endtime - starttime) / 2) + starttime;

        returnVector = new Vector2(notepositionX - appear.position.x, appear.position.y);

        return returnVector;
    }

    private float LeftLongNoteSize(float starttime, float endtime)
    {
        float notesize;

        notesize = (endtime - starttime);

        return notesize;
    }


    #endregion /////////////////////////////// 노트 제작

    #region //////////////////////노트 없어지는 것 확인
    internal void LeftNoteDestroy(GameObject leftnoteobj)
    {
        timingManager.Left_NoteList.Remove(leftnoteobj);
    }
    internal void LeftLongNoteDestroy(GameObject rightnoteobj)
    {
        timingManager.LeftCheckNoteDelete_Long();
        timingManager.Left_LongNoteList.Remove(rightnoteobj);
    }

    internal void RightNoteDestroy(GameObject rightnoteobj)
    {
        timingManager.Right_NoteList.Remove(rightnoteobj);
    }

    internal void RightLongNoteDestroy(GameObject rightnoteobj)
    {
        timingManager.RightCheckNoteDelete_Long();
        timingManager.Right_LongNoteList.Remove(rightnoteobj);
    }

    #endregion//////////////////////노트 없어지는 것 확인

    #region 노트로 노래 시작
    internal bool NotePlay()
    {
        return isMusicPlay;
    }

    internal void LeftNoteIn()
    {
        myAudio.Play();
        isMusicPlay = true;
        timingManager.LeftCheckMissingCheck();
    }

    internal void RightNoteIn()
    {
        myAudio.Play();
        isMusicPlay = true;
        timingManager.RightCheckMissingCheck();
    }

    internal void LeftNoteOut()
    {
        isMusicPlay = false;
    }

    internal void RightNoteOut()
    {
        isMusicPlay = false;
    }

    #endregion
}
