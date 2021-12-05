using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioSlider : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public static bool isDragging;
    public Slider audioSlider;
    public AudioSource audio;

    // use pointerDown and Up here to drag audio

    public void OnPointerDown(PointerEventData eventData)
    {
        // while dragging
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // mouse button up then update audio time to new value, not like before keep update it get only one value when mouse up
        AudioChange();
        isDragging = false;
    }

    private void Start()
    {
        // get audio object when start application
        audio = GetComponent<AudioSource>();
        audioSlider.onValueChanged.AddListener(delegate { AudioChange(); });
    }

    private void Update()
    {
        // update slider value, for further update leave it in showtime()
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
