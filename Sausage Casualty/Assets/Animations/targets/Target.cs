using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float life = 10;
    public Material material;
    Color newColor;

    private void Start()
    {
        newColor = Color.black;
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

            if (life >= 8) newColor = Color.magenta;
            else if (life >= 6) newColor = Color.red;
            else if (life >= 4) newColor = Color.yellow;
            else if (life >= 2) newColor = Color.white;
            else if (life <= 0) { Destroy(this.gameObject); }
        }
    }
}
