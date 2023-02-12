using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderQuiz
{
    public class Blur : Problem
    {
        [SerializeField]
        private ComputeShader _computeShader;

        private RenderTexture _inputRenderTexture;

        private ComputeBuffer _weightBuffer;

        private readonly float[] _weight = {0.227027f, 0.1945946f, 0.1216216f, 0.054054f, 0.016216f};

        protected override void Awake()
        {
            _outputRenderTexture = new RenderTexture(32, 32, 0);
            _outputRenderTexture.filterMode = FilterMode.Point;
            _outputRenderTexture.enableRandomWrite = true;

            _inputRenderTexture = new RenderTexture(_outputRenderTexture.descriptor);
            _inputRenderTexture.filterMode = FilterMode.Point;
            _inputRenderTexture.enableRandomWrite = true;
            _weightBuffer = new ComputeBuffer(5, 4);
            _weightBuffer.SetData(_weight);
        }

        protected override void OnUpdate()
        {
            Kernel initKernel = new Kernel(_computeShader, "Init");
            _computeShader.SetTexture(initKernel.KernelID, "_Output", _outputRenderTexture);
            _computeShader.Dispatch(initKernel.KernelID, _outputRenderTexture.width / initKernel.ThreadX, _outputRenderTexture.height / initKernel.ThreadY, 1);
            (_inputRenderTexture, _outputRenderTexture) = (_outputRenderTexture, _inputRenderTexture);

            Kernel horizontalKernel = new Kernel(_computeShader, "Horizontal");
            _computeShader.SetTexture(horizontalKernel.KernelID, "_Input", _inputRenderTexture);
            _computeShader.SetTexture(horizontalKernel.KernelID, "_Output", _outputRenderTexture);
            _computeShader.SetBuffer(horizontalKernel.KernelID, "_Weight", _weightBuffer);
            _computeShader.Dispatch(horizontalKernel.KernelID, _outputRenderTexture.width / horizontalKernel.ThreadX, _outputRenderTexture.height / horizontalKernel.ThreadY, 1);
            (_inputRenderTexture, _outputRenderTexture) = (_outputRenderTexture, _inputRenderTexture);

            Kernel verticalKernel = new Kernel(_computeShader, "Vertical");
            _computeShader.SetTexture(verticalKernel.KernelID, "_Input", _inputRenderTexture);
            _computeShader.SetTexture(verticalKernel.KernelID, "_Output", _outputRenderTexture);
            _computeShader.SetBuffer(verticalKernel.KernelID, "_Weight", _weightBuffer);
            _computeShader.Dispatch(verticalKernel.KernelID, _outputRenderTexture.width / verticalKernel.ThreadX, _outputRenderTexture.height / verticalKernel.ThreadY, 1);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Destroy(_inputRenderTexture);
            _weightBuffer?.Release();
        }
    }
}
