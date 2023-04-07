using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimationController : MonoBehaviour
{
    private SkinManager _skinManager;
    private static readonly int IsBody = Animator.StringToHash("isBody");

    private void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
    }

    void Start()
    {
        _skinManager.AnimatorInstance.SetBool(IsBody, true);
    }
}
