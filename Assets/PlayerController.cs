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

    [SerializeField] int height;

    public Transform hitBox;
    public float yepBox;

    public static class State
    {
        public static bool canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        a = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(Animate());

        float yMax = -float.MaxValue;
        for (int i = 0; i < a.sharedMesh.vertices.Length; i++)
        {
            if (a.sharedMesh.vertices[i].y > yMax)
            {
                yMax = a.sharedMesh.vertices[i].y;
                height = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!State.canMove) return;
        
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
        if (!State.canMove) return;

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

            hitBox.gameObject.SetActive(target < 0 && current > 0);
            hitBox.localPosition = new Vector3(hitBox.localPosition.x, hitBox.localPosition.y, Mathf.Lerp(0, yepBox, current) / transform.localScale.z);

            yield return new WaitForFixedUpdate();
        }
    }

    Vector3 TripleMult(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
