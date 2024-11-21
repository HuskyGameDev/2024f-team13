using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
   public enemyType type;
   public Animator animator;
   public  int look_angle_offset;
   [SerializeField] GameObject player;

   private Rigidbody2D rb2D;
   private float moveSpeed, angle, sightRange; //distance
   private int damage;
   int animLayer = 0;

   public enum enemyType {
      small,
      medium,
      large
   }

   bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

   void Start(){
      rb = GetComponent<Rigidbody2D>();
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

    void Awake()
    {
    }

    // TODO Make movement stop if stuck 
    // Update is called once per frame
    void Update() {
        // Move towards player 
        distance = Vector2.Distance(player.transform.position, transform.position);

        switch (type) {
            case enemyType.large:
                if (distance < 10) {
                    if (distance < 2) {
                        animator.SetTrigger("Attack");

                    } else {
                        if(!isPlaying(animator, "Medium_hide")) {
                            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                            transform.up = player.transform.position - transform.position;
                        }
                        animator.ResetTrigger("Attack");
                    }
                }
               break;
            case enemyType.medium:

                if (distance < 7)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                    transform.up = player.transform.position - transform.position;

                    if (distance < 1.5) {
                        animator.SetTrigger("Attack");
                        animator.SetTrigger("Hide");

                    } else {
                        animator.ResetTrigger("Attack");
                    }
                }

                break;
            case enemyType.small:
                if (distance < 10)
                {
                    animator.SetBool("PlayerDetected", true);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                    transform.up = player.transform.position - transform.position;
                    
                } else
                {
                    animator.SetBool("PlayerDetected", false);
                }
                break;
        }

      /** Calculate the direction to the player
      Vector3 direction = WalkScript.playerPos - transform.position;

      if (direction.magnitude < sightRange) {
         // Normalize becuase I don't know lin alg that well 
         direction.Normalize();

         // Move the enemy towards the player
         transform.position += direction * moveSpeed * Time.deltaTime;

         // Calculate the angle between the enemy and the player 
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

         // Apply the rotation 
         transform.rotation = Quaternion.AngleAxis(angle + look_angle_offset, Vector3.forward);
      }
      */
   }
   
}
