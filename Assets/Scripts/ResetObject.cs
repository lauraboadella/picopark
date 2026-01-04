using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActive;

    private void Awake()
    {
        // guardado de estado incial
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialActive = gameObject.activeSelf;
    }

    // reseteo de todo a estado inicial otra ve
    public void Reset()
    {
        Debug.Log("se hace reset en resetobject");
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(initialActive);
    }
}
