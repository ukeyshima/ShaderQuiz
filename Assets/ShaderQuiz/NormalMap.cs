using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderQuiz
{
    public class NormalMap : Problem
    {
        [SerializeField]
        private ShaderMaterial _hightMap;

        [SerializeField]
        private RenderTexture _hightMapTex;

        protected override void Awake()
        {
            base.Awake();

            _hightMap.Init();
            _hightMapTex = new RenderTexture(_outputRenderTexture.descriptor);
        }

        protected override void OnUpdate()
        {
            Graphics.Blit(null, _hightMapTex, _hightMap.Material);
            Graphics.Blit(_hightMapTex, _outputRenderTexture, _shaderMaterial.Material);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hightMap.Dispose();
            _hightMapTex.Release();
        }
    }
}
