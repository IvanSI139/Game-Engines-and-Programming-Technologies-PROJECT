using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private ICharacterMovement _movement;
        private ICharacterAnimation _animation;
        
        public bool _isGrounded;
        public float _speed;
        public float jumpForce = 5f; // Zıplama kuvveti
        public LayerMask groundLayer; // Zemin katmanı
        public Transform groundCheck; // Zemin kontrolü için boş nesne
    private bool _isJumping = false;
    public bool resetJumping = false;
    public Vector3 characterOriginalScale = Vector3.one;
        private void Awake()
        {
            _movement = new CharacterMovement(transform) as ICharacterMovement;
            _animation = new CharacterAnimation(GetComponent<Animator>()) as ICharacterAnimation;
        }
    
        private void Update()
        {
            HandleInput();
            CheckGroundStatus();
        }

        private void HandleInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");

            // Zıplama yapıldığında ve karakter yerdeyse
            if (jump && _isGrounded)
            {
                _movement.Jump(jumpForce);
                _isJumping = true; // Zıplama durumu aktif
                _isGrounded = false; // Zıplama anında yere değmiyor
                resetJumping = true;
                StartCoroutine(ResetJumping());
            }

            // Yere inildiğinde zıplama durumu pasif olmalı
            if (_isGrounded && _isJumping)
            {
                _isJumping = false; // Yere inişte zıplama durumu pasif
            }

            _movement.Move(horizontal,_speed);
            _animation.UpdateAnimation(horizontal, _isJumping);
        }

        private IEnumerator ResetJumping()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            resetJumping = false;
        }
        private void CheckGroundStatus()
        {
            if(resetJumping == false)
                _isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
            Debug.DrawRay(groundCheck.position, Vector2.down * 0.1f, Color.red);
        }
}