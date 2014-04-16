using System;
using System.Collections.Generic;

namespace Mnix.Utils.Analytics
{
    public interface IFlurryAnalytics
    {
		void Initialize(string appId, object platformData = null);
		void SetAppVersion(string version);
		string GetFlurryAgentVersion();
		void SetSecureTransportEnabled(bool value);
		void SetDebugLog(bool value);
		void SetShowErrorInLog(bool value);
		void SetEventLogging(bool value);

		void SetSessionContinue(int seconds);
		void SetSessionReportsOnClose(bool sendSessionReportsOnClose);
		void SetSessionReportsOnPause(bool setSessionReportsOnPauseEnabled);

		void SetUserID(string userID);
		void SetGender(string gender);
		void SetLocation(double latitude, double longitude, float horizontalAccuracy, float verticalAccuracy);

		void LogEvent(string eventName);
		void LogEvent(string eventName, Dictionary<string, string> eventArgs);
		void LogEvent(string eventName, bool timed);
		void LogEvent(string eventName, Dictionary<string, string> parameters, bool timed);
		void EndTimedEvent(string eventName, Dictionary<string, string> parameters);

		void LogPageView();

		//TODO understand how this works and if it's portable
//		void LogAllPageViews(object target);

		void LogError(string errorID, string message, Exception error);
    }
}

