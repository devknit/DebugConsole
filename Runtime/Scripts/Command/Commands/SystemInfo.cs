
namespace DebugConsole.Command
{
	public sealed class SystemInfo : Base
	{
		public SystemInfo() : base( "システム情報とハードウェア情報を表示します")
		{
		}
		protected override bool OnInvoke( Context context)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( "SystemInfo = {");
			builder.AppendFormat( "  batteryLevel = {0},\n", UnityEngine.SystemInfo.batteryLevel);
			builder.AppendFormat( "  batteryStatus = {0},\n", UnityEngine.SystemInfo.batteryStatus);
			builder.AppendFormat( "  copyTextureSupport = {0},\n", UnityEngine.SystemInfo.copyTextureSupport);
			builder.AppendFormat( "  deviceModel = {0},\n", UnityEngine.SystemInfo.deviceModel);
			builder.AppendFormat( "  deviceName = {0},\n", UnityEngine.SystemInfo.deviceName);
			builder.AppendFormat( "  deviceType = {0},\n", UnityEngine.SystemInfo.deviceType);
			builder.AppendFormat( "  deviceUniqueIdentifier = {0},\n", UnityEngine.SystemInfo.deviceUniqueIdentifier);
			builder.AppendFormat( "  graphicsDeviceID = {0},\n", UnityEngine.SystemInfo.graphicsDeviceID);
			builder.AppendFormat( "  graphicsDeviceName = {0},\n", UnityEngine.SystemInfo.graphicsDeviceName);
			builder.AppendFormat( "  graphicsDeviceType = {0},\n", UnityEngine.SystemInfo.graphicsDeviceType);
			builder.AppendFormat( "  graphicsDeviceVendor = {0},\n", UnityEngine.SystemInfo.graphicsDeviceVendor);
			builder.AppendFormat( "  graphicsDeviceVendorID = {0},\n", UnityEngine.SystemInfo.graphicsDeviceVendorID);
			builder.AppendFormat( "  graphicsDeviceVersion = {0},\n", UnityEngine.SystemInfo.graphicsDeviceVersion);
			builder.AppendFormat( "  graphicsMemorySize = {0},\n", UnityEngine.SystemInfo.graphicsMemorySize);
			builder.AppendFormat( "  graphicsMultiThreaded = {0},\n", UnityEngine.SystemInfo.graphicsMultiThreaded);
			builder.AppendFormat( "  graphicsShaderLevel = {0},\n", UnityEngine.SystemInfo.graphicsShaderLevel);
			builder.AppendFormat( "  graphicsUVStartsAtTop = {0},\n", UnityEngine.SystemInfo.graphicsUVStartsAtTop);
			builder.AppendFormat( "  hasDynamicUniformArrayIndexingInFragmentShaders = {0},\n", UnityEngine.SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders);
			builder.AppendFormat( "  hasHiddenSurfaceRemovalOnGPU = {0},\n", UnityEngine.SystemInfo.hasHiddenSurfaceRemovalOnGPU);
			builder.AppendFormat( "  maxCubemapSize = {0},\n", UnityEngine.SystemInfo.maxCubemapSize);
			builder.AppendFormat( "  maxTextureSize = {0},\n", UnityEngine.SystemInfo.maxTextureSize);
			builder.AppendFormat( "  minConstantBufferOffsetAlignment = {0},\n", UnityEngine.SystemInfo.minConstantBufferOffsetAlignment);
			builder.AppendFormat( "  npotSupport = {0},\n", UnityEngine.SystemInfo.npotSupport);
			builder.AppendFormat( "  operatingSystem = {0},\n", UnityEngine.SystemInfo.operatingSystem);
			builder.AppendFormat( "  operatingSystemFamily = {0},\n", UnityEngine.SystemInfo.operatingSystemFamily);
			builder.AppendFormat( "  processorCount = {0},\n", UnityEngine.SystemInfo.processorCount);
			builder.AppendFormat( "  processorFrequency = {0},\n", UnityEngine.SystemInfo.processorFrequency);
			builder.AppendFormat( "  processorType = {0},\n", UnityEngine.SystemInfo.processorType);
			builder.AppendFormat( "  supportedRenderTargetCount = {0},\n", UnityEngine.SystemInfo.supportedRenderTargetCount);
			builder.AppendFormat( "  supports2DArrayTextures = {0},\n", UnityEngine.SystemInfo.supports2DArrayTextures);
			builder.AppendFormat( "  supports32bitsIndexBuffer = {0},\n", UnityEngine.SystemInfo.supports32bitsIndexBuffer);
			builder.AppendFormat( "  supports3DRenderTextures = {0},\n", UnityEngine.SystemInfo.supports3DRenderTextures);
			builder.AppendFormat( "  supports3DTextures = {0},\n", UnityEngine.SystemInfo.supports3DTextures);
			builder.AppendFormat( "  supportsAccelerometer = {0},\n", UnityEngine.SystemInfo.supportsAccelerometer);
			builder.AppendFormat( "  supportsAsyncCompute = {0},\n", UnityEngine.SystemInfo.supportsAsyncCompute);
			builder.AppendFormat( "  supportsAsyncGPUReadback = {0},\n", UnityEngine.SystemInfo.supportsAsyncGPUReadback);
			builder.AppendFormat( "  supportsAudio = {0},\n", UnityEngine.SystemInfo.supportsAudio);
			builder.AppendFormat( "  supportsComputeShaders = {0},\n", UnityEngine.SystemInfo.supportsComputeShaders);
			builder.AppendFormat( "  supportsCubemapArrayTextures = {0},\n", UnityEngine.SystemInfo.supportsCubemapArrayTextures);
			builder.AppendFormat( "  supportsGraphicsFence = {0},\n", UnityEngine.SystemInfo.supportsGraphicsFence);
			builder.AppendFormat( "  supportsGyroscope = {0},\n", UnityEngine.SystemInfo.supportsGyroscope);
			builder.AppendFormat( "  supportsHardwareQuadTopology = {0},\n", UnityEngine.SystemInfo.supportsHardwareQuadTopology);
			builder.AppendFormat( "  supportsInstancing = {0},\n", UnityEngine.SystemInfo.supportsInstancing);
			builder.AppendFormat( "  supportsLocationService = {0},\n", UnityEngine.SystemInfo.supportsLocationService);
			builder.AppendFormat( "  supportsMipStreaming = {0},\n", UnityEngine.SystemInfo.supportsMipStreaming);
			builder.AppendFormat( "  supportsMotionVectors = {0},\n", UnityEngine.SystemInfo.supportsMotionVectors);
			builder.AppendFormat( "  supportsMultisampleAutoResolve = {0},\n", UnityEngine.SystemInfo.supportsMultisampleAutoResolve);
			builder.AppendFormat( "  supportsMultisampledTextures = {0},\n", UnityEngine.SystemInfo.supportsMultisampledTextures);
			builder.AppendFormat( "  supportsRawShadowDepthSampling = {0},\n", UnityEngine.SystemInfo.supportsRawShadowDepthSampling);
			builder.AppendFormat( "  supportsSeparatedRenderTargetsBlend = {0},\n", UnityEngine.SystemInfo.supportsSeparatedRenderTargetsBlend);
			builder.AppendFormat( "  supportsSetConstantBuffer = {0},\n", UnityEngine.SystemInfo.supportsSetConstantBuffer);
			builder.AppendFormat( "  supportsShadows = {0},\n", UnityEngine.SystemInfo.supportsShadows);
			builder.AppendFormat( "  supportsSparseTextures = {0},\n", UnityEngine.SystemInfo.supportsSparseTextures);
			builder.AppendFormat( "  supportsTextureWrapMirrorOnce = {0},\n", UnityEngine.SystemInfo.supportsTextureWrapMirrorOnce);
			builder.AppendFormat( "  supportsVibration = {0},\n", UnityEngine.SystemInfo.supportsVibration);
			builder.AppendFormat( "  systemMemorySize = {0},\n", UnityEngine.SystemInfo.systemMemorySize);
			builder.AppendFormat( "  unsupportedIdentifier = {0},\n", UnityEngine.SystemInfo.unsupportedIdentifier);
			builder.AppendFormat( "  usesReversedZBuffer = {0},\n", UnityEngine.SystemInfo.usesReversedZBuffer);
			builder.Append( "}");
			context.Output( builder.ToString());
			return true;
		}
	}
}
