using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    const int resetTime = 60;
    int current;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayButtonFlash();

        SceneChange();
    }

    void PlayButtonFlash()
    {
        if (current <= 20)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (current <= resetTime)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        if (current <= 0)
            current = resetTime;

        current--;
    }

    void SceneChange()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (Input.anyKeyDown)
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene(1); break;
                case 1:
                    SceneManager.LoadScene(0); break;
            }

        }
    }
}
