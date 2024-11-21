using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
   public enemyType type;
   public Animator animator;
   [SerializeField] GameObject player;

   private Rigidbody2D rb;
   private float moveSpeed, angle, sightRange, rotateSpeed;
   private int damage;
   private int look_angle_offset;
   int animLayer = 0;

   public enum enemyType {
      small,
      medium,
      large
   }

   bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f) {
            return true;
        } else {
            return false;
        }
    }

   void Start(){
      rotateSpeed = 5f;
      rb = GetComponent<Rigidbody2D>();
      rb.mass = 1000;
      look_angle_offset =270;

      switch (type){
         case enemyType.large:
            moveSpeed = 1f;
            damage = 3;
            sightRange = 10f;
            break;
         case enemyType.medium:
            moveSpeed = 1.75f;
            damage = 2;
            sightRange = 7f;
            break;
         case enemyType.small:
         default:
            damage = 1;
            moveSpeed = 2.5f;
            sightRange = 5f;
            break;
      }
   }

   void OnCollisionEnter2D(Collision2D enemyHit){
      Debug.Log("onCollisionEnter2D");
      if (enemyHit.gameObject.CompareTag("Player")){
         Debug.Log("Twas a player");

         if(enemyHit.gameObject.TryGetComponent<healthScript>(out healthScript hp)){
            Debug.Log("hurt the player");
            hp.hurt(damage);
         }
      }
   }


   void move(Vector3 direction){
    if (direction.magnitude < sightRange) {
         // Normalize becuase I don't know lin alg that well 
         direction.Normalize();

         // Move the enemy towards the player
         transform.position += direction * moveSpeed * Time.deltaTime;

         // Calculate the angle between the enemy and the player 
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         
         // Calculate the target rotation
         Quaternion targetRotation = Quaternion.AngleAxis(angle + look_angle_offset, Vector3.forward);
         
         // Gradually rotate from current rotation to target rotation
         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed); 
      }
   }

    // TODO Make movement stop if stuck 
    // Update is called once per frame
    void Update() {
      //Calculate the direction to the player
      Vector3 direction = WalkScript.playerPos - transform.position;

     switch (type) {
         case enemyType.large:
             if (direction.magnitude < sightRange) {
                 if (direction.magnitude < 2) {
                     animator.SetTrigger("Attack");

                 } else {
                     if(!isPlaying(animator, "Medium_hide")) {
                        move(direction);
                     }
                     animator.ResetTrigger("Attack");
                 }
             }
            break;
         case enemyType.medium:
             if (direction.magnitude < sightRange) {
                move(direction);
                 if (direction.magnitude < 1.5) {
                     animator.SetTrigger("Attack");
                     animator.SetTrigger("Hide");

                 } else {
                     animator.ResetTrigger("Attack");
                 }
             }

             break;
         case enemyType.small:
         default:
             if (direction.magnitude < sightRange) {
                 animator.SetBool("PlayerDetected", true); 
                 move(direction);
             } else {
                 animator.SetBool("PlayerDetected", false);
             }
             break;
        }
   }
}
