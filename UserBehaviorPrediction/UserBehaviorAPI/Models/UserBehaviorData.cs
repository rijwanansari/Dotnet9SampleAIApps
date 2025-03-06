using Microsoft.ML.Data;

namespace UserBehaviorAPI.Models;

public record UserBehaviorData
{
    [LoadColumn(0)] 
    public float UserId{ get; set; }
    [LoadColumn(1)] 
    public float Age { get; set; }
    [LoadColumn(2)] 
    public float PageViews { get; set; }
    [LoadColumn(3)] 
    public float TimeSpent { get; set; }
    [LoadColumn(4)] 
    public bool ClickedAd { get; set; }
}
