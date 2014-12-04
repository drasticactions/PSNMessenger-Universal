﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlayStationApp.Core.Tools
{
    public static class UriExtensions
    {
        private static readonly Regex _regex = new Regex(@"[?|&](\w+)=([^?|^&]+)");

        public static IReadOnlyDictionary<string, string> ParseQueryString(string uri)
        {
            Match match = _regex.Match(uri);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }
}
