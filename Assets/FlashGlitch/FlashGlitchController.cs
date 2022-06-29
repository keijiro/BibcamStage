using UnityEngine;
using UnityEngine.Video;

namespace BibcamStage {

public sealed class FlashGlitchController : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] VideoPlayer _source = null;

    #endregion

    #region Public method

    public void TriggerEffect(int index)
    {
        _params[index] = (1, Mathf.Pow(Random.Range(2.0f, 5.0f), 2));
        _seed = Random.Range(0, 100000);
    }

    #endregion

    #region Private members

    MaterialPropertyBlock _block;
    (float value, float speed) [] _params = new (float, float) [2];
    float _seed;

    #endregion

    #region MonoBehaviour implementation

    void Start()
      => _block = new MaterialPropertyBlock();

    void Update()
    {
        for (var i = 0; i < _params.Length; i++)
            _params[i].value -= _params[i].speed * Time.deltaTime;
    }

    void LateUpdate()
    {
        if (_source.texture == null) return;

        var renderer = GetComponent<Renderer>();
        renderer.GetPropertyBlock(_block);

        _block.SetTexture("_MainTex", _source.texture);
        _block.SetFloat("_Param1", Mathf.Max(0, _params[0].value));
        _block.SetFloat("_Param2", Mathf.Max(0, _params[1].value));
        _block.SetFloat("_Seed", _seed);
        _block.SetFloat("_HueShift", (Time.time * 0.1f) % 1);

        renderer.SetPropertyBlock(_block);
    }

    #endregion
}

} // namespace BibcamStage
