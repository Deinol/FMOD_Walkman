using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkman : MonoBehaviour
{
    public EventController eventController;
    public Tape heldTape = null;
    public Transform tapeAnchor;

    public AnimatorBoolSetter capABS, wheelLeft, wheelRight;
    public AnimatorBoolSetter bassBoostButton;

    public bool isPlaying;

    private bool bassBoostEnabled;
    public bool BassBoostEnabled
    {
        get => bassBoostEnabled; set
        {
            bassBoostEnabled = value;
            eventController.SetBassBoost(BassBoostEnabled);
            bassBoostButton.SetParameter(BassBoostEnabled);
        }
    }

    public void PlayTape()
    {
        if (heldTape == null || isPlaying) return;
        isPlaying = true;
        eventController.SetTimelinePosition(heldTape.data.startingMillisecond);
        eventController.Play();
        wheelLeft.SetParameter(true);
        wheelRight.SetParameter(true);
    }

    public void EjectTape()
    {
        eventController.Stop();
        isPlaying = false;
        wheelLeft.SetParameter(false);
        wheelRight.SetParameter(false);
        capABS.SetParameter(false);
        Invoke(nameof(UnsocketTape), .5f);
    }

    public void ToggleBassBoost()
    {
        BassBoostEnabled = !BassBoostEnabled;
    }

    #region socketSystem
    private Tape hoveringTape;

    private void SocketTape(Tape tape)
    {
        heldTape = hoveringTape;
        if (tape.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        hoveringTape.transform.position = tapeAnchor.position;
        hoveringTape.transform.rotation = tapeAnchor.rotation;
    }
    private void UnsocketTape()
    {
        if (heldTape == null) return;
        Tape oldTape = heldTape;
        heldTape = null;
        oldTape.canBeSocketed = false;
        if (oldTape.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hoveringTape = other.GetComponent<Tape>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (heldTape != null) return;
        if (hoveringTape != null && hoveringTape.canBeSocketed)
        {
            SocketTape(hoveringTape);
            capABS.SetParameter(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hoveringTape != null && other.gameObject == hoveringTape.gameObject)
            hoveringTape = null;
    }
    #endregion
}
