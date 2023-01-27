using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Animator LeftnoteHitAnimation = null;
    [SerializeField] private Animator RightnoteHitAnimation = null;
    string hit = "Hit";

    internal void LeftNoteHitEffect()
    {
        LeftnoteHitAnimation.SetTrigger(hit);
    }
    internal void RightNoteHitEffect()
    {
        RightnoteHitAnimation.SetTrigger(hit);
    }

}
