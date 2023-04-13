using UnityEngine;

public class BodyAnimationController : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController bodyController;
    
    private SkinManager _skinManager;

    private void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
        _skinManager.AnimatorController = bodyController;
        _skinManager.KeepAnimatorController = true;
    }
}
