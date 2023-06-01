using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource track1;
 
    public AudioSource track2;
  
    public AudioSource track3;

    public int trackselector;

    public int trackhistory;
    // Start is called before the first frame update
    void Start()
    {
        trackselector = Random.Range(0, 3);

        if (trackselector == 0)
        {
            track1.Play();
            trackhistory = 1;
        }
        else if (trackselector == 1)
        {
           track2.Play();
            trackhistory = 2;
        }
        else if (trackselector == 2)
        {
           track3.Play();
            trackhistory = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( track1.isPlaying == false && track2.isPlaying == false && track3.isPlaying == false)
        {

            trackselector = Random.Range(0, 3);

            if (trackselector == 0 && trackhistory != 1)
            {
                track1.Play();
                trackhistory = 1;
            }
            else if (trackselector == 1 && trackhistory != 2)
            {
                track2.Play();
                trackhistory = 2;
            }
            else if (trackselector == 2 && trackhistory != 3)
            {
                track3.Play();
                trackhistory = 3;
            }
        }
    }
}
