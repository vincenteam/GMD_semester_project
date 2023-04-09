using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private Animator _animator;
    public Animator AnimatorInstance
    {
        get => _animator;
    }

    private RuntimeAnimatorController _animatorController;
    public RuntimeAnimatorController AnimatorController
    {
        get => _animatorController;
        set => _animatorController = value;
    }

    private GameObject _skin;

    public GameObject SkinInstance
    {
        get => _skin;
    }
    
    private bool _keepAnimatorController; // should animator controller be preserved when changing skin ?
    public bool KeepAnimatorController
    {
        get => _keepAnimatorController;
        set => _keepAnimatorController = value;
    }

    private void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>(); // tmp
        _skin = Tools.GetTransformByTag(transform, "Skin").gameObject;
    }

    public void ChangeSkin(GameObject go)
    {
        Destroy(_skin);

        GameObject newSkin = Instantiate(go, transform);
        _skin = newSkin;
        
        _animator = newSkin.GetComponent<Animator>();
        if (_keepAnimatorController && _animatorController is not null)
        {
            _animator.runtimeAnimatorController = _animatorController;
        }
        else
        {
            _animatorController = _animator.runtimeAnimatorController;
        }
    }
}
