using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillPageBackCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        Mail.ToggleBillPage(false);
    }
}
