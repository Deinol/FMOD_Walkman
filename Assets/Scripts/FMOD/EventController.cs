using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class EventController : MonoBehaviour
{
    public EventReference eventReference;

    [SerializeField]
    public FMOD.Studio.EventInstance eventInstance;

    private void Awake()
    {
        eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.getDescription(out FMOD.Studio.EventDescription eventDescription);
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    private void Start()
    {
        FMOD.Studio.System.create(out var system);
        system.update();
        system.flushCommands();
    }

    public void Play()
    {
        eventInstance.start();
    }

    public void Stop()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void OnDestroy()
    {
        eventInstance.release();
    }
}
