using Cinemachine;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    private Cinemachine3rdPersonFollow _follow;

    [SerializeField] private float bottomLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float startOffset;
    private float _offset;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        _follow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        
        if(_follow != null) _follow.ShoulderOffset.y = startOffset;
    }
    

    public void RotateX(float amount)
    {
        _offset -= amount;
        if (_offset < bottomLimit)
        {
            _offset = bottomLimit;
        }else if(_offset > topLimit)
        {
            _offset = topLimit;
        }
        if(_follow != null) _follow.ShoulderOffset.y = _offset;
    }
}
