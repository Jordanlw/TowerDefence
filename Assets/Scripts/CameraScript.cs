using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    public GameObject target;
    public float cameraMoveSpeed;
    public float cameraRotationSpeed;
    public float cameraZoomSpeed;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed * Time.deltaTime;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 20, 80);
        if (Input.GetMouseButton(2))
        {
            Vector3 v = new Vector3(-Input.GetAxisRaw("Mouse X") * cameraMoveSpeed * Time.deltaTime * cam.fieldOfView, 0, -Input.GetAxisRaw("Mouse Y") * cameraMoveSpeed * Time.deltaTime * cam.fieldOfView);
            //Debug.Log("prev: " + v);
            //Debug.Log("Rot: " + cam.transform.rotation.eulerAngles.y);
            v = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * v;
            //Debug.Log("Postv: " + v);
            //Debug.Log("-----");
            target.transform.position += v;
        }
        else if (Input.GetMouseButton(1))
        {
            cam.transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * cameraRotationSpeed * Time.deltaTime);
        }
    }
}
