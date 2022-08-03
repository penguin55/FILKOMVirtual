using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject parentPage;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject[] pages;

    private GameObject currentPageOpenend;
    private GameObject lastPageOpenend;
    private int currentPage;

    public void OpenTutorial(bool open)
    {
        parentPage.SetActive(open);
        if (open)
        {
            currentPage = 0;
            RenderTutorial();
        } else
        {
            if (lastPageOpenend) lastPageOpenend.SetActive(false);
            if (currentPageOpenend) currentPageOpenend.SetActive(false);
            currentPage = 0;
        }
    }

    public void Next()
    {
        currentPage++;
        if (currentPage >= pages.Length) currentPage = pages.Length - 1;

        RenderTutorial();
    }

    public void Back()
    {
        currentPage--;
        if (currentPage < 0) currentPage = 0;

        RenderTutorial();
    }

    private void RenderTutorial()
    {
        lastPageOpenend = currentPageOpenend;
        currentPageOpenend = pages[currentPage];

        if (lastPageOpenend) lastPageOpenend.SetActive(false);
        currentPageOpenend.SetActive(true);

        if (currentPage == 0)
        {
            if (backButton.activeSelf) backButton.SetActive(false);
        }
        else
        {
            if (!backButton.activeSelf) backButton.SetActive(true);
        }

        if (currentPage == pages.Length - 1)
        {
            if (nextButton.activeSelf) nextButton.SetActive(false);
        }
        else
        {
            if (!nextButton.activeSelf) nextButton.SetActive(true);
        }
    }
}
