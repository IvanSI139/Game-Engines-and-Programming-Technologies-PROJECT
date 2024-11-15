using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : ICharacterMovement
{
    private Transform _transform;
       private Rigidbody2D _rigidbody;
       public CharacterController2D CharacterController2D;
       public CharacterMovement(Transform transform)
       {
           _transform = transform;
           _rigidbody = _transform.GetComponent<Rigidbody2D>();
           CharacterController2D = _transform.GetComponent<CharacterController2D>();
       }
   
       public void Move(float horizontalInput,float speed)
       {
           Vector3 movement = new Vector3(horizontalInput * speed, _rigidbody.velocity.y, 0);
           _rigidbody.velocity = movement;
   
           // İlerleme veya geriye hareket etme durumunu kontrol et
           if (horizontalInput != 0)
           {
               float flip = Mathf.Sign(horizontalInput);
               if (flip > 0)
               {
                   CharacterController2D.characterOriginalScale.x = Mathf.Abs(CharacterController2D.characterOriginalScale.x);
               }
               else
               {
                   CharacterController2D.characterOriginalScale.x = -1*(Mathf.Abs(CharacterController2D.characterOriginalScale.x));
               }
               
               _transform.localScale = CharacterController2D.characterOriginalScale  ; // Yüzü döndür
           }
       }
   
       public void Jump(float jumpForce)
       {
           _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
       }
   }