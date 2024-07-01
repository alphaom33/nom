using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Destroy(collision.gameObject);
            Scorer.score++;
        }
    }
}
