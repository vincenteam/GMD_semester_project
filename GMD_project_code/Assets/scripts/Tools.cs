using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Tools
{
    public static Transform GetTransformByTag(Transform parent, string tagSearched)
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

    public static TComponent GetGoWithComponent<TComponent> (Transform parent)
        where TComponent: class
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            TComponent c = child.GetComponent<TComponent>();
            if (c is not null)
            {
                return c;
            }else if (child.childCount > 0)
            {
                c = GetGoWithComponent<TComponent>(child);
                if (c is not null)
                {
                    return c;
                }
            }
        }

        return null;
    }
}
