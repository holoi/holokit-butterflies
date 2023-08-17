using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloInteractive.XR.HoloKit.iOS;

public class ButterflyManager : MonoBehaviour
{
    [SerializeField] HandTrackingManager m_HandTrackingManager;

    [SerializeField] List<Butterfly> m_Butterflies;

    [SerializeField] float m_AttractionThreshold = 0.5f;

    [SerializeField] float m_StayThreshold = 0.1f;

    [SerializeField] Transform m_HandSample;

    private void Update()
    {
#if UNITY_EDITOR
        if (true)
#else
        if (m_HandTrackingManager.HandCount > 0)
#endif
        {
#if UNITY_EDITOR
            Vector3 handPosition = m_HandSample.position;
#else
            Vector3 handPosition = m_HandTrackingManager.GetHandJointPosition(0, JointName.IndexDIP);
#endif
            float minDist = 999f;
            Butterfly closestButterfly = null;
            foreach (var butterfly in m_Butterflies)
            {
                float dist = Vector3.Distance(handPosition, butterfly.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestButterfly = butterfly;
                }
            }

            //Debug.Log($"MinDist: {minDist}");
            //Debug.Log($"Closest butterfly: {closestButterfly.name}");

            // We get the closest butterfly
            if (minDist > m_AttractionThreshold)
            {
                closestButterfly.State = ButterflyState.NotAttracted;
            }
            else if (minDist < m_StayThreshold)
            {
                closestButterfly.State = ButterflyState.Stayed;
            }
            else
            {
                closestButterfly.TargetPosition = handPosition;
                closestButterfly.State = ButterflyState.Attracted;
            }

            foreach (var butterfly in m_Butterflies)
            {
                if (butterfly != closestButterfly)
                    butterfly.State = ButterflyState.NotAttracted;
            }
        }
        else
        {
            // Set butterflies to not attracted
            foreach (var butterfly in m_Butterflies)
                butterfly.State = ButterflyState.NotAttracted;
        }
    }
}
