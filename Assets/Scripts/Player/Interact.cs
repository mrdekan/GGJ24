using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    private IInteractable lastInteractable;
    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 2), 0)), out RaycastHit hit, Mathf.Infinity, _layers);
        lastInteractable?.SetOutline(false);
        if (hit.collider != null)
        {
            lastInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
            lastInteractable?.SetOutline(true);
            if (Input.GetMouseButtonDown(0))
                lastInteractable?.Interact();
        }
    }
}
