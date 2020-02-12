using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pipeline.Test
{
    public class TestRenderFeature : ScriptableRendererFeature
    {
        [SerializeField, Range(0, 1)] public float ratio = 1f;
        private TestRenderPass _scriptablePass;

        public override void Create()
        {
            if(_scriptablePass == null)
                _scriptablePass = new TestRenderPass();

            _scriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }
        
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            _scriptablePass.SetUp(renderer.cameraColorTarget);
            _scriptablePass._ratio = ratio;
            renderer.EnqueuePass(_scriptablePass);
        }
    }
}