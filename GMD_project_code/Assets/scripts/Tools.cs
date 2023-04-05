using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public static Transform GetTransformByTag(Transform parent, string tagSearched) // could be in another class
    {
        //apparently there is no built in way to recursively look for a child in a go
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tagSearched))
            {
                return child;
            }
            if (child.childCount > 0)
            {
                return GetTransformByTag(child, tagSearched);
            }
        }

        return null;
    }
}
