using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private static readonly int IdleSpeed = Animator.StringToHash("IdleSpeed");
    private static readonly int RunSpeed = Animator.StringToHash("RunSpeed");
    private static readonly int JumpSpeed = Animator.StringToHash("JumpSpeed");
    public CharacterController2D character;
    public Animator characterAnimator;
    public AnimationClip clip;
    public Slider characterScaleSlider;
    public Slider idleSpeedSlider;
    public Slider jumpSpeedSlider;
    public Slider runSpeedSlider;
    public TMP_Text characterScaleText;
    public TMP_Text idleSpeedText;
    public TMP_Text jumpSpeedText;
    public TMP_Text runSpeedText;


    public void SetCharacterScale(float size)
    {
        
        character.transform.localScale = new Vector3(size, size, size);
        character.characterOriginalScale = new Vector3(size*Math.Sign(character.characterOriginalScale.x), size, size);
        characterScaleText.text = size.ToString("F2");
    }

    public void SetIdleSpeed(float value)
    {
        characterAnimator.SetFloat(IdleSpeed,value);
        idleSpeedText.text = value.ToString("F2");
    }
    public void SetRunSpeed(float value)
    {
        characterAnimator.SetFloat(RunSpeed,value);
        runSpeedText.text = value.ToString("F2");
    }
    public void SetJumpSpeed(float value)
    {
        characterAnimator.SetFloat(JumpSpeed,value);
        jumpSpeedText.text = value.ToString("F2");
    }
}
