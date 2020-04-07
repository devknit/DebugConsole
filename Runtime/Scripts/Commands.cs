
using System;
using UnityEngine;

namespace DebugConsole
{
	public sealed partial class GUI : MonoBehaviour
	{
		public bool AddCommand( string name, Func<string, string[], string> func)
		{
			if( string.IsNullOrEmpty( name) == false && func != null)
			{
				if( name[ 0] != '/')
				{
					name = "/" + name;
				}
				if( commands.ContainsKey( name) == false)
				{
					commands.Add( name, func);
					return true;
				}
			}
			return false;
		}
		public bool RemoveCommand( string name)
		{
			if( string.IsNullOrEmpty( name) == false)
			{
				if( name[ 0] != '/')
				{
					name = "/" + name;
				}
				if( commands.ContainsKey( name) == false)
				{
					commands.Remove( name);
					return true;
				}
			}
			return false;
		}
		void OnEntryDefaultCommands()
		{
			AddCommand( "cmd", OnCommandCommandList);
			AddCommand( "clear", OnCommandClearConsole);
			AddCommand( "clipboard", OnCommandClipboard);
			AddCommand( "sys", OnCommandSystemInfo);
			AddCommand( "app", OnCommandApplication);
			AddCommand( "mem", OnCommandMemoryStatus);
			AddCommand( "gc", OnCommandGarbageCollection);
			AddCommand( "ip", OnCommandIP);
			AddCommand( "screen", OnCommandScreen);
			AddCommand( "audio", OnCommandAudio);
		}
		string OnCommandCommandList( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			
			builder.AppendLine( input);
			
			foreach( var command in commands.Keys)
			{
				builder.AppendLine( command);
			}
			return builder.ToString();
		}
		string OnCommandClearConsole( string input, string[] args)
		{
			lock( logs)
			{
				logs.Clear();
				layout.Clear();
			}
			return string.Empty;
		}
		string OnCommandClipboard( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			
			lock( logs)
			{
				foreach( var log in logs)
				{
					builder.AppendLine( log.text);
				}
			}
			GUIUtility.systemCopyBuffer = builder.ToString();
			
			return string.Empty;
		}
		string OnCommandSystemInfo( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendLine( "SystemInfo = {");
			builder.AppendFormat( "  batteryLevel = {0},\n", SystemInfo.batteryLevel);
			builder.AppendFormat( "  batteryStatus = {0},\n", SystemInfo.batteryStatus);
			builder.AppendFormat( "  copyTextureSupport = {0},\n", SystemInfo.copyTextureSupport);
			builder.AppendFormat( "  deviceModel = {0},\n", SystemInfo.deviceModel);
			builder.AppendFormat( "  deviceName = {0},\n", SystemInfo.deviceName);
			builder.AppendFormat( "  deviceType = {0},\n", SystemInfo.deviceType);
			builder.AppendFormat( "  deviceUniqueIdentifier = {0},\n", SystemInfo.deviceUniqueIdentifier);
			builder.AppendFormat( "  graphicsDeviceID = {0},\n", SystemInfo.graphicsDeviceID);
			builder.AppendFormat( "  graphicsDeviceName = {0},\n", SystemInfo.graphicsDeviceName);
			builder.AppendFormat( "  graphicsDeviceType = {0},\n", SystemInfo.graphicsDeviceType);
			builder.AppendFormat( "  graphicsDeviceVendor = {0},\n", SystemInfo.graphicsDeviceVendor);
			builder.AppendFormat( "  graphicsDeviceVendorID = {0},\n", SystemInfo.graphicsDeviceVendorID);
			builder.AppendFormat( "  graphicsDeviceVersion = {0},\n", SystemInfo.graphicsDeviceVersion);
			builder.AppendFormat( "  graphicsMemorySize = {0},\n", SystemInfo.graphicsMemorySize);
			builder.AppendFormat( "  graphicsMultiThreaded = {0},\n", SystemInfo.graphicsMultiThreaded);
			builder.AppendFormat( "  graphicsShaderLevel = {0},\n", SystemInfo.graphicsShaderLevel);
			builder.AppendFormat( "  graphicsUVStartsAtTop = {0},\n", SystemInfo.graphicsUVStartsAtTop);
			builder.AppendFormat( "  hasDynamicUniformArrayIndexingInFragmentShaders = {0},\n", SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders);
			builder.AppendFormat( "  hasHiddenSurfaceRemovalOnGPU = {0},\n", SystemInfo.hasHiddenSurfaceRemovalOnGPU);
			builder.AppendFormat( "  maxCubemapSize = {0},\n", SystemInfo.maxCubemapSize);
			builder.AppendFormat( "  maxTextureSize = {0},\n", SystemInfo.maxTextureSize);
			builder.AppendFormat( "  minConstantBufferOffsetAlignment = {0},\n", SystemInfo.minConstantBufferOffsetAlignment);
			builder.AppendFormat( "  npotSupport = {0},\n", SystemInfo.npotSupport);
			builder.AppendFormat( "  operatingSystem = {0},\n", SystemInfo.operatingSystem);
			builder.AppendFormat( "  operatingSystemFamily = {0},\n", SystemInfo.operatingSystemFamily);
			builder.AppendFormat( "  processorCount = {0},\n", SystemInfo.processorCount);
			builder.AppendFormat( "  processorFrequency = {0},\n", SystemInfo.processorFrequency);
			builder.AppendFormat( "  processorType = {0},\n", SystemInfo.processorType);
			builder.AppendFormat( "  supportedRenderTargetCount = {0},\n", SystemInfo.supportedRenderTargetCount);
			builder.AppendFormat( "  supports2DArrayTextures = {0},\n", SystemInfo.supports2DArrayTextures);
			builder.AppendFormat( "  supports32bitsIndexBuffer = {0},\n", SystemInfo.supports32bitsIndexBuffer);
			builder.AppendFormat( "  supports3DRenderTextures = {0},\n", SystemInfo.supports3DRenderTextures);
			builder.AppendFormat( "  supports3DTextures = {0},\n", SystemInfo.supports3DTextures);
			builder.AppendFormat( "  supportsAccelerometer = {0},\n", SystemInfo.supportsAccelerometer);
			builder.AppendFormat( "  supportsAsyncCompute = {0},\n", SystemInfo.supportsAsyncCompute);
			builder.AppendFormat( "  supportsAsyncGPUReadback = {0},\n", SystemInfo.supportsAsyncGPUReadback);
			builder.AppendFormat( "  supportsAudio = {0},\n", SystemInfo.supportsAudio);
			builder.AppendFormat( "  supportsComputeShaders = {0},\n", SystemInfo.supportsComputeShaders);
			builder.AppendFormat( "  supportsCubemapArrayTextures = {0},\n", SystemInfo.supportsCubemapArrayTextures);
			builder.AppendFormat( "  supportsGraphicsFence = {0},\n", SystemInfo.supportsGraphicsFence);
			builder.AppendFormat( "  supportsGyroscope = {0},\n", SystemInfo.supportsGyroscope);
			builder.AppendFormat( "  supportsHardwareQuadTopology = {0},\n", SystemInfo.supportsHardwareQuadTopology);
			builder.AppendFormat( "  supportsInstancing = {0},\n", SystemInfo.supportsInstancing);
			builder.AppendFormat( "  supportsLocationService = {0},\n", SystemInfo.supportsLocationService);
			builder.AppendFormat( "  supportsMipStreaming = {0},\n", SystemInfo.supportsMipStreaming);
			builder.AppendFormat( "  supportsMotionVectors = {0},\n", SystemInfo.supportsMotionVectors);
			builder.AppendFormat( "  supportsMultisampleAutoResolve = {0},\n", SystemInfo.supportsMultisampleAutoResolve);
			builder.AppendFormat( "  supportsMultisampledTextures = {0},\n", SystemInfo.supportsMultisampledTextures);
			builder.AppendFormat( "  supportsRawShadowDepthSampling = {0},\n", SystemInfo.supportsRawShadowDepthSampling);
			builder.AppendFormat( "  supportsSeparatedRenderTargetsBlend = {0},\n", SystemInfo.supportsSeparatedRenderTargetsBlend);
			builder.AppendFormat( "  supportsSetConstantBuffer = {0},\n", SystemInfo.supportsSetConstantBuffer);
			builder.AppendFormat( "  supportsShadows = {0},\n", SystemInfo.supportsShadows);
			builder.AppendFormat( "  supportsSparseTextures = {0},\n", SystemInfo.supportsSparseTextures);
			builder.AppendFormat( "  supportsTextureWrapMirrorOnce = {0},\n", SystemInfo.supportsTextureWrapMirrorOnce);
			builder.AppendFormat( "  supportsVibration = {0},\n", SystemInfo.supportsVibration);
			builder.AppendFormat( "  systemMemorySize = {0},\n", SystemInfo.systemMemorySize);
			builder.AppendFormat( "  unsupportedIdentifier = {0},\n", SystemInfo.unsupportedIdentifier);
			builder.AppendFormat( "  usesReversedZBuffer = {0},\n", SystemInfo.usesReversedZBuffer);
			builder.Append( "}");
			return builder.ToString();
		}
		string OnCommandApplication( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendLine( "Application = {");
			builder.AppendFormat( "  absoluteURL = {0},\n", Application.absoluteURL);
			builder.AppendFormat( "  backgroundLoadingPriority = {0},\n", Application.backgroundLoadingPriority);
			builder.AppendFormat( "  buildGUID = {0},\n", Application.buildGUID);
			builder.AppendFormat( "  cloudProjectId = {0},\n", Application.cloudProjectId);
			builder.AppendFormat( "  companyName = {0},\n", Application.companyName);
			builder.AppendFormat( "  consoleLogPath = {0},\n", Application.consoleLogPath);
			builder.AppendFormat( "  dataPath = {0},\n", Application.dataPath);
			builder.AppendFormat( "  genuine = {0},\n", Application.genuine);
			builder.AppendFormat( "  genuineCheckAvailable = {0},\n", Application.genuineCheckAvailable);
			builder.AppendFormat( "  identifier = {0},\n", Application.identifier);
			builder.AppendFormat( "  installerName = {0},\n", Application.installerName);
			builder.AppendFormat( "  installMode = {0},\n", Application.installMode);
			builder.AppendFormat( "  internetReachability = {0},\n", Application.internetReachability);
			builder.AppendFormat( "  isBatchMode = {0},\n", Application.isBatchMode);
			builder.AppendFormat( "  isConsolePlatform = {0},\n", Application.isConsolePlatform);
			builder.AppendFormat( "  isEditor = {0},\n", Application.isEditor);
			builder.AppendFormat( "  isFocused = {0},\n", Application.isFocused);
			builder.AppendFormat( "  isMobilePlatform = {0},\n", Application.isMobilePlatform);
			builder.AppendFormat( "  isPlaying = {0},\n", Application.isPlaying);
			builder.AppendFormat( "  persistentDataPath = {0},\n", Application.persistentDataPath);
			builder.AppendFormat( "  platform = {0},\n", Application.platform);
			builder.AppendFormat( "  productName = {0},\n", Application.productName);
			builder.AppendFormat( "  runInBackground = {0},\n", Application.runInBackground);
			builder.AppendFormat( "  sandboxType = {0},\n", Application.sandboxType);
			builder.AppendFormat( "  streamingAssetsPath = {0},\n", Application.streamingAssetsPath);
			builder.AppendFormat( "  systemLanguage = {0},\n", Application.systemLanguage);
			builder.AppendFormat( "  targetFrameRate = {0},\n", Application.targetFrameRate);
			builder.AppendFormat( "  temporaryCachePath = {0},\n", Application.temporaryCachePath);
			builder.AppendFormat( "  unityVersion = {0},\n", Application.unityVersion);
			builder.AppendFormat( "  version = {0},\n", Application.version);
			builder.Append( "}");
			return builder.ToString();
		}
		string OnCommandMemoryStatus( string input, string[] args)
		{
			long monoUsed = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
			long monoSize = UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
			long totalUsed = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();
			long totalSize = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendFormat( "[Mono] {0:#,0} / {1:#,0} KiB ({2:f1}%)\n", 
				monoUsed / 1024, monoSize / 1024, 100.0 * monoUsed / monoSize);
			builder.AppendFormat( "[Total] {0:#,0}/{1:#,0} KiB ({2:f1}%)\n",
				totalUsed / 1024, totalSize / 1024, 100.0 * totalUsed / totalSize);
			return builder.ToString();
		}
		string OnCommandGarbageCollection( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendFormat( "{0:#,0} KiB\n", GC.GetTotalMemory( true) / 1024);
			return builder.ToString();
		}
		string OnCommandIP( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			
			string hostname = System.Net.Dns.GetHostName();
			var addresses = System.Net.Dns.GetHostAddresses( hostname);
			
			foreach( var address in addresses)
			{
				switch( address.AddressFamily)
				{
					case System.Net.Sockets.AddressFamily.InterNetwork:
					{
						builder.Append( "[IPv4]");
						break;
					}
					case System.Net.Sockets.AddressFamily.InterNetworkV6:
					{
						if( address.IsIPv6LinkLocal == false)
						{
							builder.Append( "[IPv6] ");
						}
						else
						{
							builder.Append( "[link-local IPv6] ");
						}
						break;
					}
					default:
					{
						builder.Append( string.Format( "[{0}] ", address.AddressFamily));
						break;
					}
				}
				builder.AppendLine( address.ToString());
			}
			return builder.ToString();
		}
		string OnCommandScreen( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendLine( "Screen = {");
			builder.AppendFormat( "  autorotateToLandscapeLeft = {0},\n", Screen.autorotateToLandscapeLeft);
			builder.AppendFormat( "  autorotateToLandscapeRight = {0},\n", Screen.autorotateToLandscapeRight);
			builder.AppendFormat( "  autorotateToPortrait = {0},\n", Screen.autorotateToPortrait);
			builder.AppendFormat( "  autorotateToPortraitUpsideDown = {0},\n", Screen.autorotateToPortraitUpsideDown);
			builder.AppendFormat( "  currentResolution = {0},\n", Screen.currentResolution);
			builder.AppendFormat( "  dpi = {0},\n", Screen.dpi);
			builder.AppendFormat( "  fullScreen = {0},\n", Screen.fullScreen);
			builder.AppendFormat( "  fullScreenMode = {0},\n", Screen.fullScreenMode);
			builder.AppendFormat( "  height = {0},\n", Screen.height);
			builder.AppendFormat( "  orientation = {0},\n", Screen.orientation);
			builder.AppendFormat( "  resolutions = {0},\n", Screen.resolutions);
			builder.AppendFormat( "  safeArea = {0},\n", Screen.safeArea);
			builder.AppendFormat( "  sleepTimeout = {0},\n", Screen.sleepTimeout);
			builder.AppendFormat( "  width = {0},\n", Screen.width);
			builder.Append( "}");
			return builder.ToString();
		}
		string OnCommandAudio( string input, string[] args)
		{
			var builder = new System.Text.StringBuilder();
			builder.AppendLine( input);
			builder.AppendLine( "AudioSettings = {");
			builder.AppendFormat( "  driverCapabilities = {0},\n", AudioSettings.driverCapabilities);
			builder.AppendFormat( "  dspTime = {0},\n", AudioSettings.dspTime);
			builder.AppendFormat( "  outputSampleRate = {0},\n", AudioSettings.outputSampleRate);
			builder.AppendFormat( "  speakerMode = {0},\n", AudioSettings.speakerMode);
			builder.AppendLine( "}");
			builder.AppendLine( "AudioListener = {");
			builder.AppendFormat( "  pause = {0},\n", AudioListener.pause);
			builder.AppendFormat( "  volume = {0},\n", AudioListener.volume);
			builder.Append( "}");
			return builder.ToString();
		}
	}
}
