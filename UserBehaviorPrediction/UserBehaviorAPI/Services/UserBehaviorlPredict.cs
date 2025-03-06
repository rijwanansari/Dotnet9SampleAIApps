using Microsoft.ML;
using UserBehaviorAPI.Models;

namespace UserBehaviorAPI.Services;

public class UserBehaviorPredict
{
     private readonly PredictionEngine<UserBehaviorData, UserBehaviorPrediction> _predictionEngine;

        public UserBehaviorPredict()
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load("MLModel/UserBehaviorModel.zip", out _);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<UserBehaviorData, UserBehaviorPrediction>(model);
        }

        public UserBehaviorPrediction Predict(UserBehaviorData userBehaviorData)
        {
            return _predictionEngine.Predict(userBehaviorData);
        }

}
