using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector3 camPos, camRot;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        camPos = new Vector3(0, 29.6f, 10.4f);
        camRot = new Vector3(60f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = PlayerController.instance.transform.position + camPos;
        cam.transform.rotation.SetEulerAngles(camRot);
    }
}
