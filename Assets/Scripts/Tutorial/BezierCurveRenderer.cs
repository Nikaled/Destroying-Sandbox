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

    private float CurrentDistance;
    private float NormalDistance = 10f;

    private float tilingMultiplier = 0.3f;
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

    public void HideArrow()
    {
        ArrowIsActive = false;
        lineRenderer.positionCount = 0;
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
        CurrentDistance = Vector3.Distance(startPoint.position, endPoint.position);
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateBezierPoint(t, startPoint.position+new Vector3(0,1,0), betweenpoint, endPoint.position);
        }
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);

        float tilingX = tilingMultiplier* CurrentDistance / NormalDistance;

        // Получаем текущий Tiling материала
        Vector2 currentTiling = arrowMaterial.mainTextureScale;

        // Устанавливаем новый Tiling по X
        arrowMaterial.mainTextureScale = new Vector2(tilingX, currentTiling.y);
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