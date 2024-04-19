using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using FMOD.Studio;

public class EventController : MonoBehaviour
{
    public EventReference eventReference;
    public EventInstance _instance;
    public EventDescription eventDescription;

    private PARAMETER_ID _id_lowboost;
    private PARAMETER_DESCRIPTION _lowBoost;

    private void Awake()
    {
        _instance = RuntimeManager.CreateInstance(eventReference);
        _instance.getDescription(out eventDescription);
        _instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    private void Start()
    {
        FMOD.Studio.System.create(out var system);
        system.update();
        system.flushCommands();
        eventDescription.getParameterDescriptionByName("LowBoost", out _lowBoost);
        _id_lowboost = _lowBoost.id;
    }

    public void Play()
    {
        _instance.start();
    }

    public void Stop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetTimelinePosition(int milliseconds)
    {
        _instance.setTimelinePosition(milliseconds); //Setting the timeline position before _instance.start() does play the song from the set position.
    }

    public void SetBassBoost(bool on)
    {
        _instance.setParameterByID(_id_lowboost, on ? 1 : 0);
    }

    //public void Rewind(bool forward = false)
    //{
    //    _instance.getTimelinePosition(out int milliseconds);
    //    _instance.setTimelinePosition(milliseconds + (forward ? 1 : -1));
    //}

    void OnDestroy()
    {
        _instance.release();
    }
}
