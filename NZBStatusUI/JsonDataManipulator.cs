﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Enums;
using JsonDataManipulator.DTOs;
using JsonDataManipulator.Enums;
using JsonDataManipulator.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NZBStatusUI
{
    public class JsonDataManipulator
    {
        private readonly string _url;
        private readonly string _urlHistory;
        private readonly string _baseURL;
        private readonly string _apiKey;
        private JToken _jsonString;
        private JArray _slots;
        private JArray _history;
        private JArray _categories;
        private readonly WebClient _webClient;
        private readonly WebClient _webClientHistory;
        private ConnectionStatus _connectionStatus;
        private ConnectionStatus _connectionCommandStatus;
        private string _result;
        private string _resultHistory;
        private bool _commandResult;
        private readonly List<string> _errorList;
        private string _lastError;

        public string GetLastError()
        {
            if (!string.IsNullOrEmpty(_lastError))
            {
                return _lastError;
            }
            return string.Empty;
        }

        public decimal TotalMB
        {
            get { return GetRootValue<decimal>("mb"); }
        }

        public decimal MBLeft
        {
            get { return GetCurrentSlotValue<decimal>("mbleft"); }
        }

        public decimal TotalMBLeft
        {
            get { return GetRootValue<decimal>("mbleft"); }
        }

        public decimal CurrentMB
        {
            get { return GetCurrentSlotValue<decimal>("mb"); }
        }

        public int TotalPercentage
        {
            get
            {
                var downloaded = (CurrentMB - MBLeft);
                return TotalMB > 0 && CurrentMB > 0 ? Convert.ToInt32(Math.Round(downloaded * 100 / CurrentMB, 0)) : 0;
            }
        }

        public int CurrentPercentage
        {
            get
            {
                var downloaded = (CurrentMB - MBLeft);
                return TotalMB > 0 && CurrentMB > 0 ? Convert.ToInt32(Math.Round(downloaded * 100 / CurrentMB, 0)) : 0;
            }
        }

        public string ETA
        {
            get { return GetRootValue<string>("timeleft") ?? String.Empty; }
        }

        public string Version
        {
            get { return GetRootValue<string>("version") ?? String.Empty; }
        }

        public decimal Speed
        {
            get { return GetRootValue<decimal>("kbpersec"); }
        }

        public decimal AlreadyDownloadedMB
        {
            get { return GetRootValue<decimal>("mb") - GetRootValue<decimal>("mbleft"); }
        }

        public ConnectionStatus ConnectionStatus()
        {
            return _connectionStatus;
        }

        public bool IsDownloading
        {
            get { return Status.Equals("downloading", StringComparison.InvariantCultureIgnoreCase); }
        }

        public bool IsPaused
        {
            get { return GetRootValue<bool>("paused"); }
        }

        public string Status
        {
            get { return GetRootValue<string>("status") ?? String.Empty; }
        }

        public Slot GetCurrentSlot()
        {
            if (_slots.Count > 0)
            {
                return JsonConvert.DeserializeObject<Slot>(_slots[0].ToString());
            }
            return new Slot();
        }

        public HashSet<Slot> GetAllSlots()
        {
            var result = new HashSet<Slot>();
            foreach (JToken slot in _slots)
            {
                result.Add(JsonConvert.DeserializeObject<Slot>(slot.ToString()));
            }
            return result;
        }

        public HashSet<History> GetHistory(int noOfRows)
        {
            var result = new HashSet<History>();
            foreach (JToken slot in _history)
            {
                result.Add(JsonConvert.DeserializeObject<History>(slot.ToString()));
            }
            return result;
        }

        public List<string> Categories
        {
            get
            {
                return _categories.Select(slot => (slot.ToString())).ToList();
            }
        }

        private TClassType GetRootValue<TClassType>(string key)
        {
            JToken token = _jsonString.SelectToken(key);
            if (token != null && new JRaw(token).Value.ToString() != string.Empty)
            {


                var value = token.Value<TClassType>();
                return value;
            }
            return default(TClassType);
        }

        private TClass GetCurrentSlotValue<TClass>(string key)
        {
            if (_slots.Count > 0)
            {
                var token = _slots[0].SelectToken(key);
                var value = token != null ? token.Value<TClass>() : default(TClass);
                return value;
            }
            return default(TClass);
        }

        public JsonDataManipulator(string url, string apiKey)
            : this(url, apiKey, false)
        { }

        public JsonDataManipulator(string url, string apiKey, bool dontParse)
            : this(url, apiKey, dontParse, "queue")
        { }

        public JsonDataManipulator(string url, string apiKey, bool dontParse, string mode)
        {
            _baseURL = url;
            _url = GetBaseURL(url, mode);
            _urlHistory = GetBaseURL(url, "history");
            _apiKey = apiKey;
            _slots = new JArray();
            _history = new JArray();
            _categories = new JArray();
            _jsonString = new JObject();
            _webClient = new WebClient();
            _webClientHistory = new WebClient();
            _errorList = new List<string>();
            _webClient.DownloadStringCompleted += DownloadFinished;
            _webClientHistory.DownloadStringCompleted += DownloadFinishedHistory;
            if (!dontParse)
            {
                InitializeData();
            }
            else
            {
                GetData();
            }
        }

        private string GetBaseURL(string url, string mode, string name = null)
        {
            return GetBaseURL(url, mode, name, null);
        }

        private string GetBaseURL(string url, string mode, string name, string value)
        {
            name = "&name=" + name;
            value = "&value=" + value;

            return string.Format("http://{0}/sabnzbd/api?mode={1}{2}{3}&output=json", url, mode, name, value);
        }

        private bool InitializeData()
        {
            bool returnVal = false;

            GetData();

            if (!string.IsNullOrEmpty(_result) && _connectionStatus == Enums.ConnectionStatus.Ok)
            {
                _jsonString = JObject.Parse(_result).GetValue("queue") ?? new JObject();
                _slots = (JArray)_jsonString["slots"] ?? new JArray();
                _categories = (JArray)_jsonString["categories"] ?? new JArray();
                // this ensures that the true will be returned on next pass only where there is new data downloaded
                _result = string.Empty;
                returnVal = true;
            }

            if (!string.IsNullOrEmpty(_resultHistory) && _connectionStatus == Enums.ConnectionStatus.Ok)
            {
                JToken history = JObject.Parse(_resultHistory).GetValue("history") ?? new JObject();
                _history = (JArray)history["slots"] ?? new JArray();
                // this ensures that the true will be returned on next pass only where there is new data downloaded
                _resultHistory = string.Empty;
                returnVal = true && returnVal;
            }
            return returnVal;
        }

        private void GetData()
        {
            try
            {
                if (!_webClient.IsBusy && !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_baseURL))
                {
                    var url = new Uri(string.Format("{0}&apikey={1}", _url, _apiKey));
                    _webClient.DownloadStringAsync(url);
                }

                if (!_webClientHistory.IsBusy && !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_baseURL))
                {
                    var url = new Uri(string.Format("{0}&apikey={1}", _urlHistory, _apiKey));
                    _webClientHistory.DownloadStringAsync(url);
                }
            }
            catch (WebException e)
            {

                switch (e.Status)
                {
                    case WebExceptionStatus.Timeout:
                        _connectionStatus = Enums.ConnectionStatus.Timeout;
                        break;
                    case WebExceptionStatus.ConnectFailure:
                    case WebExceptionStatus.NameResolutionFailure:
                        _connectionStatus = Enums.ConnectionStatus.CantConnect;
                        break;
                }
            }
        }

        public bool RefreshData()
        {
            return InitializeData();
        }

        private void DownloadFinished(object senderm, DownloadStringCompletedEventArgs eventArgs)
        {
            if (!eventArgs.Cancelled && eventArgs.Error == null)
            {
                // BUG resolve what happens when wrong api is supplied
                _result = eventArgs.Result;
                _connectionStatus = Enums.ConnectionStatus.Ok;
            }
            else
            {
                if (eventArgs.Error != null)
                {
                    LogError(eventArgs.Error, "DownloadFinished");
                }
            }
        }

        private void DownloadFinishedHistory(object senderm, DownloadStringCompletedEventArgs eventArgs)
        {
            if (!eventArgs.Cancelled && eventArgs.Error == null)
            {
                // BUG resolve what happens when wrong api is supplied
                _resultHistory = eventArgs.Result;
                _connectionStatus = Enums.ConnectionStatus.Ok;
            }
            else
            {
                if (eventArgs.Error != null)
                {
                    LogError(eventArgs.Error, "DownloadFinishedHistory");
                }
            }
        }

        private void CommandFinished(object senderm, DownloadStringCompletedEventArgs eventArgs)
        {

            if (!eventArgs.Cancelled && eventArgs.Error == null)
            {
                // BUG resolve what happens when wrong api is supplied
                _commandResult = JObject.Parse(eventArgs.Result).Value<bool>("status");
                _connectionCommandStatus = Enums.ConnectionStatus.Ok;
            }
            else
            {
                if (eventArgs.Error != null)
                {
                    LogError(eventArgs.Error, "CommandFinished");
                }
            }
        }

        public bool ResumeMain()
        {
            return CommandResult(Command.Resume);
        }

        public bool PauseMain()
        {
            return CommandResult(Command.Pause);
        }

        public bool SetSpeedLimit(int speedLimit)
        {
            return CommandResult(Command.Config, Name.SpeedLimit, speedLimit.ToString(CultureInfo.InvariantCulture));
        }

        public int SpeedLimit
        {
            get
            {
                return GetRootValue<int>("speedlimit");
            }
        }


        public bool Pause(string nzo_id)
        {
            return CommandResult(Command.Queue, Name.Pause, nzo_id);
        }

        public bool Resume(string nzo_id)
        {
            return CommandResult(Command.Queue, Name.Resume, nzo_id);
        }

        public bool Delete(string nzo_id)
        {
            return CommandResult(Command.Queue, Name.Delete, nzo_id);
        }

        private bool CommandResult(Command command)
        {
            return CommandResult(command, Name.None, string.Empty);
        }

        private bool CommandResult(Command command, Name name)
        {
            return CommandResult(command, name, string.Empty);
        }

        private bool CommandResult(Command command, Name name, string value)
        {
            try
            {
                var webClient = new WebClient();
                webClient.DownloadStringCompleted += CommandFinished;
                if (!webClient.IsBusy)
                {
                    var url = new Uri(string.Format("{0}&apikey={1}", GetBaseURL(_baseURL, command.Description(), name.Description(), value), _apiKey));
                    webClient.DownloadStringAsync(url);
                }
            }
            catch (WebException e)
            {
                switch (e.Status)
                {
                    case WebExceptionStatus.Timeout:
                        _connectionStatus = Enums.ConnectionStatus.Timeout;
                        break;
                    case WebExceptionStatus.ConnectFailure:
                    case WebExceptionStatus.NameResolutionFailure:
                        _connectionStatus = Enums.ConnectionStatus.CantConnect;
                        break;
                }
            }
            return _commandResult;
        }

        private void LogError(Exception e, string source)
        {
            var error = string.Format("[{0}] - [{1}] - {2}", DateTime.Now, source, e.Message);
            _errorList.Add(error);
            _lastError = error;
        }
    }
}