using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
