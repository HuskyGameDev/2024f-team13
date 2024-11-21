using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
   public int hp;
  
   public void hurt(int damage){
      Debug.Log("Does this get called");
      hp -= damage;
   }

   // Start is called before the first frame update
   void Start(){
      hp = (hp <= 0) ? 50 : hp; 
   }

   // Update is called once per frame
   void Update(){
      Debug.Log("Health: " + hp);
      if (hp <= 0) Destroy(gameObject); 
   }
}
//if (health == 0) Destroy(player);
