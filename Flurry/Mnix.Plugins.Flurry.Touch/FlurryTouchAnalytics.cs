using System;
using Mnix.Utils.Analytics;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Linq;

namespace Mnix.Plugins.Flurry.Touch
{
	internal static class DictionaryExtensions
	{
		internal static NSDictionary ToNSDictionary(this Dictionary<string, string> eventArgs)
		{
			return NSDictionary.FromObjectsAndKeys(
				eventArgs.Values.ToArray(),
				eventArgs.Keys.ToArray()
			);
		}
	}

	public class FlurryTouchAnalytics : IFlurryAnalytics
	{
		#region IFlurryAnalytics implementation

		public void Initialize(string appId, object platformData = null)
		{
			FlurryAnalytics.Flurry.StartSession(appId);
		}

		public void SetAppVersion(string version)
		{
			FlurryAnalytics.Flurry.SetAppVersion(version);
		}

		public string GetFlurryAgentVersion()
		{
			return FlurryAnalytics.Flurry.GetFlurryAgentVersion();
		}

		public void SetSecureTransportEnabled(bool value)
		{
			FlurryAnalytics.Flurry.SetSecureTransportEnabled(value);
		}

		public void SetDebugLog(bool value)
		{
			FlurryAnalytics.Flurry.SetDebugLog(value);
		}

		public void SetShowErrorInLog(bool value)
		{
			FlurryAnalytics.Flurry.SetShowErrorInLog(value);
		}

		public void SetEventLogging(bool value)
		{
			FlurryAnalytics.Flurry.SetEventLogging(value);
		}

		public void SetSessionContinue(int seconds)
		{
			FlurryAnalytics.Flurry.SetSessionContinue(seconds);
		}

		public void SetSessionReportsOnClose(bool sendSessionReportsOnClose)
		{
			FlurryAnalytics.Flurry.SetSessionReportsOnClose(sendSessionReportsOnClose);
		}

		public void SetSessionReportsOnPause(bool setSessionReportsOnPauseEnabled)
		{
			FlurryAnalytics.Flurry.SetSessionReportsOnPause(setSessionReportsOnPauseEnabled);
		}

		public void SetUserID(string userID)
		{
			FlurryAnalytics.Flurry.SetUserID(userID);
		}

		public void SetGender(string gender)
		{
			FlurryAnalytics.Flurry.SetGender(gender);
		}

		public void SetLocation(double latitude, double longitude, float horizontalAccuracy, float verticalAccuracy)
		{
			FlurryAnalytics.Flurry.SetLocation(
				latitude,
				longitude,
				horizontalAccuracy,
				verticalAccuracy
			);
		}

		public void LogEvent(string eventName)
		{
			FlurryAnalytics.Flurry.LogEvent(eventName);
		}

		public void LogEvent(string eventName, Dictionary<string, string> eventArgs)
		{
			FlurryAnalytics.Flurry.LogEvent(
				eventName,
				eventArgs.ToNSDictionary()
			);
		}

		public void LogEvent(string eventName, bool timed)
		{
			FlurryAnalytics.Flurry.LogEvent(eventName, timed);
		}

		public void LogEvent(string eventName, Dictionary<string, string> parameters, bool timed)
		{
			FlurryAnalytics.Flurry.LogEvent(
				eventName,
				parameters.ToNSDictionary(),
				timed
			);
		}

		public void EndTimedEvent(string eventName, Dictionary<string, string> parameters)
		{
			FlurryAnalytics.Flurry.EndTimedEvent(
				eventName,
				parameters.ToNSDictionary()
			);
		}

		public void LogPageView()
		{
			FlurryAnalytics.Flurry.LogPageView();
		}

		//TODO see Mnix.Plugins.Flurry.IFlurryAnalytics::LogAllPageViews
//		public void LogAllPageViews(object target)
//		{
//			FlurryAnalytics.Flurry.LogAllPageViews((NSObject)target)
//		}

		public void LogError(string errorID, string message, Exception error)
		{
			FlurryAnalytics.Flurry.LogError(
				errorID,
				message,
				new NSException(
					error.GetType().FullName,
					error.Message,
					new Dictionary<string, string>{{"stacktrace", error.StackTrace}}.ToNSDictionary()
				)
			);
		}

		#endregion
	}
}

