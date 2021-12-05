using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioSlider : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public static bool isDragging;
    public Slider audioSlider;
    public AudioSource audio;


    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioChange();
        isDragging = false;
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audioSlider.onValueChanged.AddListener(delegate { AudioChange(); });
    }

    private void Update()
    {
        ShowTime();
    }


    void AudioChange()
    {
        if(audioSlider.value != 1)
        {
            audio.time = audioSlider.value * audio.clip.length;
        }
        
    }


    void ShowTime()
    {
        audioSlider.value = audio.time / audio.clip.length;
    }
}
