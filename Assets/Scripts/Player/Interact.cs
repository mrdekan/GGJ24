using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 2), 0)), out RaycastHit hit, Mathf.Infinity, _layers);
            if (hit.collider != null)
            {
                var obj = hit.collider.gameObject.GetComponent<IInteractable>();
                obj?.Interact();
            }
        }
    }
}
