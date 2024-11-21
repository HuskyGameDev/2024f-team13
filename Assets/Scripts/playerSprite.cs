using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSprite : MonoBehaviour
{
   public  Animator animator;
   public int look_angle_offset;

   private Vector2 aim, prevMousePosition;
   private float angle;
   private bool inW ,inA, inS, inD;
   private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
   void Update() { 
      inW = Input.GetKey(KeyCode.W);
      inA = Input.GetKey(KeyCode.A);
      inS = Input.GetKey(KeyCode.S);
      inD = Input.GetKey(KeyCode.D);

      aim = Input.mousePosition;
      Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
      if (inW || inA || inS || inD) {
         angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0, 0, angle + look_angle_offset);

      } else if (prevMousePosition != aim){ 
         //Only aim at mouse if mouse moved since last frame other wise keep current rotation
         angle = Mathf.Atan2((aim.y - object_pos.y), (aim.x - object_pos.x)) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0, 0, angle + look_angle_offset);
      }
      prevMousePosition = aim;

      //WALK ANIMATION
      if (rb.velocity != Vector2.zero) { 
         animator.SetBool("walking", true);
      } else {
         animator.SetBool("walking", false); 
      }

   }
}
