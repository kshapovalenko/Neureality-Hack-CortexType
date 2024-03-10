using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLSLInletController : LSLInletInterface
{
    // Start is called before the first frame update
    void Start()
    {
        streamName = Presets.PredictionLSLInletStreamName;
        StartContinousResolver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void pullPredictionSample()
    {
        if (streamActivated)
        {
            pullSample();
            clearBuffer();
        }
    }


}
