using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public void Init()
    {
        if (!Game.Instance.Progress.CompletedTraining)
        {
            FileWorker.SaveProgress(new());
            FileWorker.SaveUserJokes(new());
            FileWorker.SaveSelectedJokes(new());
        }
    }
}
