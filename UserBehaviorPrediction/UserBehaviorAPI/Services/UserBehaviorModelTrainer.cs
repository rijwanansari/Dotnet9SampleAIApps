
using Microsoft.ML;
using UserBehaviorAPI.Models;

namespace UserBehaviorAPI.Services;

public class UserBehaviorModelTrainer
{
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public UserBehaviorModelTrainer()
    {
        _mlContext = new MLContext();
    }

    public void TrainModel()
    {
        var dataView = _mlContext.Data.LoadFromTextFile<UserBehaviorData>("Data/UserBehaviorData.csv", separatorChar: ',', hasHeader: true);
        var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("ClickedAd")
                        .Append(_mlContext.Transforms.Concatenate("Features", "Age", "PageViews", "TimeSpent"))
                        .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                        .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());
        
        _model = pipeline.Fit(dataView);
        _mlContext.Model.Save(_model, dataView.Schema, "MLModel/UserBehaviorModel.zip");
    }

}
