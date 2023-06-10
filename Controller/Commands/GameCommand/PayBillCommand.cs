using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayBillCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.BillControler.PayBill();
    }
}
