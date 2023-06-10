using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AspectRatio
{
    private static float width = 16.0f;
    private static float height = 9.0f;
    public static void Controlling()
    {
        var targetaspect = width / height;
        var windowaspect = Screen.width / (float)Screen.height;
        var scaleheight = windowaspect / targetaspect;
        if (scaleheight < 1.0f)
            AddLeterbox(scaleheight);
        else
            AddPillarbox(scaleheight);
    }

    private static void AddLeterbox(float scaleheight)
    {
        var rect = Root.Instance.MainCamera.rect;
        rect.width = 1.0f;
        rect.height = scaleheight;
        rect.x = 0;
        rect.y = (1.0f - scaleheight) / 2.0f;
        Root.Instance.MainCamera.rect = rect;
    }

    private static void AddPillarbox(float scaleheight)
    {
        var scalewidth = 1.0f / scaleheight;
        var rect = Root.Instance.MainCamera.rect;
        rect.width = scalewidth;
        rect.height = 1.0f;
        rect.x = (1.0f - scalewidth) / 2.0f;
        rect.y = 0;
        Root.Instance.MainCamera.rect = rect;
    }
}
