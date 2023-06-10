using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuInGameCommand : ICommand
{
    public void Do()
    {
        Room.Instance.OpenMenu();
    }
}
