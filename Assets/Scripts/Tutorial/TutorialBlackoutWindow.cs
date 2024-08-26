using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialBlackoutWindow : MonoBehaviour
{

    [SerializeField] GameObject BlackoutWindow;
    [SerializeField] GameObject ChangeModeButton;
    [SerializeField] GameObject ChangeModeParent;
    [SerializeField] GameObject MultiplatformCanvas;
    [SerializeField] GameObject TutorialGuider;
    [SerializeField] GameObject BlockPanel;

    private int _BlockPanelSiblingIndex;
    public void ShowBlackoutOnChangeMode()
    {
        StartCoroutine(ShowAfterDelay());
        IEnumerator ShowAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            if (TutorialManager.instance.Phase7Objects.activeInHierarchy == false)
            {
                BlackoutWindow.SetActive(true);
                BlackoutWindow.GetComponent<Image>().DOFade(0,0);
                BlackoutWindow.GetComponent<Image>().DOFade(0.7f,0.5f);
                BlackoutWindow.transform.parent = MultiplatformCanvas.transform;
                BlackoutWindow.transform.SetAsLastSibling();
                TutorialGuider.transform.SetAsLastSibling();
                ChangeModeButton.transform.parent = MultiplatformCanvas.transform;
                ChangeModeButton.transform.SetAsLastSibling();
            }   
        }
      
    }
    public void HideBlackoutOnChangeMode()
    {
        BlackoutWindow.SetActive(false);
        TutorialGuider.transform.SetAsFirstSibling();
        ChangeModeButton.transform.parent = ChangeModeParent.transform;
    }
    public void ShowBlackoutOnChooseBlock()
    {
        _BlockPanelSiblingIndex =  BlockPanel.transform.GetSiblingIndex();
        BlackoutWindow.SetActive(true);
        BlackoutWindow.GetComponent<Image>().DOFade(0.7f, 0);
        BlackoutWindow.transform.parent = MultiplatformCanvas.transform;
        BlackoutWindow.transform.SetAsLastSibling();
        TutorialGuider.transform.SetAsLastSibling();
        BlockPanel.transform.parent = MultiplatformCanvas.transform;
        BlockPanel.transform.SetAsLastSibling();
    }
    public void HideBlackoutOnChooseBlock()
    {
        BlackoutWindow.SetActive(false);
        TutorialGuider.transform.SetAsFirstSibling();
        BlockPanel.transform.parent = MultiplatformCanvas.transform;
        BlockPanel.transform.SetSiblingIndex(_BlockPanelSiblingIndex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShowBlackoutOnChangeMode();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            HideBlackoutOnChangeMode();
        }
    }
}
