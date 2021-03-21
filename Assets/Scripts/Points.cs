using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [SerializeField] private int _pointsForItem;
    private int _points = 0;

    public void AddPoint()
    {
        _points += _pointsForItem;       
        GetComponentInChildren<TextMeshProUGUI>().text = _points.ToString();

    }
}
