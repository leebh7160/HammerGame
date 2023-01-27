using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;


//참조 : https://geojun.tistory.com/65
class SaveData
{
    internal string RecordAudioName;

    //=========================================저장 데이터
    [SerializeField] internal List<double> RecordLongNoteStart_Right;
    [SerializeField] internal List<double> RecordLongNoteEnd_Right;
    [SerializeField] internal List<double> RecordShortNote_Right;
                     
    [SerializeField] internal List<double> RecordLongNoteStart_Left;
    [SerializeField] internal List<double> RecordLongNoteEnd_Left;
    [SerializeField] internal List<double> RecordShortNote_Left;
    //=========================================저장 데이터^^

    public SaveData()
    {
        RecordLongNoteStart_Right = new List<double>();
        RecordLongNoteEnd_Right = new List<double>();
        RecordShortNote_Right = new List<double>();

        RecordLongNoteStart_Left = new List<double>();
        RecordLongNoteEnd_Left = new List<double>();
        RecordShortNote_Left = new List<double>();
    }
}

public class MusicRecord : MonoBehaviour
{
    public bool LoadComplete = false;

    private SaveData saveData;
    private AudioSource RecordAudio;
    private bool RecordData = false;
    

    private string PCpath;
    private string IOpath;


    private void Awake()
    {
        saveData = new SaveData();
        //PCpath = Path.Combine(Application.persistentDataPath + "/database/", "database.json");
        PCpath = $"{Application.streamingAssetsPath}/database.json";
        IOpath = Path.Combine(Application.persistentDataPath, "database.json");
    }

    /*private void PathFinder()
    {
        string tmp = $"{Application.dataPath}/database.json";

        if(Directory.Exists(tmp))
        {
            DirectoryInfo dinfo = new DirectoryInfo(tmp);

        }

    }*/

    private void Start()
    {
    }

    private void Update()
    {
        if (RecordData == false)
            return;
        
        //음악 끝나면 자동으로 저장
        //세이브 데이터 버튼을 따로 만들 필요가 있다.
        if (RecordAudio.isPlaying == false) 
        {
            RecordData = false;
            MusicSaveJson();
            return;
        }
        double t_audio = (RecordAudio.time * 1000) + 1450;

        if (Input.GetKeyDown(KeyCode.D))
        {
            saveData.RecordLongNoteStart_Right.Add(t_audio);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            saveData.RecordLongNoteEnd_Right.Add(t_audio);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            saveData.RecordLongNoteStart_Left.Add(t_audio);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            saveData.RecordLongNoteEnd_Left.Add(t_audio);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow)) //Right
        {
            saveData.RecordShortNote_Right.Add(t_audio);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //Left
        {
            saveData.RecordShortNote_Left.Add(t_audio);
        }

    }

    internal void MusicRecordStart(bool recordcheck, AudioSource playaudio)
    {
        RecordAudio = playaudio;
        RecordData = recordcheck;
        RecordAudio.Play();
    }


    #region /////////////////////////////////////JsonSave
    private void MusicSaveJson()
    {
        SaveAudioName();

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(PCpath, json);
        Debug.Log("저장 완료");
    }

    private void SaveAudioName()
    {
        saveData.RecordAudioName = RecordAudio.name;
    }
    #endregion //////////////////////////////////JsonSave

    #region /////////////////////////////////////JsonLoad
    internal SaveData MusicLoadJson()
    {
        SaveData t_savedata = new SaveData();

        if(!File.Exists(PCpath))
        {
            Debug.Log("에러 확인");
        }
        else
        {
            string loadJson = File.ReadAllText(PCpath);
            t_savedata = JsonUtility.FromJson<SaveData>(loadJson);

            if(t_savedata != null)
            {
                saveData.RecordLongNoteStart_Right.AddRange(t_savedata.RecordLongNoteStart_Right);
                saveData.RecordLongNoteEnd_Right.AddRange(t_savedata.RecordLongNoteEnd_Right);
                saveData.RecordShortNote_Right.AddRange(t_savedata.RecordShortNote_Right);
                saveData.RecordLongNoteStart_Left.AddRange(t_savedata.RecordLongNoteStart_Left);
                saveData.RecordLongNoteEnd_Left.AddRange(t_savedata.RecordLongNoteEnd_Left);
                saveData.RecordShortNote_Left.AddRange(t_savedata.RecordShortNote_Left);
                LoadComplete = true;
            }
        }

        return saveData;
    }

    #endregion //////////////////////////////////JsonLoad

}
