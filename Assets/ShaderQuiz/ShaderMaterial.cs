using UnityEngine;

namespace ShaderQuiz
{
    [System.Serializable]
    public class ShaderMaterial
    {
        [SerializeField]
        private Shader _shader;

        [SerializeField]
        private Material _material;

        public Material Material
        {
            get => _material;
        }

        public void Init()
        {
            _material = new Material(_shader);
        }

        public void Dispose()
        {
            Object.Destroy(_material);
        }
    }
}
