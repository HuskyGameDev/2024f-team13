using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeycardScript : MonoBehaviour
{
    [SerializeField] GameObject originalLevelDoor;
    [SerializeField] GameObject openLevelDoor;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("This is working");
            originalLevelDoor.gameObject.SetActive(false);
            openLevelDoor.gameObject.SetActive(true);
            this.gameObject.SetActive(false);

        }
    }
}
