using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderQuiz
{
    public class VertexAnimTex : Problem
    {
        [SerializeField]
        private Texture2D _vertexAnimTex;

        [SerializeField]
        private float _width = 0.2f;

        [SerializeField]
        private float _height = 0.8f;

        [SerializeField]
        private int _frame = 4000;
        private int _frameCount;

        protected override void Awake()
        {
            base.Awake();

            _vertexAnimTex = new Texture2D(_frame * 4, 1);
            _vertexAnimTex.filterMode = FilterMode.Point;
            _vertexAnimTex.wrapMode = TextureWrapMode.Clamp;
            for (int i = 0; i < _frame; i++)
            {
                float l = (float)i / (float)_frame;
                float b = 0.5f - _height / 2.0f;
                float r = l + _width;
                float u = b + _height;

                Vector4 lb = new Vector4(l, b, 0.0f, 1.0f);
                Vector4 lu = new Vector4(l, u, 0.0f, 1.0f);
                Vector4 ru = new Vector4(r, u, 0.0f, 1.0f);
                Vector4 rb = new Vector4(r, b, 0.0f, 1.0f);
                _vertexAnimTex.SetPixel(i * 4 + 0, 0, lb);
                _vertexAnimTex.SetPixel(i * 4 + 1, 0, lu);
                _vertexAnimTex.SetPixel(i * 4 + 2, 0, ru);
                _vertexAnimTex.SetPixel(i * 4 + 3, 0, rb);
            }
            _vertexAnimTex.Apply();
        }

        protected override void OnUpdate()
        {
            _shaderMaterial.Material.SetInt("_FrameCount", _frameCount);
            _shaderMaterial.Material.SetTexture("_VertexAnimTex", _vertexAnimTex);

            _frameCount++;
            base.OnUpdate();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Destroy(_vertexAnimTex);
        }
    }
}
