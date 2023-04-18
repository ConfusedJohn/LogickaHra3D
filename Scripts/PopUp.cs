using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private Canvas tutorial;
    [SerializeField]
    private AudioSource popUpSound;
    [SerializeField]
    private AudioMixerGroup MyMixerGroup;
    private void Start()
    {
        popUpSound.outputAudioMixerGroup = MyMixerGroup;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            popUpSound.Play();
            Time.timeScale = 0;
            Cursor.visible = true;
            tutorial.gameObject.SetActive(true);
        }
    }
}
