using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Action<IReadOnlyList<Transform>> OnChangeVisibleTargets;
    public IReadOnlyList<Transform> VisibleTargets => _visibleTargets;
    public List<ViewParam> ViewParams { get => _viewParams; set => _viewParams = value;}

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private List<ViewParam> _viewParams;
    private List<Transform> _visibleTargets = new();
    private List<Transform> _lastVisibleTargets = new();
    void Start() => StartCoroutine ("FindTargetsWithDelay", .2f);

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds (delay);
            _visibleTargets.Clear ();
            foreach (var viewParam in ViewParams)
                FindVisibleTargets(viewParam);
            if (!_lastVisibleTargets.All(_visibleTargets.Contains) && _lastVisibleTargets.Count == _visibleTargets.Count)
                OnChangeVisibleTargets.Invoke(_visibleTargets);
            _lastVisibleTargets = new List<Transform>(_visibleTargets);
        }
    }

    private void FindVisibleTargets(ViewParam viewParam) 
    {
        _visibleTargets.Clear ();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewParam.ViewRadius, _targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) 
        {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle (transform.forward, dirToTarget) < viewParam.ViewAngle / 2) 
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

[Serializable]
public class ViewParam
{
    public float ViewAngle => _viewAngle;
    public float ViewRadius => _viewRadius;

    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewRadius;
}