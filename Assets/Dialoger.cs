using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialoger : MonoBehaviour
{
    [SerializeField] TMP_Text box;
    [SerializeField] GameObject child;

    public void RunText(string text)
    {
        IEnumerator Text()
        {
            child.SetActive(true);
            PlayerController.State.canMove = false;

            box.maxVisibleCharacters = 0;
            box.text = text;
            while (box.maxVisibleCharacters < text.Length)
            {
                box.maxVisibleCharacters++;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

            child.SetActive(false);
            PlayerController.State.canMove = true;
        }
        StartCoroutine(Text());
    }
}
