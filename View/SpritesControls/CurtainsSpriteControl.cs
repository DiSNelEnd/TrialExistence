using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurtainsSpriteControl 
{
    private static readonly string curtainsSpriteLv1Name = "Curtains1lv";
    private static readonly string curtainsSpriteLv2Name = "Curtains2lv";
    private static readonly string curtainsSpriteLv3Name = "Curtains3lv";
    public static void ToggleCurtains(int lv)
    {
        if (lv == 2)
            Toggle2Lv();
        else if (lv == 3)
            Toggle3Lv();
        else
            Toggle1Lv();
    }

    private static void Toggle1Lv()
    {
        Switcher.Toggle(curtainsSpriteLv1Name, true);     
    }

    private static void Toggle2Lv()
    {
        Switcher.Toggle(curtainsSpriteLv2Name, true);
        Switcher.Toggle(curtainsSpriteLv1Name, false);
    }

    private static void Toggle3Lv()
    {
        Switcher.Toggle(curtainsSpriteLv3Name, true);
        Switcher.Toggle(curtainsSpriteLv2Name, false);
    }
}
