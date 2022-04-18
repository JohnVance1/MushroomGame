using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypewriterEffect : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private float typingspeed = 50f;


    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        

        float t = 0;
            int charIndex = 0;
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typingspeed; //increment over time
            charIndex = Mathf.FloorToInt(t); //store floored value of the timer

            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring( 0, charIndex);

            yield return null;

        }

        textLabel.text = textToType;
    }



}
