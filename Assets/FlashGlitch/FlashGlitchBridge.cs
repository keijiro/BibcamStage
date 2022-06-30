using UnityEngine;
using UnityEngine.Video;
using FlashGlitch;

namespace BibcamStage {

public sealed class FlashGlitchBridge : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float _randomize = 0.2f;

    public void OnNoteChannel0(int note, float velocity)
      => OnTrigger(0, velocity);

    public void OnNoteChannel1(int note, float velocity)
      => OnTrigger(1, velocity);

    void OnTrigger(int index, float value)
    {
        if (value == 0 || _controller == null) return;
        value *= Random.Range(1 - _randomize, 1.0f);
        _controller.TriggerEffect(index, value);
        _controller.RandomizeSeed();
        if (Random.value < 0.01f) _controller.RandomizeHue();
    }

    FlashGlitchController _controller;
    VideoPlayer _source;

    void Start()
    {
        _controller = GetComponent<FlashGlitchController>();
        _source = FindObjectOfType<VideoPlayer>();
        transform.parent = GameObject.FindWithTag("MainCamera").transform;
        transform.localPosition = Vector3.forward;
    }

    void Update()
    {
        if (_source.texture == null) return;
        _controller.Source = _source.texture;
    }
}

} // namespace BibcamStage
