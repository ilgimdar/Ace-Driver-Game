using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControl : MonoBehaviour
{
    Vector3 localAngle;
    float steerAngle, maxSteerAngle = 30f;
    public GameObject player;

    void Update()
    {
        steerAngle = (player.GetComponent<PlayerMovement>().xSpeed * 60f) * maxSteerAngle;
    }

    private void LateUpdate()
    {
        localAngle = transform.localEulerAngles;
        localAngle.z = steerAngle;
        transform.localEulerAngles = localAngle;
    }
}
