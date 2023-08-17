using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ButterflyState
{
    NotAttracted = 0,
    Attracted = 1,
    Stayed= 2
}

public class Butterfly : MonoBehaviour
{
    public ButterflyState State = ButterflyState.NotAttracted;

    public Vector3 TargetPosition;

    [SerializeField] float m_Speed = 0.6f;

    private void Update()
    {
        switch (State)
        {
            case ButterflyState.NotAttracted:
                OnNotAttracted();
                break;
            case ButterflyState.Attracted:
                OnAttracted();
                break;
            case ButterflyState.Stayed:
                OnStayed();
                break;
        }
    }

    private void OnNotAttracted()
    {
        // TODO: Sizheng
    }

    private void OnAttracted()
    {
        transform.LookAt(TargetPosition);
        transform.position = Vector3.Lerp(transform.position, TargetPosition, m_Speed * Time.deltaTime);
    }

    private void OnStayed()
    {

    }
}
