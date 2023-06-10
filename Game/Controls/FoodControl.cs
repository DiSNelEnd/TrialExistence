using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl
{
    private string lastEatFood;
    private int foodCount;
    private int differentFoodCount;
    private bool ItsBoring => GameRoot.Game.Player.Contains("Bored");
    private readonly string monotonyName = "Monotony";
    private readonly string starvationName = "Starvation";
    private readonly string gluttonyName = "Gluttony";
    private readonly string desertName = "Dessert";
    private readonly int maxFoodCountInDay = 4;
    private bool IsGluttony=>GameRoot.Game.Player.Contains(gluttonyName);
    private bool IsMonotony
    {
        get
        {
            var count = ItsBoring ? 2 : 3;
            return differentFoodCount > count;
        }
    }

    public FoodControl(int foodCount, int differentFoodCount,string lastEatFood)
    {
        this.foodCount = foodCount;
        this.differentFoodCount = differentFoodCount;
        this.lastEatFood = lastEatFood;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveFoodData(foodCount, differentFoodCount, lastEatFood);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckFoodCount;
    }

    public void Eat(string foodName)
    {
        DeleteCondition(starvationName);
        foodCount++;
        CountDifferentFood(foodName);
        if (foodName == desertName) foodCount++;
        if (foodCount > maxFoodCountInDay || IsGluttony)
            ImposeCondition(gluttonyName);
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }

    private void CountDifferentFood(string foodName)
    {
        lastEatFood ??= foodName;
        if (lastEatFood == foodName)
            differentFoodCount++;
        else
        {
            differentFoodCount = 0;
            lastEatFood = foodName;
        }
        if (IsMonotony)
            ImposeCondition(monotonyName);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }

    private void CheckFoodCount()
    {
        if (GameRoot.Game.StateControl.Enlightenment) return;
        if (foodCount == 0)
            ImposeCondition(starvationName);
        foodCount = 0;
        differentFoodCount = 0;
    }
}
