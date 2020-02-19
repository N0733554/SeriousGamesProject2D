using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera camObj;

    Vector3 originPos;
    float originZoom;

    public float lerpSpeed;
    public float zoomAmount;

    void Start()
    {
        camObj = GetComponent<Camera>();
        originPos = transform.position;
        originZoom = camObj.orthographicSize;
    }

    public void MoveToPos(Vector3 newPos)
    {
        StartCoroutine(lerpTo(newPos, zoomAmount));
    }

    public void MoveToOrigin()
    {
        StartCoroutine(lerpTo(originPos, originZoom));
    }

    IEnumerator lerpTo(Vector3 newPos, float newZoom)
    {
        float oldZoom = camObj.orthographicSize;
        Vector3 oldPos = transform.position;
        newPos.z = originPos.z;
        float t = 0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            transform.position = Vector3.Lerp(oldPos, newPos, t);
            camObj.orthographicSize = Mathf.Lerp(oldZoom, newZoom, t);

            yield return 0;
        }
    }
}
