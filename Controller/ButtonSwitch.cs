using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ButtonSwitch : MonoBehaviour
{
    private Dictionary<string, ICommand> commands;
    private Pult pult;

    private void Awake()
    {
        pult = new Pult();
        FillCommands();
    }

    public void Click()
     {
        var name = EventSystem.current.currentSelectedGameObject.name;
        if (!commands.ContainsKey(name)) 
            throw new KeyNotFoundException("Command not found");
        var command = commands[name];
        pult.SetCommand(command);
        pult.PressButton();
    }

    #region Commands
    private void FillCommands()
    {
        commands = new Dictionary<string, ICommand>
        {
            {"ComputerButton",new ToggleComputerCommand(Doing.On)},
            {"MainPageButton",new OpenMainPageOnComputerCommand()},
            {"NewGameButton", new NewGameCommand()},
            {"InformationButton", new OpenInfPageComputerCommand()},
            {"ComputerBackMainButton",new ToggleComputerCommand(Doing.Off)},
            {"EatButton", new OpenEatPageComputerCommand()},
            {"RelaxButton", new OpenRelaxPageComputerCommand()},
            {"WorkButton", new OpenWorkPageComputerCommand() },
            {"FastFoodButton", new ShowFastFoodButtonCommand() },
            {"StandardKitchenButton", new ShowStandardKitchenButtonCommand()},
            {"ProperNutritionButton", new ShowProperNutritionButtonCommand() },
            {"DessertButton", new ShowDessertButtonCommand() },
            {"AlcoholButton",new ShowAlcoholButtonCommand() },
            {"MenuButton", new OpenMenuInGameCommand() },
            {"BackGameButton", new BackGameCommand() },
            {"ExitButton", new ExitGameCommand() },
            {"SettingsButton", new ToggleSettingPageCommand(Doing.On)},
            {"BackMenuButton", new ToggleSettingPageCommand(Doing.Off) },
            {"ApplySettingsButton", new ApplySettingsCommand() },
            {"FastFoodCosmoButton", new EatFastFoodCommand() },
            {"StandardKitchenCosmoButton", new EatStandardKitchenCommand() },
            {"ProperNutritionCosmoButton", new EatProperNutritionCommand() },
            {"DessertCosmoButton", new EatDessertCommand() },
            {"AlcoholCosmoButton", new DrinkAlcoholCommand() },
            {"ResultCloseButton", new ToggleResultScreenCommand(Doing.Off)},
            {"PlayGameButton", new FunPlayGameCommand() },
            {"SeeSomethingButton", new FunSeeSomethingCommand() },
            {"SocialNetworkButton", new TalkSocialNetCommand() },
            {"PornSiteButton", new GoPornSiteCommand() },
            {"HobbyButton" , new TookeUpHobbyCommand() },
            {"ProccedButton", new GetToWorkCommand() },
            {"VacationButton", new TakeVacationCommand()},
            {"DayOffButton", new TakeDayOffCommand() },
            {"BedButton", new ToggleBedCommand(Doing.On) },
            {"BedBackMainButton", new ToggleBedCommand(Doing.Off) },
            {"SleepButton", new SleepCommand() },
            {"NapButton", new TakeNapCommand() },
            {"TrainButton", new ToggleTrainCommand(Doing.On) },
            {"TrainBackMainButton", new ToggleTrainCommand(Doing.Off) },
            {"EasyWorkoutButton", new TrainEasyWorkoutCommand() },
            {"HardWorkoutButton", new TrainHardWorkoutCommand() },
            {"StretchingButton", new TrainStretchingCommand() },
            {"MailButton", new OpenMailPageComputerCommand() },
            {"PayButton", new PayBillCommand() },
            {"BillPageBackButton", new BillPageBackCommand() },
            {"ProceedButton", new ProceedGameCommand() },
            {"MenuNotiYesButton", new ToggleMenuNotiCommand(Doing.On) },
            {"MenuNotiNoButton", new ToggleMenuNotiCommand(Doing.Off) },
            {"CreditsButton",new ToggleCreditsPageCommand(Doing.On) },
            {"BackSettingsButton", new ToggleCreditsPageCommand(Doing.Off) },
            {"IDidButtonEnd",new TolkEndCommand("TolkTextIDid",1) },
            {"WhereButtonEnd", new TolkEndCommand("TolkTextWhere",1) },
            {"RememberButtonEnd", new TolkEndCommand("TolkTextRemember",2) },
            {"WhatAmIButtonEnd", new TolkEndCommand("TolkTextWhatAmI",1) },
            {"WhatAreYouButtonEnd", new TolkEndCommand("TolkTextWhatAreYou",1) },
            {"ImReadyButtonEnd", new TolkEndCommand("TolkTextImReady",0) }
        };
    }
    #endregion
}
