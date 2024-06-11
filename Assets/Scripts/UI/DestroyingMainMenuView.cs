using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyingMainMenuView : MonoBehaviour
{
    [SerializeField] GameObject[] Pages;
    private int CurrentPageIndex = 0;
    [SerializeField] Button NextPageButton;
    [SerializeField] Button PreviousPageButton;
    [SerializeField] Image[] NumbersBackground;
    [SerializeField] Sprite InactiveNumberSprite;
    [SerializeField] Sprite ActiveNumberSprite;
    private void Start()
    {
        PreviousPageButton.gameObject.SetActive(false);
        NumbersBackground[0].sprite = ActiveNumberSprite;
    }
    private void HideAllPages()
    {
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].SetActive(false);
            NumbersBackground[i].sprite = InactiveNumberSprite;
        }
    }
    public void GoNextPage()
    {
        HideAllPages();
        CurrentPageIndex++;
        Pages[CurrentPageIndex].SetActive(true);
        NumbersBackground[CurrentPageIndex].sprite = ActiveNumberSprite;
        CheckLastPageAndHideButtons();
    }
    private void CheckLastPageAndHideButtons()
    {
        PreviousPageButton.gameObject.SetActive(true);
        NextPageButton.gameObject.SetActive(true);
        if (CurrentPageIndex == Pages.Length-1)
        {
            NextPageButton.gameObject.SetActive(false);
        }
        if (CurrentPageIndex == 0)
        {
            PreviousPageButton.gameObject.SetActive(false);
        }
    }
    public void GoPreviousPage()
    {
        HideAllPages();
        CurrentPageIndex--;
        Pages[CurrentPageIndex].SetActive(true);
        NumbersBackground[CurrentPageIndex].sprite = ActiveNumberSprite;
        CheckLastPageAndHideButtons();
    }
    public void GoToPage(int index)
    {
        CurrentPageIndex = index;
        HideAllPages();
        Pages[CurrentPageIndex].SetActive(true);
        NumbersBackground[CurrentPageIndex].sprite = ActiveNumberSprite;
        CheckLastPageAndHideButtons();
    }
}
