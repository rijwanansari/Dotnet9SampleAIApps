using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserBehaviorAPI.Models;
using UserBehaviorAPI.Services;

namespace UserBehaviorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBehaviourContoller : ControllerBase
    {
        //api to train the model
        [HttpPost]
        [Route("TrainModel")]
        public IActionResult TrainModel()
        {
            var userBehaviorModelTrainer = new UserBehaviorModelTrainer();
            userBehaviorModelTrainer.TrainModel();
            return Ok("Model trained successfully");
        }

        //api to predict if a user will click on an ad
        [HttpPost]
        [Route("Predict")]
        public IActionResult Predict(UserBehaviorData userBehaviorData)
        {
            var userBehaviorModelPrediction = new UserBehaviorModelPrediction();
            var prediction = userBehaviorModelPrediction.Predict(userBehaviorData);
            return Ok(prediction);
        }
    }
}
