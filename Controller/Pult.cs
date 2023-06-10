using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public void Do();
}

public class Pult
{
    private ICommand command;
    public Pult(){}

    public void SetCommand(ICommand com)
    {
        command = com;
    }

    public void PressButton()
    {
        command?.Do();
    }
}

public enum Doing
{
    On,
    Off
}
