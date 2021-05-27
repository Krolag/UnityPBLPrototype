using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableComponents : MonoBehaviour
{
    [SerializeField] public bool isColliderStatic;
    [SerializeField] public bool isInteractable;
    [SerializeField] public bool isTreasure;
    [SerializeField] public bool isCash;
}
