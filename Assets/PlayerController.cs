using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject CamerFollow;

    SkinnedMeshRenderer a;
    public float target = -1;
    public float speedy;
    public float current;

    public float topClamp;
    public float bottomClamp;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        a = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")), Space.Self);

        CamerFollow.transform.Rotate(new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X")));

        if (Input.GetMouseButtonDown(0))
        {
            target = 1;
            CameraController.Camera.current = 1;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            target = -1;
            CameraController.Camera.current = 0;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(-90, transform.eulerAngles.y, 0);
    }

    IEnumerator Animate()
    {
        while (true)
        {
            current += speedy * Mathf.Sign(target);
            current = Mathf.Clamp01(current);
            a.SetBlendShapeWeight(0, Mathf.Lerp(0, 100, current));
            yield return new WaitForEndOfFrame();
        }
    }
}
