using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderQuiz
{
    public class Problem : MonoBehaviour
    {
        [SerializeField]
        protected ShaderMaterial _shaderMaterial;

        [SerializeField]
        protected QuadRenderer _quadRenderer;

        [SerializeField]
        protected RenderTexture _outputRenderTexture;

        protected virtual void Awake()
        {
            if (_shaderMaterial != null)
                _shaderMaterial.Init();
            _outputRenderTexture = new RenderTexture(1024, 1024, 0);
        }

        private void Update()
        {
            RenderTextureUtility.Clear(_outputRenderTexture, Color.clear);
            OnUpdate();
            _quadRenderer.RenderTexture = _outputRenderTexture;
        }

        protected virtual void OnUpdate()
        {
            Graphics.Blit(null, _outputRenderTexture, _shaderMaterial.Material);
        }

        protected virtual void OnDestroy()
        {
            if (_shaderMaterial != null)
                _shaderMaterial.Dispose();
            _outputRenderTexture.Release();
        }
    }
}
