using UnityEngine;
using UnityEngine.Video;

namespace FlashGlitch {

public sealed class FlashGlitchSetup : MonoBehaviour
{
    [SerializeField] VideoPlayer _source = null;
    [SerializeField] FlashGlitchController _controller;

    void Update()
    {
        if (_source.texture == null) return;
        _controller.Source = _source.texture;
    }
}

} // namespace FlashGlitch
