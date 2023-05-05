using UnityEngine;

public class MenuToLevel1 : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameObject _temp;
    public void ChangeScene()
    {
        _temp = GameObject.FindGameObjectWithTag("BackgroundMusic");
        Destroy(_temp);
        TransitionManager.transitionInstance.Transition(sceneName);
    }
}
