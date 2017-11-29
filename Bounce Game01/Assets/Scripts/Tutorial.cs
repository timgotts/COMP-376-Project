using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public List<GameObject> slides;
    public Button buttonNext;
    public Button buttonPrev;
    public Transform posTemp;


    Transform origNext;
    Transform orignPrev;

    int index = 0;

    bool shouldCheck;

    // Use this for initialization
    void Start()
    {
        origNext = buttonNext.transform;
        orignPrev = buttonPrev.transform;

       // buttonPrev.transform.position = posTemp.position;

        slides[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (index == slides.Count -1)
        {
            buttonNext.gameObject.SetActive(false);
        }
        else
        {
            buttonNext.gameObject.SetActive(true);
        }

        if (index == 0)
        {
            buttonPrev.gameObject.SetActive(false);
        }
        else
        {
            buttonPrev.gameObject.SetActive(true);
        }
    }


    public void GoNext()
    {
       

        slides[index].SetActive(false);
        index++;
        slides[index].SetActive(true);

      

        shouldCheck = true;
    }

   public void GoPrev()
    {
        

        slides[index].SetActive(false);
        index--;
        slides[index].SetActive(true);

       

        shouldCheck = true;
    }
}
