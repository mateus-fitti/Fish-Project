using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Slider volumeBar;
    public string musicName;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.UnPauseGame();
        SetVolumeBar();

        if (musicName != null && musicName != "")
        {
            SoundManager.instance.StopAllSounds();
            PlaySound(musicName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonScene(string sceneName)
    {
        GameController.instance.OnSceneChange(sceneName);
    }

    public void PlaySound(string name)
    {
        SoundManager.instance.Play(name);
    }

    public void SetVolume(Slider bar)
    {
        SoundManager.instance.SetVolume(bar);
    }

    void SetVolumeBar()
    {
        if (volumeBar != null)
            volumeBar.value = SoundManager.instance.mainVolume;
    }
}
