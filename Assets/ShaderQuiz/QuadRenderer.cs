using UnityEngine;

namespace ShaderQuiz
{
    public class QuadRenderer : MonoBehaviour
    {
        [SerializeField] private Mesh _quad;

        [SerializeField] private Camera _camera;

        [SerializeField] private ShaderMaterial _shaderMaterial;

        private RenderTexture _renderTexture;
        private Matrix4x4 _quadMatrix = Matrix4x4.identity;
        private Vector2Int _screenResolution = new Vector2Int(0, 0);

        public RenderTexture RenderTexture { set => _renderTexture = value; }

        private void Awake()
        {
            _shaderMaterial.Init();
            _camera.orthographic = true;
            _camera.transform.position = new Vector3(0, 0, -1);
        }

        private void Update()
        {
            if (_renderTexture != null)
                _shaderMaterial.Material.mainTexture = _renderTexture;

            Graphics.DrawMesh(_quad, _quadMatrix, _shaderMaterial.Material, 0);

            if (_screenResolution.x == Screen.width && _screenResolution.y == Screen.height)
                return;
            OnChangedResolution();
        }

        private void OnChangedResolution()
        {
            _screenResolution = new Vector2Int(Screen.width, Screen.height);
            float aspectRatio = (float)_screenResolution.x / (float)_screenResolution.y;
            float height = _camera.orthographicSize * 2;
            float width = height * aspectRatio;
            _quadMatrix.SetTRS(Vector3.zero, Quaternion.identity, new Vector3(width, _camera.orthographicSize * 2, 0));
        }

        private void OnDestroy()
        {
            _shaderMaterial.Dispose();
            _renderTexture?.Release();
        }
    }
}
