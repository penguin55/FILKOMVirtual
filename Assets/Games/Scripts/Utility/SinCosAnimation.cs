using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCosAnimation : MonoBehaviour
{
    [SerializeField] private float radius = 30f;
    [SerializeField] private float speed;
    [SerializeField] private Transform objectMove;

    private Vector2 basePos;

    private void Start()
    {
        basePos = objectMove.position.ParseVector2();
    }

    private void Update()
    {
        Animate();
    }

    private void Animate() {
        objectMove.position = basePos + new Vector2(Mathf.Sin(Time.time*speed) * radius, Mathf.Cos(Time.time*speed) * radius);
    }
}
