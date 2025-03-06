using Microsoft.ML.Data;

namespace UserBehaviorBlazor.Models;


public class UserBehaviorData
{
    [LoadColumn(0)] 
    public float UserId;
    [LoadColumn(1)] 
    public float Age;
    [LoadColumn(2)] 
    public float PageViews;
    [LoadColumn(3)] 
    public float TimeSpent;
    [LoadColumn(4)] 
    public bool ClickedAd;
}