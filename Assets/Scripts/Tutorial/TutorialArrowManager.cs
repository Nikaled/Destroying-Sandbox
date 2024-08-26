using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowManager : MonoBehaviour
{
    [SerializeField] Transform[] TutorContorlPoints;
    [SerializeField] Transform[] TutorEndPoints;
    [SerializeField] BezierCurveRenderer _bezierCurve;
    int currentTutorArrIndex = -1;
    private void Start()
    {
        for (int i = 0; i < TutorContorlPoints.Length; i++)
        {
            TutorContorlPoints[i].gameObject.SetActive(false);
            TutorEndPoints[i].gameObject.SetActive(false);
        }
        SetNewArrowDestination();
    }
    public void HideArrow()
    {
        _bezierCurve.HideArrow();
    }
    public void SetNewArrowDestination()
    {
        currentTutorArrIndex++;

        if(currentTutorArrIndex < TutorContorlPoints.Length)
        {
            _bezierCurve.controlPoint = TutorContorlPoints[currentTutorArrIndex];
            _bezierCurve.endPoint = TutorEndPoints[currentTutorArrIndex];
            _bezierCurve.ArrowIsActive = true;
        }

    }
}
