using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLongNote : MonoBehaviour
{
    public bool isChecked = false;
    private float noteSpeed = 5f;

    private float noteinterval = 200f;
    private UnityEngine.UI.Image noteImage;

    private RectTransform rect;

    //Collider offset x 값 왼쪽 끝 = - ((최대값 - 30) / 2)

    private void Start()
    {
        rect        = this.GetComponent<RectTransform>();
        noteImage   = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(RightLongNoteMoveCorutine());
    }

    internal void intervalData(float interval)
    {
        noteinterval = interval;
    }

    internal void HideNote()
    {
        //noteImage.enabled = false;
        isChecked = true;
    }


    private IEnumerator RightLongNoteMoveCorutine()
    {
        Vector3 moveroutine;
        while (true)
        {
            moveroutine = Vector2.right * noteSpeed * noteinterval * Time.deltaTime;

            if (isChecked == false)
                this.transform.localPosition -= moveroutine;
            if (isChecked == true)
            {
                if (rect.sizeDelta.x >= 0)
                    rect.sizeDelta -= new Vector2(moveroutine.x,0);
                else if (rect.sizeDelta.x <= 0)
                    this.transform.localPosition -= moveroutine;
            }
            yield return null;
        }
    }
}
