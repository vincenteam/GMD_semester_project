using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private const string MountPointTag = "CamMountPoint";
    private bool locked = false; // indicate if the camera is filming something
    private Queue<GameObject> targets = new Queue<GameObject>();


    public void ChangeFollowTarget(GameObject go)
    {
        if (!locked && targets.Count == 0)
        {
            _changeFollowTarget(go);
        }
        else
        {
            targets.Enqueue(go);
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
        
        transform.SetParent(parent);
        transform.localPosition = new Vector3();
        transform.localRotation = new Quaternion();

        locked = true;
    }

    public void UnLock()
    {
        locked = false;
        if (targets.Count > 0)
        {
            _changeFollowTarget(targets.Dequeue());
        }
    }
}
