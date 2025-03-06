using Microsoft.ML.Data;

namespace UserBehaviorBlazor.Models;

public class UserBehaviorPrediction
{
    [ColumnName("PredictedLabel")] 
    public bool Prediction;
    public float Probability;
}
