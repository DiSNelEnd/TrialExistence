using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconView : MonoBehaviour
{
    public void SetSprite(Sprite conSprite)
    {
        var icon = GetComponent<Image>();
        icon.sprite = conSprite;
    }
}
