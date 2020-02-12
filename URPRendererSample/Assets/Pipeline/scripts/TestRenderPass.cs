﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Pipeline.Test
{
    public class TestRenderPass : ScriptableRenderPass
    {
        private const string Tag = nameof(TestRenderPass);
        private RenderTargetIdentifier _currentTarget;

        public float _ratio;

        public void SetUp(RenderTargetIdentifier target)
        {
            _currentTarget = target;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var commandBuffer = CommandBufferPool.Get(Tag);
            var renderTextureId = Shader.PropertyToID("_SampleURPScriptableRenderer");
            var material = new Material(Shader.Find("pipeline/InvRgb"));
            var cameraData = renderingData.cameraData;
            var w = cameraData.camera.scaledPixelWidth;
            var h = cameraData.camera.scaledPixelHeight;
            
            material.SetFloat("_ratio", _ratio);
            
            commandBuffer.GetTemporaryRT(renderTextureId, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            commandBuffer.Blit(_currentTarget, renderTextureId);
            commandBuffer.Blit(renderTextureId, _currentTarget, material);

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }
    }
}