using UnityEngine;

namespace ShaderQuiz
{
    public class Kernel
    {
        private int _kernelID;
        private uint _threadX, _threadY, _threadZ;

        public int KernelID { get => _kernelID; }
        public int ThreadX { get => (int)_threadX; }
        public int ThreadY { get => (int)_threadY; }
        public int ThreadZ { get => (int)_threadZ; }

        public Kernel(ComputeShader computeShader, string kernel)
        {
            _kernelID = computeShader.FindKernel(kernel);
            computeShader.GetKernelThreadGroupSizes(_kernelID, out _threadX, out _threadY, out _threadZ);
        }
    }
}
