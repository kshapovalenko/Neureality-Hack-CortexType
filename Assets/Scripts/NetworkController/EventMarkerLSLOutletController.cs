using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMarkerLSLOutletController : LSLOutletInterface
{
    // Start is called before the first frame update
    void Start()
    {
        initLSLStreamOutlet(
                            Presets.EventMarkerLSLOutletStreamName,
                            Presets.EventMarkerLSLOutletStreamType,
                            Presets.EventMarkerChannelNum,
                            Presets.EventMarkerNominalSamplingRate,
                            LSL.channel_format_t.cf_float32
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initEventMarkerLSLOutlet()
    {
        initLSLStreamOutlet(
                            Presets.EventMarkerLSLOutletStreamName,
                            Presets.EventMarkerLSLOutletStreamType,
                            Presets.EventMarkerChannelNum,
                            Presets.EventMarkerNominalSamplingRate,
                            LSL.channel_format_t.cf_float32
        );
    }


    public void SendFlashingTrailStartMarker(float blockMarker)
    {
        float[] eventMarkerArray = createEventMarkerArrayFloat();
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingTrailMarkerIndex] = blockMarker;
        streamOutlet.push_sample(eventMarkerArray);
    }

    public void SendFlashingTrailEndMarker(float blockMarker)
    {
        float[] eventMarkerArray = createEventMarkerArrayFloat();
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingTrailMarkerIndex] = blockMarker * -1.0f;
        streamOutlet.push_sample(eventMarkerArray);
    }

    public void SendFlashingMarker(float flashingRowOrColumnMarker, float flashingRowOrColumnIndexMarker, float flashingTargetMarker)
    {
        float[] eventMarkerArray = createEventMarkerArrayFloat();
        // flashing marker is 1 for all the time. It is used to indicate this is a flashing event
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingMarkerIndex] = 1.0f; 

        // flashingRowOrColumnMarker is 1 for row and 2 for column
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingRowOrColumnMarkerIndex] = flashingRowOrColumnMarker;

        // flashingRowOrColumnIndexMarker is the index of the row or column
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingRowOrColumnIndexMarkerIndex] = flashingRowOrColumnIndexMarker;

        // flashingTargetMarker is the index of the target
        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.FlashingTargetMarkerIndex] = flashingTargetMarker;
        streamOutlet.push_sample(eventMarkerArray);
    }

    public void SendTrainMarker()
    {
        float[] eventMarkerArray = createEventMarkerArrayFloat();

        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.TrainMarkerIndex] = 1.0f;

        streamOutlet.push_sample(eventMarkerArray);
    }

    public void SendTestMarker()
    {
        float[] eventMarkerArray = createEventMarkerArrayFloat();

        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.TestMarkerIndex] = 1.0f;

        streamOutlet.push_sample(eventMarkerArray);
    }


    public void SendInterruptMarker()
    {

        float[] eventMarkerArray = createEventMarkerArrayFloat();

        eventMarkerArray[(int)Presets.EventMarkerChannelInfo.InterruptMarkerIndex] = 1.0f;

        streamOutlet.push_sample(eventMarkerArray);
    }



}
