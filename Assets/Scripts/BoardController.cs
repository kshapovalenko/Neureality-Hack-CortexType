using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Presets;

public class BoardController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Letter Controller")]
    public LetterController A;
    public LetterController B;
    public LetterController C;
    public LetterController D;
    public LetterController E;
    public LetterController F;
    public LetterController G;
    public LetterController H;
    public LetterController I;
    public LetterController J;
    public LetterController K;
    public LetterController L;
    public LetterController M;
    public LetterController N;
    public LetterController O;
    public LetterController P;
    public LetterController Q;
    public LetterController R;
    public LetterController S;
    public LetterController T;
    public LetterController U;
    public LetterController V;
    public LetterController W;
    public LetterController X;
    public LetterController Y;
    public LetterController Z;
    public LetterController Zero;
    public LetterController One;
    public LetterController Two;
    public LetterController Three;
    public LetterController Four;
    public LetterController Five;
    public LetterController Six;
    public LetterController Seven;
    public LetterController Eight;
    public LetterController Nine;

    [Header("Board Grid")]
    public LetterController[,] boardControllers = new LetterController[6, 6];


    [Header("Train Configuration")]
    public List<Presets.Letters> trainLetters;
    public int repeatTimesTrain;
    public float flashDurationTrain;
    public float flashIntervelTrain;

    [Header("Test Configuration")]
    public int repeatTimesTest;
    public float flashDurationTest;
    public float flashIntervelTest;

    [Header("Board Buttons")]
    public Button ContinueButton;

    [Header("Game Manager")]
    public GameManager gameManager;

    // a list of list
    List<List<int>> flashingSequence  = new List<List<int>>() { 
        // first element indicate the row and column, the second element indicate the index
        new List<int> { 1, 0 },
        new List<int> { 1, 1 },
        new List<int> { 1, 2 },
        new List<int> { 1, 3 },
        new List<int> { 1, 4 },
        new List<int> { 1, 5 },

        new List<int> { 2, 0 },
        new List<int> { 2, 1 },
        new List<int> { 2, 2 },
        new List<int> { 2, 3 },
        new List<int> { 2, 4 },
        new List<int> { 2, 5 },

    };
    
    // create the Presets.Letters AND LetterController dictionary
    public Dictionary<Presets.Letters, LetterController> letterControllerDict = new Dictionary<Presets.Letters, LetterController>();


    [Header("Train Test IEnumerator")]
    public IEnumerator trainCoroutine;
    public IEnumerator testCoroutine;



    void Start()
    {
        ContinueButton.onClick.AddListener(ContinueButtonClicked);
        // get all children of this object
        boardControllers[0,0] = A;
        boardControllers[0,1] = B;
        boardControllers[0,2] = C;
        boardControllers[0,3] = D;
        boardControllers[0,4] = E;
        boardControllers[0,5] = F;
        
        boardControllers[1,0] = G;
        boardControllers[1,1] = H;
        boardControllers[1,2] = I;
        boardControllers[1,3] = J;
        boardControllers[1,4] = K;
        boardControllers[1,5] = L;
        
        boardControllers[2,0] = M;
        boardControllers[2,1] = N;
        boardControllers[2,2] = O;
        boardControllers[2,3] = P;
        boardControllers[2,4] = Q;
        boardControllers[2,5] = R;
        
        boardControllers[3,0] = S;
        boardControllers[3,1] = T;
        boardControllers[3,2] = U;
        boardControllers[3,3] = V;
        boardControllers[3,4] = W;
        boardControllers[3,5] = X;
        
        boardControllers[4,0] = Y;
        boardControllers[4,1] = Z;
        boardControllers[4,2] = Zero;
        boardControllers[4,3] = One;
        boardControllers[4,4] = Two;
        boardControllers[4,5] = Three;
        
        boardControllers[5,0] = Four;
        boardControllers[5,1] = Five;
        boardControllers[5,2] = Six;
        boardControllers[5,3] = Seven;
        boardControllers[5,4] = Eight;
        boardControllers[5,5] = Nine;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                LetterController letterController = boardControllers[i, j];
                letterControllerDict.Add(letterController.letter, letterController);
            }
        }
               
    }


    public void SetBoardTrainConfig(List<Presets.Letters> trainLetters, int repeatTimesTrain, float flashDurationTrain, float flashIntervelTrain)
    {
        // copy the trainLetters

        this.trainLetters = new List<Presets.Letters>(trainLetters);
        this.repeatTimesTrain = repeatTimesTrain;
        this.flashDurationTrain = flashDurationTrain;
        this.flashIntervelTrain = flashIntervelTrain;

    }

    public void SetBoardTestConfig(int repeatTimesTest, float flashDurationTest, float flashIntervelTest)
    {
        this.repeatTimesTest = repeatTimesTest;
        this.flashDurationTest = flashDurationTest;
        this.flashIntervelTest = flashIntervelTest;
    }


    // Update is called once per frame
    void Update()
    {



    }


    /// <summary>
    /// This function is called when the continue button is clicked
    /// The function will start the train or test IEnumerator based on the game state
    /// </summary>
    public void ContinueButtonClicked()
    {
        

        if (gameManager.gameState == Presets.GameState.TrainState)
        {

            if (trainLetters.Count == 0)
            {
                gameManager.SetIdleState();

                // send the train marker to trigger train epochs function
                gameManager.eventMarkerLSLOutletController.SendTrainMarker();

                return;
            }
            else
            {
                Presets.Letters targetLetter = trainLetters[0];
                trainCoroutine = TrainIEnumerator(targetLetter);

                StartCoroutine(trainCoroutine);

                // remove the fist letter from the list
                trainLetters.RemoveAt(0);

            }

        }
        else if (gameManager.gameState == Presets.GameState.TestState)
        {
            testCoroutine = TestIEnumerator();
            StartCoroutine(testCoroutine);
        }
        DeactivateContinueButton();
    }



    /// <summary>
    /// This IEnumerator will flash the board based on the train configuration set in the game manager
    /// </summary>
    /// <param name="targetLetter"></param>
    /// <returns></returns>
    public IEnumerator TrainIEnumerator(Presets.Letters targetLetter)
    {
        
        // send the train trail start marker
        gameManager.eventMarkerLSLOutletController.SendFlashingTrailStartMarker((float)Presets.TrailMarker.TrainMarker);

        SetAllLettersOffColor();

        LetterController targetLetterController = letterControllerDict[targetLetter];

        Debug.Log("TrainIEnumerator Start");
        yield return new WaitForSeconds(Presets.BoardEnableWaitTime);
        Debug.Log("Flash Start");

        

        int repeatTimes = repeatTimesTrain;

        // turn on the target letter hint
        targetLetterController.SetLetterTrainHintHighlightColor();
        yield  return new WaitForSeconds(Presets.LetterTrainHintHighlightDuration);
        targetLetterController.SetLetterOffColor();

        SetAllLettersOffColor();

        yield return new WaitForSeconds(Presets.WaitDurationBeforeStartFlashing);

        for(int i = 0; i < repeatTimes; i++)
        {

            // copy   flashingSequence
            List<List<int>> flashingSequenceCopy = new List<List<int>>(flashingSequence);

            Utils.Shuffle(flashingSequenceCopy);

            foreach (List<int> index in flashingSequenceCopy)
            {

                int raw_col_indicator = index[0];
                int index_indicator = index[1];
                bool target_flashing = false;

                if(raw_col_indicator ==1)
                {
                    // turn on the index_indicator row
                    for (int j = 0; j < 6; j++)
                    {
                        LetterController letterController = boardControllers[index_indicator, j];
                        letterController.SetLetterOnColor();

                        // if the target letter is on, set the target_flashing to true
                        if(targetLetterController == letterController)
                        {
                            target_flashing = true;
                        }
                    }
                }
                else if(raw_col_indicator == 2)
                {
                    // turn on the target column
                    for (int j = 0; j < 6; j++)
                    {
                        LetterController letterController = boardControllers[j, index_indicator];
                        letterController.SetLetterOnColor();

                        // if the target letter is on, set the target_flashing to true
                        if (targetLetterController == letterController)
                        {
                            target_flashing = true;
                        }
                    }
                }

                // the target is on
                // send the flashing marker
                // raw_col_indicator: 1 for row, 2 for column
                // index_indicator: the index of the row or column
                // target_flashing: 1 for target flashing, 0 for non-target flashing
                gameManager.eventMarkerLSLOutletController.SendFlashingMarker((float)raw_col_indicator, (float)index_indicator, target_flashing ? 1.0f : 0.0f);


                yield return new WaitForSeconds(flashDurationTrain);
                SetAllLettersOffColor();
                yield return new WaitForSeconds(flashIntervelTrain);


            }

        }

        yield return new WaitForSeconds(1.0f); 
        // send the train trail end marker
        gameManager.eventMarkerLSLOutletController.SendFlashingTrailEndMarker((float)Presets.TrailMarker.TrainMarker);


        yield return new WaitForSeconds(Presets.FlashEndRestDuration);
        ResetBoardGUI();


        // TODO: RPC call to the PhysioLabXR Scripting Interface to trigger some Training Event if needed.



    }

    /// <summary>
    /// This IEnumerator will flash the board based on the test configuration set in the game manager
    /// </summary>
    /// <returns></returns>

    public IEnumerator TestIEnumerator()
    {

        // send the test trail start marker
        gameManager.eventMarkerLSLOutletController.SendFlashingTrailStartMarker((float)Presets.TrailMarker.TestMarker);



        SetAllLettersOffColor();



        Debug.Log("TrainIEnumerator Start");
        yield return new WaitForSeconds(Presets.BoardEnableWaitTime);
        Debug.Log("Flash Start");



        int repeatTimes = repeatTimesTest;

        // turn on the target letter hint
        SetAllLettersOnColor();
        yield return new WaitForSeconds(Presets.LetterTrainHintHighlightDuration); // turn all the board on for a while before the flashing starts
        SetAllLettersOffColor();

        yield return new WaitForSeconds(Presets.WaitDurationBeforeStartFlashing);

        for (int i = 0; i < repeatTimes; i++)
        {

            // copy   flashingSequence
            List<List<int>> flashingSequenceCopy = new List<List<int>>(flashingSequence);

            Utils.Shuffle(flashingSequenceCopy);

            foreach (List<int> index in flashingSequenceCopy)
            {

                int raw_col_indicator = index[0];
                int index_indicator = index[1];

                if (raw_col_indicator == 1)
                {
                    // turn on the index_indicator row
                    for (int j = 0; j < 6; j++)
                    {
                        LetterController letterController = boardControllers[index_indicator, j];
                        letterController.SetLetterOnColor();
                    }

                }
                else if (raw_col_indicator == 2)
                {
                    // turn on the target column
                    for (int j = 0; j < 6; j++)
                    {
                        LetterController letterController = boardControllers[j, index_indicator];
                        letterController.SetLetterOnColor();
                    }
                }

                // send the flashing marker
                // raw_col_indicator: 1 for row, 2 for column
                // index_indicator: the index of the row or column
                // target_flashing: always 0 for test
                gameManager.eventMarkerLSLOutletController.SendFlashingMarker((float)raw_col_indicator, (float)index_indicator, 0.0f);

                yield return new WaitForSeconds(flashDurationTest);
                SetAllLettersOffColor();
                yield return new WaitForSeconds(flashIntervelTest);

            }

        }


        yield return new WaitForSeconds(1.0f);
        // send the test trail end marker
        gameManager.eventMarkerLSLOutletController.SendFlashingTrailEndMarker((float)Presets.TrailMarker.TestMarker);

        yield return new WaitForSeconds(Presets.FlashEndRestDuration);


        // this will trigger the predict() in PhysioLabXR Scripting Interface
        gameManager.eventMarkerLSLOutletController.SendTestMarker();
        gameManager.predictionLSLInletController.clearBuffer();

        bool predictionReceived = false;

        while (!predictionReceived)
        {
            gameManager.predictionLSLInletController.pullPredictionSample();
            if (gameManager.predictionLSLInletController.frameTimestamp != 0)
            {
                predictionReceived = true;
            }
            yield return new WaitForSeconds(0.2f); // wait for 0.2 second for pulling
        }


        // get the letter controller with predicted letter index in the board 
        LetterController predictedLetterController = boardControllers[
            (int)gameManager.predictionLSLInletController.frameDataBuffer[0],
            (int)gameManager.predictionLSLInletController.frameDataBuffer[1]
            ];

        // turn on the predicted char for a second
        predictedLetterController.SetLetterTrainHintHighlightColor();
        yield return new WaitForSeconds(2.0f);
        SetAllLettersOffColor();
        yield return new WaitForSeconds(1.0f);

        ResetBoardGUI();
    }


    public void ResetBoardGUI()
    {
        ActivateContinueButton();
        SetAllLettersOnColor();
    }

    public void ActivateContinueButton()
    {
        ContinueButton.gameObject.SetActive(true);
    }

    public void DeactivateContinueButton()
    {
        ContinueButton.gameObject.SetActive(false);
    }


    public void SetAllLettersOffColor()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                LetterController letterController = boardControllers[i, j];
                letterController.SetLetterOffColor();
            }
        }
    }

    public void SetAllLettersOnColor()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                LetterController letterController = boardControllers[i, j];
                letterController.SetLetterOnColor();
            }
        }
    }


    public void InterruptBoard()
    {
        if (trainCoroutine != null)
        {
            StopCoroutine(trainCoroutine);
            Debug.Log("Train Coroutine Stopped");

            // send interrupt signal
        }
        if (testCoroutine != null)
        {
            StopCoroutine(testCoroutine);
            Debug.Log("Test Coroutine Stopped");
        }

        ResetBoardGUI();

        // send interrupt signal via LSL
        gameManager.eventMarkerLSLOutletController.SendInterruptMarker();

    }

}
