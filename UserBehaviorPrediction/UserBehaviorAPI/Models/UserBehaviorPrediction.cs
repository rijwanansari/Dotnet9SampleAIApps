
using Microsoft.ML.Data;

namespace UserBehaviorAPI.Models;

public record UserBehaviorPrediction
{
    [ColumnName("PredictedLabel")] 
    public bool Prediction { get; set; }
    public float Probability { get; set; }

}
