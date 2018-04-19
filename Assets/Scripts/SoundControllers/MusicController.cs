using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField]
    AudioSource mainMusic;
    [SerializeField]
    AudioClip allianceMusic;
    [SerializeField]
    AudioClip hordeMusic;

    public void SwitchMusic(bool alliance)
    {
        if ((mainMusic.clip == allianceMusic) && !alliance)
            mainMusic.clip = hordeMusic;
        else if ((mainMusic.clip == allianceMusic) && alliance)
            return;
        else if ((mainMusic.clip != allianceMusic) && alliance)
            mainMusic.clip = allianceMusic;
        else
            return;
        mainMusic.Play();
    }

    public void ToggleMusic()
    {
        mainMusic.mute = !mainMusic.mute;
    }
}
