using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text fpsText;

    private void Awake()
    {
        fpsText = GetComponent<Text>();
        StartCoroutine(UpdateFramesPerSecond());
    }

    private float frameLengthTotal = 0f;
    private int framesLookedAt = 0;

    // Update is called once per frame
    void Update ()
    {
        frameLengthTotal += Time.deltaTime;
        framesLookedAt++;
    }

    private IEnumerator UpdateFramesPerSecond()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            //print(framesLookedAt + " " + frameLengthTotal);
            fpsText.text = Mathf.RoundToInt(1f / (frameLengthTotal / framesLookedAt)).ToString();
            frameLengthTotal = 0f;
            framesLookedAt = 0;
        }
    }
}
