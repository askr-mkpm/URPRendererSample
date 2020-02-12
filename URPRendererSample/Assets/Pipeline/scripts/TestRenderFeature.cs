using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pipeline.Test
{
    public class TestRenderFeature : ScriptableRendererFeature
    {
        /*[SerializeField, Range(0, 1)] public float ratio = 1f;*/
        [SerializeField] public bool invBool;
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
            /*_scriptablePass._ratio = ratio;*/
            if (invBool == false)
            {
                _scriptablePass._invBool = 0;
            }
            else
            {
                _scriptablePass._invBool = 1;
            }
            renderer.EnqueuePass(_scriptablePass);
        }
    }
}