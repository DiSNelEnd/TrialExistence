using System;
using System.Collections;
using System.Collections.Generic;

public class GameTime
{
    public event Action OnDayChanged;
    public event Action OnMonthChanged;
    public event Action OnYearChanged;
    public event Action OnTimeChanged;
    private readonly int month = 30;
    private readonly int year = 300;
    private DateTime time;
    public int DayInTime => ((time.Year-1) * 365) + time.DayOfYear;
    private int day;
    public GameTime(double minutes,double day)
    {
        time =new DateTime();
        AddTime(minutes);
        if(day>1)time=time.AddDays(day-1);
        this.day = DayInTime;
    }

    public double TimeInMinutes => time.Hour*60+time.Minute;
    public string TimeText => time.ToString("HH:mm");
    public string Day => DayInTime.ToString();

    public void AddTime(double minutes)
    {
        time=time.AddMinutes(minutes);
        OnTimeChanged?.Invoke();
        if (day < DayInTime)
            ChangeDay();
        if (day % month == 0)
            OnMonthChanged?.Invoke();
        if (day % year == 0)
            OnYearChanged?.Invoke();
    }

    public static Tuple<int,int> ParseTime(double timeInMinutes)
    {
        var hour = (int)timeInMinutes / 60;
        var minutes = (int)timeInMinutes % 60;
        return new Tuple<int, int>(hour, minutes);
    }

    public static string CorrectFormat(int number)
    {
        return number <= 9 ? $"0{number}" : $"{number}";
    }

    private void ChangeDay()
    {
        OnDayChanged?.Invoke();
        day = DayInTime;
    }
}
