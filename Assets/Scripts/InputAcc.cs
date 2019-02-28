using UnityEngine;

public class InputAcc : MonoBehaviour
{
    public float speed = 10.0f;

    private void Start() { Input.gyro.enabled = true; }

    public static Vector3 Acceleration { get { return Input.gyro.userAcceleration; } }

}