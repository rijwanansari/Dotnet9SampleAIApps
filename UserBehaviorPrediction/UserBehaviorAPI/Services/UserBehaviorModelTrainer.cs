
using Microsoft.ML;
using UserBehaviorAPI.Models;
using System.IO;

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
        //add path to the data file under folder data based on root folder
        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserBehaviourData.csv");  
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("Data file not found");
        }

        var dataView = _mlContext.Data.LoadFromTextFile<UserBehaviorData>(
                            filepath, separatorChar: ',', hasHeader: true);

        // var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("ClickedAd")
        //                 .Append(_mlContext.Transforms.Concatenate("Features", "Age", "PageViews", "TimeSpent"))
        //                 .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
        //                 .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

        var pipeline = _mlContext.Transforms.Concatenate("Features", "Age", "PageViews", "TimeSpent")  // Concatenate features
                        .Append(_mlContext.Transforms.NormalizeMinMax("Features"))  // Normalize features
                        .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "ClickedAd"));

        
        _model = pipeline.Fit(dataView);
        _mlContext.Model.Save(_model, dataView.Schema, "MLModel/UserBehaviorModel.zip");
    }

}
