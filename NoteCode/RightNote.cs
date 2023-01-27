using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RightNote : MonoBehaviour
{
    public bool isChecked = false;
    private float noteSpeed = 5f;

    private float noteinterval = 200f;
    private UnityEngine.UI.Image noteImage;

    private void Start()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(RightNoteMoveCorutine());
    }

    internal void IntervalData(float interval)
    {
        noteinterval = interval;
    }

    internal void HideNote()
    {
        noteImage.enabled = false;
        isChecked = true;
    }

    private IEnumerator RightNoteMoveCorutine()
    {
        while (true)
        {
            if (this.transform.localPosition.x >= 1450)
                noteImage.enabled = false;
            else
                noteImage.enabled = true;
            //this.transform.localPosition -= Vector3.right * noteSpeed * noteintervel * Time.deltaTime;
            this.transform.localPosition -= Vector3.right * noteSpeed * noteinterval * Time.deltaTime;
            yield return null;
        }
    }


}
