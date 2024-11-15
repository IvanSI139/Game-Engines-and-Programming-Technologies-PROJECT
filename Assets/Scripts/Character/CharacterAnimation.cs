using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : ICharacterAnimation
{
    private Animator _animator;
       public CharacterAnimation(Animator animator)
       {
           _animator = animator;
       }
   
       public void UpdateAnimation(float horizontalInput, bool _isJumping)
       {
           bool isIdle = horizontalInput == 0 && !_isJumping;
           bool isRunning = horizontalInput != 0 && !_isJumping; // _isJumping durumu burada kontrol edilmez
           bool isJumping = _isJumping; // Zıplama durumu
           _animator.SetBool("IsIdle", isIdle);
           _animator.SetBool("IsRun", isRunning);
           _animator.SetBool("IsJump", isJumping);
       }
   }
