using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Pretty standard, I'm sure you can work out how this script works.

    public Transform target;

    public Vector2 pitchMinMax;

    public float xSensitivity;
    public float ySensitivity;
    public float rotAccelTime;

    Vector3 rotAccelVelocity;
    Vector3 currentRot;

    float pitch;
    float yaw;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = Cursor.visible;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxisRaw("Mouse X") * xSensitivity;
        pitch += Input.GetAxisRaw("Mouse Y") * ySensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRot = Vector3.SmoothDamp(currentRot, new Vector3(pitch, yaw), ref rotAccelVelocity, rotAccelTime);
        transform.eulerAngles = currentRot;

        transform.position = target.position;
    }
}
