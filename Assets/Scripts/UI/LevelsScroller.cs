using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsScroller : MonoBehaviour {

    static readonly string[] LEVELS = { "tutorial skinnato Elisa", "tutorial skinnato Elisa", "tutorial skinnato Elisa" };

    List<RectTransform> levels = new List<RectTransform>();
    int showedLevel = 0;
    Button back;
    bool theySeeMeScrollinTheyHatin = false;
    bool onButton = false;

    void Awake() {
        foreach (Transform child in transform) { levels.Add(child.GetComponent<RectTransform>()); }
        var joystick = GameObject.FindObjectOfType<JoystickListener>();
        back = GameObject.Find("Back").GetComponent<Button>();
        joystick.OnJoystickLeft += () => StartCoroutine(ScrollRight());
        joystick.OnJoystickRight += () => StartCoroutine(ScrollLeft());
        joystick.OnJoystickDown += SelectBackButton;
        joystick.OnJoystickUp += DeselectBackButton;
        levels[showedLevel].gameObject.GetComponent<Button>().Select();
    }

    public void LaunchLevel() {
        SceneManager.LoadSceneAsync(LEVELS[showedLevel]);
        gameObject.SetActive(false);
    }

    public void Back() {
        SceneManager.LoadSceneAsync("Menu");
    }

    IEnumerator ScrollLeft() {
        if (showedLevel == levels.Count - 1) { yield break; }
        if (theySeeMeScrollinTheyHatin || onButton) { yield break; }
        theySeeMeScrollinTheyHatin = true;
        ++showedLevel;
        RectTransform level = levels[showedLevel];
        while (level.anchoredPosition.x > 0) {
            foreach (var lvl in levels) {
                lvl.anchoredPosition -= new Vector2(Time.deltaTime * 800, 0f);
            }
            yield return null;
        }
        foreach (var lvl in levels) {
            lvl.anchoredPosition -= new Vector2(level.anchoredPosition.x, 0f);
        }
        levels[showedLevel].gameObject.GetComponent<Button>().Select();
        theySeeMeScrollinTheyHatin = false;
    }

    IEnumerator ScrollRight() {
        if (showedLevel == 0) { yield break; }
        if (theySeeMeScrollinTheyHatin || onButton) { yield break; }
        theySeeMeScrollinTheyHatin = true;
        --showedLevel;
        RectTransform level = levels[showedLevel];
        while (level.anchoredPosition.x < 0) {
            foreach (var lvl in levels) {
                lvl.anchoredPosition += new Vector2(Time.deltaTime * 800, 0f);
            }
            yield return null;
        }
        foreach (var lvl in levels) {
            lvl.anchoredPosition -= new Vector2(level.anchoredPosition.x, 0f);
        }
        levels[showedLevel].gameObject.GetComponent<Button>().Select();
        theySeeMeScrollinTheyHatin = false;
    }

    void SelectBackButton() {
        if (onButton || theySeeMeScrollinTheyHatin) { return; }
        onButton = true;
        back.Select();
    }

    void DeselectBackButton() {
        if (!onButton || theySeeMeScrollinTheyHatin) { return; }
        onButton = false;
        levels[showedLevel].gameObject.GetComponent<Button>().Select();
    }
}
