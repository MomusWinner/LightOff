using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Action<IReadOnlyList<Transform>> OnChangeVisibleTargets;
    
    public float ViewRadius;
    [Range(0,360)]
    public float ViewAngle;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    public IReadOnlyList<Transform> VisibleTargets => _visibleTargets;

    private List<Transform> _visibleTargets = new();
    
    void Start() => StartCoroutine ("FindTargetsWithDelay", .2f);

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds (delay);
            FindVisibleTargets ();
        }
    }

    private void FindVisibleTargets() 
    {
        _visibleTargets.Clear ();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, ViewRadius, _targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) 
        {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle (transform.forward, dirToTarget) < ViewAngle / 2) 
            {
                float dstToTarget = Vector3.Distance (transform.position, target.position);
                
                if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, _obstacleMask)) 
                    _visibleTargets.Add (target);
            }
        }
        
        if(_visibleTargets.Any())
            OnChangeVisibleTargets.Invoke(_visibleTargets);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal) 
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}