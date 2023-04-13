using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private const string MountPointTag = "CamMountPoint";
    private bool _locked = false; // indicate if the camera is filming something
    private Queue<GameObject> _targets = new Queue<GameObject>();


    public void ChangeFollowTarget(GameObject go)
    {
        if (!_locked && _targets.Count == 0)
        {
            _changeFollowTarget(go);
        }
        else
        {
            _targets.Enqueue(go);
        }
    }
    
    public void ForceChangeFollowTarget(GameObject go)
    {
        _changeFollowTarget(go);
    }

    private void _changeFollowTarget(GameObject go)
    {
        Transform parent = Tools.GetTransformByTag(go.transform, MountPointTag);
        if (parent is null) parent = go.transform;

        Transform transform1;
        (transform1 = transform).SetParent(parent);
        transform1.localPosition = new Vector3();
        transform1.localRotation = new Quaternion();

        _locked = true;
    }

    public void UnLock()
    {
        _locked = false;
        if (_targets.Count > 0)
        {
            _changeFollowTarget(_targets.Dequeue());
        }
    }
}
