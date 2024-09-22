using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector3 camPos, camRot;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        cam.transform.position = PlayerController.instance.transform.position + camPos;
        cam.transform.rotation.SetEulerAngles(camRot);
    }
}
