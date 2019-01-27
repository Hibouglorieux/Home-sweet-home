using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager inst;

    [SerializeField] Image _fade_back, _fade_front;
    [Space(10)]
    [Header("Death")]
    [SerializeField] GameObject _death;
    [SerializeField] Text _die;
    [SerializeField] Text _desc;

    [Space(10)]
    [Header("Pause")]
    [SerializeField] GameObject _pause;
    [SerializeField] Text _pause_text;
    [SerializeField] Button _button_resume, _button_menu;
    [Space(10)]
    [Header("Menu")]
    [SerializeField] GameObject _menu;
    [SerializeField] Text _title;
    [SerializeField] Button _button_play, _button_quit;
    float remainingTime;

    bool _lanching = false;

    string[] deathReason = new string[] { "With no place left to put your books in",
                                                           "",
                                                           "",
                                                           "",
                                                           "",
                                                           "",
                                                           "",
                                                           "",
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
        yield return UIAnimation.Fade(_fade_front, FadeType.Out, .6f);
        _fade_front.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown && GameManager.inst.states == GameManager.GameStates.end)
        {
            Menu();
        }

        if (Input.GetKeyDown("joystick button 7"))
        {
            if (GameManager.inst.states != GameManager.GameStates.paused)
                Pause();
            else Unpause();
        }
    }

    private string Get_dialog(int index)
    {
        Debug.Log(index);
        return index < deathReason.Length ? deathReason[index] : "";
    }

    public void DisplayDieDialogue(int deathDesc)
    {
        _desc.text = Get_dialog(deathDesc);
        GameManager.inst.states = GameManager.GameStates.end;
        StartCoroutine(DisplayDieAnim());
    }
    public void HideMenu()
    {
        if (!_lanching)
        {
            _lanching = true;
            StartCoroutine(HideMenuAnim());
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
        yield return UIAnimation.FadeButton(_button_menu, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_pause_text, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_resume, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_fade_back, FadeType.Out, .2f);

        yield return new WaitForSeconds(.1f);
        _pause.gameObject.SetActive(false);
        GameManager.inst.states = GameManager.GameStates.playing;
    }
    IEnumerator HideMenuAnim()
    {
        yield return UIAnimation.FadeButton(_button_quit, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_title, FadeType.Out, .2f);
        yield return UIAnimation.FadeButton(_button_play, FadeType.Out, .2f);
        yield return UIAnimation.Fade(_fade_back, FadeType.Out, .2f);

        yield return new WaitForSeconds(.1f);
        _menu.SetActive(false);
        GameManager.inst.states = GameManager.GameStates.playing;
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        if (_lanching && GameManager.inst.states == GameManager.GameStates.starting)
            return;

        Application.Quit();
    }
}
