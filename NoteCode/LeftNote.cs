using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeftNote : MonoBehaviour
{
    public bool isChecked = false;
    private float noteSpeed = 5f;
    
    private float noteintervel = 200f;
    private UnityEngine.UI.Image noteImage;

    private void Start()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(LeftNoteMoveCorutine());
    }

    internal void IntervalData(float interval)
    {
        noteintervel = interval;
    }

    internal void HideNote()
    {
        noteImage.enabled = false;
        isChecked = true;
    }

    private IEnumerator LeftNoteMoveCorutine()
    {
        while (true)
        {
            if (this.transform.localPosition.x <= -1450)
                noteImage.enabled = false;
            else
                noteImage.enabled = true;

            //this.transform.localPosition -= Vector3.left * noteSpeed * Time.deltaTime;
            this.transform.localPosition -= Vector3.left * noteSpeed * Time.deltaTime * noteintervel;
            yield return null;
        }
    }
}
