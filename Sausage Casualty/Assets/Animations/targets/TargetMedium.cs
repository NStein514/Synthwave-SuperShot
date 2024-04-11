using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMedium : MonoBehaviour
{
    public float life = 6;
    public Material material;
    Color newColor;

    private void Start()
    {
        newColor = Color.red;
    }

    private void Update()
    {
        this.gameObject.GetComponent<Renderer>().material.color = newColor;
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("test");
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Destroy(other.transform.parent.gameObject);
            Debug.Log(life);
            life--;

            if (life >= 4) newColor = Color.yellow;
            else if (life >= 2) newColor = Color.white;
            else if (life <= 0) { Destroy(this.gameObject); }
        }
    }
}


// 10 black
// 8 magenta
// 6 red
// 4 yellow
// 2 white