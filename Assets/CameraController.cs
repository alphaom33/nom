using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject[] cameras;

    public static class Camera
    {
        public static int current;
    }

    private void Update()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == Camera.current)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }
    }
}
