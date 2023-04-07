using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private Animator _animator;
    public Animator AnimatorInstance
    {
        get => _animator;
    }

    private GameObject _skin;

    public GameObject SkinInstance
    {
        get => _skin;
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
        _animator.Rebind(); // needed ?
    }
}
