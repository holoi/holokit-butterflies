using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public enum ButterflyState
{
    NotAttracted = 0,
    Attracted = 1,
    Stayed = 2
}

public class Butterfly : MonoBehaviour
{
    public ButterflyState State = ButterflyState.NotAttracted;

    public Vector3 TargetPosition;

    [SerializeField] float m_Speed = 0.6f;

    [SerializeField] VisualEffect m_Vfx;

    float m_AttractedAmp = 0;

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
        m_AttractedAmp = 0;
    }

    private void OnAttracted()
    {
        //transform.LookAt(TargetPosition);
        var tempPos = Vector3.Lerp(transform.position, TargetPosition, m_Speed * Time.deltaTime);
        transform.position = tempPos;
        var dist = Vector3.Distance(tempPos, TargetPosition);

        if (dist > 1f) dist = 1f;

        m_AttractedAmp = (1f - dist);

        m_Vfx.SetFloat("Attracted Amp", m_AttractedAmp);

    }

    private void OnStayed()
    {

    }
}