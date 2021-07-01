using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishAnimator : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private TextMeshProUGUI dialogtxt;
    private bool isSkipping;
    private readonly string[] dialogs = { "Thank you for freeing me, now I'm free!",
        "Wait, wait, I don't have legs?",
        "I DON'T HAVE LEGS!!!",
        "Ah, wait, that's okay, the modeller just didn't have time to make them as he was working with animals for the first time.",
        "I hope he didn't mess up the face, I wish I had a mirror.",
        "Anyway, thanks for playing"
    };
    [SerializeField] private GameObject DialogData;
    [SerializeField] private GameObject meshok;
    internal async void Animate()
    {
        GetComponent<Animator>().enabled = true;
        await Task.Delay(4000);
        dialog.SetActive(true);
        StartCoroutine(nameof(Dialog));
        DialogData.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isSkipping = true;
    }
    private int i = 0;
    private int dialogI;
    private IEnumerator Dialog()
    {
        meshok.SetActive(false);
        string curDialog = dialogs[0];
        while (true)
        {
            if (i < curDialog.Length)
                dialogtxt.text = $"{dialogtxt.text}{curDialog[i++]}";
            yield return new WaitForSeconds(0.1f);
            if (isSkipping)
            {
                if (dialogtxt.text != curDialog)
                {
                    dialogtxt.text = curDialog;
                    i = curDialog.Length;
                }
                else
                {
                    dialogI++;
                    if (dialogI < dialogs.Length)
                    {
                        curDialog = dialogs[dialogI];
                        i = 0;
                        dialogtxt.text = string.Empty;                        
                    }
                    else
                    {
                        SceneManager.LoadScene("finishScene");
                    }
                }
                isSkipping = false;
            }
        }
    }
}