using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour{

   private Rigidbody2D rb2D;
   private float moveSpeed, angle;
   public  int look_angle_offset;
   public enemyType type;

   public enum enemyType {
      small,
      medium,
      large
   }

   void Start(){
      switch (type){
         case enemyType.large:
            moveSpeed = 0.001f;
            break;
         case enemyType.medium:
            moveSpeed = 0.005f;
            break;
         case enemyType.small:
         default:
            moveSpeed = 0.01f;
            break;
      }
   }

   void Awake(){
   }

      // TODO Make movement stop if stuck 
    // Update is called once per frame
   void Update(){
      // Move towards player 
      if (WalkScript.playerPos.y < gameObject.transform.position.y){
         transform.position = transform.position - new Vector3(0f, moveSpeed, 0);
      }
      if (WalkScript.playerPos.y > gameObject.transform.position.y){
         transform.position = transform.position + new Vector3(0f, moveSpeed, 0);
      }
      if (WalkScript.playerPos.x > gameObject.transform.position.x){
         transform.position = transform.position + new Vector3(moveSpeed, 0f, 0);
      }
      if (WalkScript.playerPos.x < gameObject.transform.position.x){
         transform.position = transform.position - new Vector3(moveSpeed, 0f, 0);
      }

      //TODO Figure out angel offset
      // Look at player this was working then stopped I love it so much 
     // Quaternion rotation = Quaternion.LookRotation(
       //  WalkScript.playerPos - transform.position ,
         //transform.TransformDirection(Vector3.up)
      //);
      //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
      //transform.right = WalkScript.playerPos - transform.position;
      Vector3 dir = WalkScript.playerPos - transform.position;
      angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle + look_angle_offset, Vector3.forward);
   }
   
}
