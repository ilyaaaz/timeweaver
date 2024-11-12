using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;


    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the object that collided is in the specified layer
        if (other.CompareTag("Reversible"))
        {
            Debug.Log(other.gameObject.name);
            GameManager.instance.reversibleObjects.Add(other.gameObject);
        }

        //call GameManager's function to actually do reverse for these objects;
        GameManager.instance.ReverseObjects();
    }
}
