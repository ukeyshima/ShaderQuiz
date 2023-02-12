using UnityEngine;

namespace ShaderQuiz
{
    [System.Serializable]
    public static class RenderTextureUtility
    {
        public static void Clear(RenderTexture tex, Color color)
        {
            RenderTexture rt = RenderTexture.active;
            RenderTexture.active = tex;
            GL.Clear(true, true, color);
            RenderTexture.active = rt;
        }
    }
}
