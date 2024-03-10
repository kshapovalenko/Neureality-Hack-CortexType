using UnityEngine;

public static class Presets
{

    // LabStreamingLayer settings
    public static string PredictionLSLInletStreamName = "CortexTypePredictionLSL";

    public static string EventMarkerLSLOutletStreamName = "CortexTypeP300SpellerEventMarkerLSL";
    public static string EventMarkerLSLOutletStreamType = "EventMarker";
    public static string EventMarkerLSLOutletStreamID = "1";
    public static int EventMarkerChannelNum = 8;
    public static float EventMarkerNominalSamplingRate = 1;

    

    public enum EventMarkerChannelInfo
    {
        
        FlashingTrailMarkerIndex = 0, // Train 1, -1 Test 2, -2
        FlashingMarkerIndex = 1, // 1 means active
        FlashingRowOrColumnMarkerIndex = 2, // 1 means row, 2 means column
        FlashingRowOrColumnIndexMarkerIndex = 3, // 0 - 5 which indicates the index of the row or column
        FlashingTargetMarkerIndex = 4, // 1 is target, 0 is non-target

        TrainMarkerIndex = 5, // send 1 to trigger start 
        TestMarkerIndex = 6, // send 1 to trigger end
        InterruptMarkerIndex = 7, // interrupt the train or test function

    }

    public enum TrailMarker
    {
        TrainMarker = 1, // 
        TestMarker = 2
    }

    public enum Letters
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5,
        G = 6,
        H = 7,
        I = 8,
        J = 9,
        K = 10,
        L = 11,
        M = 12,
        N = 13,
        O = 14,
        P = 15,
        Q = 16,
        R = 17,
        S = 18,
        T = 19,
        U = 20,
        V = 21,
        W = 22,
        X = 23,
        Y = 24,
        Z = 25,
        Zero = 26,
        One = 27,
        Two = 28,
        Three = 29,
        Four = 30,
        Five = 31,
        Six = 32,
        Seven = 33,
        Eight = 34,
        Nine = 35
    }


    public enum GameState
    {
        IdleState = 0,
        TrainState = 1,
        TestState = 2,
    }

    public enum BoardState
    {
        IdelState,
        RuningState,
    }



    public static float BoardEnableWaitTime = 3.0f;
    public static float LetterTrainHintHighlightDuration = 2.0f;
    public static float WaitDurationBeforeStartFlashing = 2.0f;
    public static float FlashEndRestDuration = 3.0f;


    public static float WaitFeedbackDuration = 4.0f;
    public static float HighLightDuration = 2.0f;


    public static Color LetterOffColor = new Color(0.5f, 0.5f, 0.5f, 1);
    public static Color LetterOnColor = new Color(1, 1, 1, 1);

    public static Color LetterTrainHintHighlightColor = new Color(0, 1, 0, 1);
    public static Color LetterPredictionHighlightColor = new Color(1, 0, 0, 1);




    // key settings
    public static KeyCode InterruptKey = KeyCode.Escape;


}
