using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    bool paused = false;
    public GameObject optionsPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    GameObject player;
    public TextMeshProUGUI eggText;
    int eggTotal;
    int eggCount = 0;
    public Slider volumeBar;
    public string musicName;

    // Start is called before the first frame update
    void Start()
    {
        UnPause();

        eggTotal = GameObject.Find("Eggs").transform.childCount;
        player = GameObject.Find("Player");

        MakeEggsUI();
        SetVolumeBar();

        SoundManager.instance.StopAllSounds();
        PlaySound(musicName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                optionsPanel.SetActive(true);
                Pause();
            }
            else
            {
                paused = false;
                optionsPanel.SetActive(false);
                UnPause();
            }
        }

        if (player.GetComponent<PlayerCollision>().GetLifePoints() < 0)
            LoseGame();
    }

    void MakeEggsUI()
    {
        eggText.text = eggCount + " / " + eggTotal;
    }

    public void EggCollected()
    {
        eggCount++;
        MakeEggsUI();
        if (eggCount >= eggTotal)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winPanel.SetActive(true);
        Pause();
    }

    void LoseGame()
    {
        losePanel.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        GameController.instance.PauseGame();
    }

    public void UnPause()
    {
        paused = false;
        optionsPanel.SetActive(false);
        GameController.instance.UnPauseGame();
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
        volumeBar.value = SoundManager.instance.mainVolume;
    }
}
