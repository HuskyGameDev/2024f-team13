using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour{

   private Rigidbody2D rb2D;
   private float moveSpeed, angle;
   public int look_angle_offset;

   void Start(){
     // rb2D = gameObject.GetComponent<Rigidbody2D>();
      moveSpeed = 0.01f;

   }

   void Awake(){
   }

    // Update is called once per frame
   void Update(){
      // Move towards player 
      if (WalkScript.playerY < gameObject.transform.position.y){
         transform.position = transform.position - new Vector3(0f, moveSpeed, 0);
      }
      if (WalkScript.playerY > gameObject.transform.position.y){
         transform.position = transform.position + new Vector3(0f, moveSpeed, 0);
      }
      if (WalkScript.playerX > gameObject.transform.position.x){
         transform.position = transform.position + new Vector3(moveSpeed, 0f, 0);
      }
      if (WalkScript.playerX < gameObject.transform.position.x){
         transform.position = transform.position - new Vector3(moveSpeed, 0f, 0);
      }

      //TODO Figure out angel offset
      // Look at player 
      Quaternion rotation = Quaternion.LookRotation(
         WalkScript.playerPos - transform.position ,
         transform.TransformDirection(Vector3.up)
      );
      transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
   }
}
