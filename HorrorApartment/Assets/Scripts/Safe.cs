using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Safe : MonoBehaviour {

    public Canvas safeCanvas;
    public GameObject playerObject;

    private int number01 = 0;
    private int number02 = 0;
    private int number03 = 0;
    private int number04 = 0;

    public Text textNumber01;
    public Text textNumber02;
    public Text textNumber03;
    public Text textNumber04;

    public bool opened;
    public float doorOpenAngle = 90f;
    public float smooth = 2f;

    void Start()
    {
        opened = false;
        safeCanvas.enabled = false;        

    }


	public void ShowSafeCanvas () {
        safeCanvas.enabled = true;
        //disable the player controller
        playerObject.GetComponent<FirstPersonController>().enabled = false;
        //unlocks the mouse Coursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerObject.GetComponent<FirstPersonController>().enabled = true;

            safeCanvas.enabled = false;
        }

        //checks that 1112 code was inserted
        if(number01 == 1 && number02 == 1 && number03 == 1 && number04 == 2)
        {            
            opened = true;
        }

        if(opened == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerObject.GetComponent<FirstPersonController>().enabled = true;
            safeCanvas.enabled = false;

            gameObject.layer = 0;
            UnlockSafe();
        }
    }

    void UnlockSafe()
    {
        //Open the safe
        Quaternion targetRotationOpen = Quaternion.Euler(0f, 0f, doorOpenAngle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
    }

    public void IncreaseNumber(int _number)
    {
        if(_number == 1)
        {
            number01++;
            textNumber01.text = number01.ToString();

            if(number01 > 9)
            {
                number01 = 0;
                textNumber01.text = number01.ToString();
            }
        }
        else if (_number == 2)
        {
            number02++;
            textNumber02.text = number02.ToString();

            if (number02 > 9)
            {
                number02 = 0;
                textNumber02.text = number02.ToString();
            }
        }
        else if (_number == 3)
        {
            number03++;
            textNumber03.text = number03.ToString();

            if (number03 > 9)
            {
                number03 = 0;
                textNumber03.text = number03.ToString();
            }
        }
        else if (_number == 4)
        {
            number04++;
            textNumber04.text = number04.ToString();

            if (number04 > 9)
            {
                number04 = 0;
                textNumber04.text = number04.ToString();
            }
        }
    }

    public void DecreaseNumber(int _number)
    {
        if (_number == 1)
        {
            number01--;
            textNumber01.text = number01.ToString();

            if (number01 < 0)
            {
                number01 = 9;
                textNumber01.text = number01.ToString();
            }
        }
        else if (_number == 2)
        {
            number02--;
            textNumber02.text = number02.ToString();

            if (number02 < 0)
            {
                number02 = 9;
                textNumber02.text = number02.ToString();
            }
        }
        else if (_number == 3)
        {
            number03--;
            textNumber03.text = number03.ToString();

            if (number03 < 0)
            {
                number03 = 9;
                textNumber03.text = number03.ToString();
            }
        }
        else if (_number == 4)
        {
            number04--;
            textNumber04.text = number04.ToString();

            if (number04 < 0)
            {
                number04 = 9;
                textNumber04.text = number04.ToString();
            }
        }
    }

}
