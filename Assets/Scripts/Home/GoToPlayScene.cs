using UnityEngine;

public class GoToPlayScene : MonoBehaviour
{
    [SerializeField] private LoadingScene _sceneLoader;
    [SerializeField] private Checkpoint _checkpoint;
    private void Start() => _checkpoint.OnTrigger += Triggered;
    private void Triggered(Collider col) => _sceneLoader.LoadScene(1);
}
