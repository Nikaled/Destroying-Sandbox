using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurveRenderer : MonoBehaviour
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform endPoint;
    public Material arrowMaterial;
    public int exerciseIndex;

    private LineRenderer lineRenderer;
    public int numPoints = 50; // Количество точек на кривой
    public Vector3[] positions; // Массив позиций для точек на кривой
    public bool ArrowIsActive;
    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        positions = new Vector3[numPoints];
        StartCoroutine(InfiniteDrawCurve());
    }

    private void Update()
    {
        arrowMaterial.mainTextureOffset += new Vector2(-Time.deltaTime, 0);
    }

    private IEnumerator InfiniteDrawCurve()
    {
        while (true)
        {
            yield return 0.3f;
            if (ArrowIsActive)
            {
                DrawCurve();
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }
    }
    void DrawCurve()
    {
        if (Vector3.Distance(startPoint.position, endPoint.position) < 3)
        {
            lineRenderer.positionCount = 0;
            return;
        }
        Vector3 betweenpoint = controlPoint.position;
        if (Vector3.Distance(startPoint.position, controlPoint.position) > Vector3.Distance(startPoint.position, endPoint.position))
        {
            betweenpoint = endPoint.position;
        }
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateBezierPoint(t, startPoint.position, betweenpoint, endPoint.position);
        }
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Рассчет квадратичной кривой Безье: B(t) = (1-t)^2 * P0 + 2(1-t)t * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uut = 2 * u * t;

        Vector3 p = uu * p0;    // первый термин
        p += uut * p1;          // второй термин
        p += tt * p2;           // третий термин

        return p;
    }
}