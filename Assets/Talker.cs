using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    private Dialoger dialoger;
    public string text;
    bool playered;

    // Start is called before the first frame update
    void Start()
    {
        dialoger = GameObject.FindWithTag("dlogger").GetComponent<Dialoger>();
    }

    private void Update()
    {
        if (playered && PlayerController.State.canMove && Input.GetKeyDown(KeyCode.E)) dialoger.RunText(text);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playered = false;
    }
}
