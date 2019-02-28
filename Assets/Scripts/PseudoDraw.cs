using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoDraw : MonoBehaviour
{
    public GyroCamera gyroCam;
    public Material lineMat;
    public float stepCheckDelay = .05f;
    public float lineWidth = .4f;

    static LineRenderer currentLine;
    static List<Vector3> currentPoints;
    static Vector3 lastMousePos;
    static Coroutine drawingCoroutine;

    const float AXIS_Z_DISTANCE = 30;
    static float drawOffsetZ;

    Camera mainCam;
    WaitForSeconds waitForStep;

    void Start()
    {
        waitForStep = new WaitForSeconds(stepCheckDelay);
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!currentLine)
            {
                var spawnedLine = new GameObject(Time.time.ToString());

                currentLine = spawnedLine.AddComponent<LineRenderer>();
                currentLine.material = lineMat;
                currentLine.positionCount = 0;
                currentLine.startWidth = currentLine.startWidth = lineWidth;
                currentLine.numCapVertices = currentLine.numCornerVertices = 90;
                currentPoints = new List<Vector3> { (lastMousePos = GetWorldMousePosition()) };
                drawingCoroutine = StartCoroutine(DrawLine());
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
            currentPoints = null;
            StopCoroutine(drawingCoroutine);
            drawOffsetZ = 0;
        }
        if (gyroCam.allowRotation = Input.touchCount == 0)
            drawOffsetZ = AXIS_Z_DISTANCE;
        else drawOffsetZ += AXIS_Z_DISTANCE + GyroCamera.ZAxisAcceleration;
    }

    Vector3 GetWorldMousePosition()
    {
        return mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, AXIS_Z_DISTANCE));
    }

    IEnumerator DrawLine()
    {
        while (gameObject.activeInHierarchy)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = GetWorldMousePosition();
                if (mousePos != lastMousePos)
                {
                    currentLine.positionCount = currentPoints.Count;
                    currentPoints.Add(mousePos);
                    currentLine.SetPositions(currentPoints.ToArray());
                    lastMousePos = mousePos;
                }
            }
            yield return waitForStep;
        }
    }
}
