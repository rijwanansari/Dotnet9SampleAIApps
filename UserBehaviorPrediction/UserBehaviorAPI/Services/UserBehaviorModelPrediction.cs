using Microsoft.ML;
using UserBehaviorAPI.Models;

namespace UserBehaviorAPI.Services;

public class UserBehaviorModelPrediction
{
     private readonly PredictionEngine<UserBehaviorData, UserBehaviorPrediction> _predictionEngine;

        public UserBehaviorModelPrediction()
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load("MLModel/UserBehaviorModel.zip", out _);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<UserBehaviorData, UserBehaviorPrediction>(model);
        }

        // Predict if a user will click on an ad
        public UserBehaviorPrediction Predict(UserBehaviorData userBehaviorData)
        {
            return _predictionEngine.Predict(userBehaviorData);
        }

}
