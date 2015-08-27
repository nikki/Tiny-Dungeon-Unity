using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Transitions : MonoBehaviour {

    public bool isActive = false;

    public enum Direction {
        Left,
        Right,
        Top,
        Bottom
    }
    public Direction slideDirection;

    void TogglePanel(GameObject panel, bool enable) {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.interactable = enable ? true : false;
        group.blocksRaycasts = enable ? true : false;
    }

    void TogglePanels(GameObject hide, GameObject show) {
        TogglePanel(hide, false);
        TogglePanel(show, true);
    }

    void ShowPanel(GameObject panel, bool show) {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = show ? 1 : 0;
    }

    public void SlideIn(GameObject to) {
        // set enabled
        TogglePanels(gameObject, to);

        // get rect refs
        RectTransform fromRect = gameObject.GetComponent<RectTransform>();
        RectTransform toRect = to.GetComponent<RectTransform>();

        // set initial position
        ShowPanel(to, true);
        toRect.anchoredPosition = new Vector2((float)Screen.width * Game.scale, 0f);

        // animate positions
        fromRect.DOAnchorPos(new Vector2(-(float)Screen.width * Game.scale, 0f), 2f, true).SetEase(Ease.OutQuint);
        toRect.DOAnchorPos(new Vector2(0f, 0f), 2f, true).SetEase(Ease.OutQuint);
    }

    public void CrossFade(GameObject to) {
        // set enabled
        TogglePanels(gameObject, to);

        // fade
        gameObject.GetComponent<CanvasGroup>().DOFade(0f, 2f).SetEase(Ease.OutQuint);
        to.GetComponent<CanvasGroup>().DOFade(1f, 2f).SetEase(Ease.OutQuint);
    }

    public void FadeToBlack(GameObject to) {
        // set enabled
        TogglePanels(gameObject, to);

        // fade
        gameObject.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.OutQuint);
        to.GetComponent<CanvasGroup>().DOFade(1f, 1f).SetEase(Ease.OutQuint).SetDelay(1f);
    }

    public void GoBack(GameObject to) {
        // clear all tweens
        DOTween.Clear();

        // reset position
        to.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

        // set enabled
        TogglePanels(gameObject, to);

        // show/hide
        ShowPanel(gameObject, false);
        ShowPanel(to, true);
    }

    void Start () {
        // show/hide panel on start
        if (!isActive) {
            TogglePanel(gameObject, false);
            ShowPanel(gameObject, false);
        }
    }
}
