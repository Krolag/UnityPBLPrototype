using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private int _pointsForItem;
    private int _points = 0;

    public void AddPoint()
    {
        _points+=_pointsForItem;
        Debug.Log("Points: "+_points);
    }
}
