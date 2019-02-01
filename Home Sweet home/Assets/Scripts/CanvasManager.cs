using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager inst;

    [SerializeField] Image _fade_back, _fade_front;
    [Space(10)]
    [Header("Death")]
    [SerializeField] GameObject _death;
    [SerializeField] TextMeshProUGUI _die;
    [SerializeField] TextMeshProUGUI _desc;

    [Space(10)]
    [Header("Pause")]
    [SerializeField] GameObject _pause;
    [SerializeField] TextMeshProUGUI _pause_text;
    [SerializeField] Button _button_resume, _button_menu;

    [Space(10)]
    [Header("Options")]
    [SerializeField] GameObject _options;
    [SerializeField] TextMeshProUGUI _options_text;
    [SerializeField] Button _button_return;
    [SerializeField] Graphic[] _controlles;
    [SerializeField] Graphic[] _sliderMusic;
    [SerializeField] Graphic[] _sliderSFX;

    [Space(10)]
    [Header("Menu")]
    [SerializeField] GameObject _menu;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] Button _button_play, _button_options, _button_quit;
    [SerializeField] Image _ggjLogo;
    [SerializeField] Graphic[] _names;

    float remainingTime;

    bool end = false;
    bool _lanching = false;

    float _time;
    float _timeBeforeInteractAfterEnd = .6f;

    string[] deathReason = new string[] { "You've reached the end of your shelf life",
                                                           "It's not cool anymore",
                                                           "",
                                                           "BEEP BEEP BEEP",
                                                           "What a shock !",
                                                           "And you won't be alone ever again.",
                                                           "But at least you have a sweet swimming pool !",
                                                           "What a rotten end !",
                                                           "And you died.",

                                                        };

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        StartCoroutine(StartFade());
        _button_play.Select();
    }
    
    IEnumerator StartFade()
    {
        _fade_front.gameObject.SetActive(true);
        _fade_back.gameObject.SetActive(true);
        _menu.SetActive(true);

        yield return UIAnimation.Fade(_fade_front, FadeType.Out, .4f);
        _fade_front.gameObject.SetActive(false);

        
    }

    void Update()
    {
        if (Input.anyKeyDown && GameManager.inst.states == GameManager.GameStates.end && _time < Time.unscaledTime)
        {
            Menu();
        }

        if ((Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.Escape)))
        {
            if (GameManager.inst.states == GameManager.GameStates.playing)
                Pause();
            else if (GameManager.inst.states == GameManager.GameStates.paused)
                Unpause();
        }
    }

    private string Get_dialog(int index)
    {
        Debug.Log(index);
        return index < deathReason.Length ? deathReason[index] : "";
    }

    public void DisplayDieDialogue(int deathDesc)
    {
        if (end)
            return;

        _time = Time.unscaledTime + _timeBeforeInteractAfterEnd;

        end = true;
        _desc.text = Get_dialog(deathDesc);
        GameManager.inst.states = GameManager.GameStates.end;
        StartCoroutine(DisplayDieAnim());
    }
    public void DisplayWonDialogue()
    {
        if (end)
            return;

        _time = Time.unscaledTime + _timeBeforeInteractAfterEnd;

        end = true;
        _die.text = "Home sweet home";
        _desc.text = "You succeed to keep it sweet";
        GameManager.inst.states = GameManager.GameStates.end;
        StartCoroutine(DisplayDieAnim());
    }
    public void HideMenu()
    {
        if (!_lanching)
        {
            _lanching = true;
            StartCoroutine(LaunchGame());
        }
    }

    public void Pause()
    {
        if (GameManager.inst.states == GameManager.GameStates.starting)
            return;

        Time.timeScale = 0;
        GameManager.inst.states = GameManager.GameStates.paused;
        _pause.gameObject.SetActive(true);

        {
            _button_menu.Select();
            _button_resume.Select();
        }

        StartCoroutine(DisplayPauseAnim());
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        StartCoroutine(HidePauseAnim());
    }

    public void DisplayOptions()
    {        

        StartCoroutine(DisplayOptionsAnim());
    }

    public void HideOptions()
    {
        StartCoroutine(HideOptionsAnim());
    }

    IEnumerator DisplayDieAnim()
    {
        _death.gameObject.SetActive(true);
        float duration = .4f;
        yield return UIAnimation.Fade(_fade_back, FadeType.In, duration, .6f);
        yield return UIAnimation.Fade(_die, FadeType.In, duration);
        yield return UIAnimation.Fade(_desc, FadeType.In, duration);
    }
    IEnumerator DisplayPauseAnim()
    {
        yield return UIAnimation.Fade(_fade_back, FadeType.In, .2f, .6f);
        yield return UIAnimation.Fade(_pause_text, FadeType.In, .3f);
        yield return UIAnimation.FadeButton(_button_resume, FadeType.In, .3f);
        yield return UIAnimation.FadeButton(_button_menu, FadeType.In, .3f);
    }
    IEnumerator HidePauseAnim()
    {
        GameManager.inst.states = GameManager.GameStates.playing;
        yield return UIAnimation.FadeButton(_button_menu, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_pause_text, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_resume, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_fade_back, FadeType.Out, .2f);

        yield return new WaitForSeconds(.1f);
        _pause.gameObject.SetActive(false);
    }

    IEnumerator LaunchGame()
    {
        yield return HideMenuAnim();
        yield return UIAnimation.Fade(_fade_back, FadeType.Out, .2f);
        yield return GameManager.inst.states = GameManager.GameStates.playing;
    }

    IEnumerator HideMenuAnim(bool hide = true)
    {
        yield return UIAnimation.FadeButton(_button_quit, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_options, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_play, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_ggjLogo, FadeType.Out, .2f);
        yield return UIAnimation.FadeGroup(_names, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_title, FadeType.Out, .2f);

        if (hide)
            _menu.SetActive(false);
    }

    IEnumerator DisplayMenuAnim()
    {
        _menu.SetActive(true);

        yield return UIAnimation.FadeButton(_button_quit, FadeType.In, .2f);
        yield return UIAnimation.FadeButton(_button_options, FadeType.In, .2f);
        yield return UIAnimation.FadeButton(_button_play, FadeType.In, .2f);
        yield return UIAnimation.Fade(_ggjLogo, FadeType.In, .2f);
        yield return UIAnimation.FadeGroup(_names, FadeType.In, .2f);
        yield return UIAnimation.Fade(_title, FadeType.In, .2f);
    }

    IEnumerator DisplayOptionsAnim()
    {
        yield return UIAnimation.FadeButton(_button_quit, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_play, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_options, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_ggjLogo, FadeType.Out, .2f);
        yield return UIAnimation.FadeGroup(_names, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_title, FadeType.Out, .2f);

        _options.SetActive(true);
        _button_return.Select();
        _menu.SetActive(false);

        yield return UIAnimation.FadeGroup(_controlles, FadeType.In, .2f);
        yield return UIAnimation.FadeGroup(_sliderMusic, FadeType.In, .2f);
        yield return UIAnimation.FadeGroup(_sliderSFX, FadeType.In, .2f);
        yield return UIAnimation.Fade(_options_text, FadeType.In, .2f);
        yield return UIAnimation.FadeButton(_button_return, FadeType.In, .2f);
    }

    IEnumerator HideOptionsAnim()
    {
        yield return UIAnimation.FadeGroup(_controlles, FadeType.Out, .2f);
        yield return UIAnimation.FadeGroup(_sliderMusic, FadeType.Out, .2f);
        yield return UIAnimation.FadeGroup(_sliderSFX, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_options_text, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_return, FadeType.Out, .2f);

        _menu.SetActive(true);
        _button_play.Select();
        _options.SetActive(false);
        yield return DisplayMenuAnim();
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        if (_lanching && GameManager.inst.states == GameManager.GameStates.starting)
            return;

        Application.Quit();
    }

    [SerializeField] private AudioMixer _audioMixerSFX = null, _audioMixerMusic;
    public void SFXSetVolume(Slider slider)
    {
        _audioMixerSFX.SetFloat("Volume", slider.value <= 0 ? -80 : slider.value * 5f - 30f);
    }
    public void MusicSetVolume(Slider slider)
    {
        _audioMixerMusic.SetFloat("Volume", slider.value <= 0 ? -80 : slider.value * 5f - 30f);
    }
}
