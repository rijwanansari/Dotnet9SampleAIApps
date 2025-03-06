
using Microsoft.ML.Data;

namespace UserBehaviorAPI.Models;

public class UserBehaviorPrediction
{
    [ColumnName("PredictedLabel")] 
    public bool Prediction;
    public float Probability;

}
