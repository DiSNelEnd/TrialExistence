using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcheble : MonoBehaviour, ISwitcheble
{
    private void Awake()
    {
        SwitcheblesRepository.AddSwitcheble(this);
    }

    public string Name => gameObject.name;

    public void Toggle(bool flag)
    {
        gameObject.SetActive(flag);
    }
}
