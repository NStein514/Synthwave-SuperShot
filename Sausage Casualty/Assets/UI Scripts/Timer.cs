using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float val;
    bool srt;
    public Text disvar;
    // Start is called before the first frame update
    void Start()
    {
        val = 0;
        srt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (srt)
        {
            val += Time.deltaTime;
        }

        double b = System.Math.Round(val, 2);

        disvar.text = b.ToString();
    }

    public void StopButton()
    {
        srt = false;
    }

    public void ResetButton()
    {
        srt = false;
        val = 0;
    }

    public void StartButton()
    { 
        srt = true; 
    }
}
