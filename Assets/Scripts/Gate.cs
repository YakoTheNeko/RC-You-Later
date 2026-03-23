using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    // We define the method "signature" for our event.
    // https://www.dotnetperls.com/delegate
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates
    public delegate void OnGatePassed(Gate gate);

    // Then we defined a public event of this type.
    // https://www.dotnetperls.com/event
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
    public static event OnGatePassed OnPassed = null;

    private TextMeshPro label = null;

    [SerializeField]
    private string filteringTag = "Player";

    [SerializeField]
    private GameObject activeStatusObject = null;
    private new Collider collider = null;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        label = GetComponentInChildren<TextMeshPro>();
    }

    public void SetGateLabel(string text)
    {
        label.SetText(text);
    }

    public void ActivateGate(bool activate)
    {
        collider.enabled = activate;
        activeStatusObject.SetActive(activate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(filteringTag))
        {
            // If someone is registered to our event, call it.
            OnPassed?.Invoke(this);
        }
    }
}
