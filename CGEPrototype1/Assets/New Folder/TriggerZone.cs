using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{

    public TMP_Text output;

    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggered by " + collision.gameObject.name);

        if (collision.gameObject.tag == "Player")
        {
            output.text = textToDisplay;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
