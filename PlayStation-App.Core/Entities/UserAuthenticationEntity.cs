﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SQLite.Net.Attributes;

namespace PlayStationApp.Core.Entities
{
    public class UserAuthenticationEntity
    {
        public string AccessToken { get; private set; }

        public string RefreshToken { get; private set; }

        public long ExpiresIn { get; private set; }

        public string TokenType { get; private set; }

        public string Scope { get; private set; }

        public void Parse(string json)
        {
            var o = JObject.Parse(json);
            AccessToken = (String)o["access_token"];
            RefreshToken = (String)o["refresh_token"];
            ExpiresIn = (long)o["expires_in"];
            TokenType = (String)o["token_type"];
            Scope = (String)o["scope"];
        }
    }
}
