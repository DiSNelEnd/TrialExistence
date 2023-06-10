using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGameCommand : ICommand
{
    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        MenuGame.Instance.ToggleMenu(false);
        Room.Instance.ToggleRoom(true);
        Room.Instance.PlayRoomMusic();
    }
}
