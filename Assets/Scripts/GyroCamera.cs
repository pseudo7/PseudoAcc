using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class GyroCamera : MonoBehaviour
{
    public bool allowRotation;
    public Text position;
    public Vector3 gyro;

    Vector3 lastPos, delta;
    float yRotation, xRotation;

    const float ACC_LEVEL = .1f;

    private void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        Debug.LogError(Input.acceleration);

        if (!allowRotation) return;
        UpdateCameraPostion();
        UpdateCameraRotation();
    }

    void UpdateCameraPostion()
    {
        //Vector3 acc = ;
        //gyro = acc * 500 * Time.deltaTime;
        //position.text = string.Format("{0}\n{1}\n{2}", transform.position, gyro, acc);
        transform.Translate(0, 0, Mathf.Abs(ZAxisAcceleration) > ACC_LEVEL ? ZAxisAcceleration * 500 * Time.deltaTime : 0, Space.World);
    }

    public static float ZAxisAcceleration
    {
        get
        {
            return -Input.gyro.userAcceleration.z;
        }
    }

    void UpdateCameraRotation()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor))
        {
            if (Input.GetMouseButtonDown(0))
                lastPos = Input.mousePosition;
            else if (Input.GetMouseButton(0))
            {
                delta = Input.mousePosition - lastPos;

                transform.Rotate(new Vector3(-delta.y, delta.x));
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

                lastPos = Input.mousePosition;
            }
        }
        else
        {
            xRotation += -Input.gyro.rotationRateUnbiased.x;
            yRotation += -Input.gyro.rotationRateUnbiased.y;
            transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
        }

    }

    public void ResetCamera()
    {
        xRotation = yRotation = 0;
        transform.rotation = Quaternion.identity;
    }
}