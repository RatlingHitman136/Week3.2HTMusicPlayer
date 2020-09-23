using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Track[] Tracks;
    public Scrollbar Volume;
    public Image SongPreview;
    public Text Play_Pause;
    public Text SongNameTextSpace;
    public Animator NameAnimator;
    int currentSon;

    void Start()
    {
        currentSon = 0;
        this.GetComponent<AudioSource>().clip = Tracks[currentSon].Clip;
        SongPreview.sprite = Tracks[currentSon].SongPreview;
        NameAnimator.SetBool("Up", true);   
        this.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        this.GetComponent<AudioSource>().volume = Volume.value;
        if(NameAnimator.GetBool("Up"))
            SongNameTextSpace.text = Tracks[currentSon].Text;
    }

    public void NextSong()
    {
        currentSon = (currentSon + 1) % Tracks.Count();
        this.GetComponent<AudioSource>().clip = Tracks[currentSon].Clip;
        SongPreview.sprite = Tracks[currentSon].SongPreview;
        if(NameAnimator.GetBool("Up"))
        {
            NameAnimator.SetBool("Up", false);
            Invoke("UpText", 0.5f);
        }
        else
            NameAnimator.SetBool("Up", true);
        this.GetComponent<AudioSource>().Play();
    }

    public void PrevSong()
    {
        currentSon = currentSon == 0 ? Tracks.Count() - 1 : currentSon - 1;
        this.GetComponent<AudioSource>().clip = Tracks[currentSon].Clip;
        SongPreview.sprite = Tracks[currentSon].SongPreview;
        if (NameAnimator.GetBool("Up"))
        {
            NameAnimator.SetBool("Up", false);
            Invoke("UpText", 0.05f);
        }
        else
            NameAnimator.SetBool("Up", true);
        this.GetComponent<AudioSource>().Play();

    }

    void UpText()
    {
        NameAnimator.SetBool("Up", true);
    }
    public void PauseSong()
    {
        if (Play_Pause.text == "Pause")
        {
            NameAnimator.SetBool("Up",false);
            Play_Pause.text = "Play";
            this.GetComponent<AudioSource>().Pause();
        }
        else if (Play_Pause.text == "Play")
        {
            NameAnimator.SetBool("Up", true);
            Play_Pause.text = "Pause";
            this.GetComponent<AudioSource>().Play();
        }
    }
}
