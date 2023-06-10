using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Link : MonoBehaviour
{
    [SerializeField] private string link;
    public void ClickLink()
    {
        Application.OpenURL(link);
    }
}
