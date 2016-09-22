﻿using System;
using System.IO;
using System.Security.Cryptography;
using BencodeNET.Objects;

namespace BencodeNET.Torrents
{
    public static class TorrentUtil
    {
        public static string CalculateInfoHash(Torrent torrent)
        {
            var info = torrent.ToBDictionary().Get<BDictionary>("info");
            return CalculateInfoHash(info);
        }

        public static byte[] CalculateInfoHashBytes(Torrent torrent)
        {
            var info = torrent.ToBDictionary().Get<BDictionary>("info");
            return CalculateInfoHashBytes(info);
        }

        public static string CalculateInfoHash(BDictionary info)
        {
            var hashBytes = CalculateInfoHashBytes(info);

            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public static byte[] CalculateInfoHashBytes(BDictionary info)
        {
            using (var sha1 = SHA1.Create())
            using (var stream = new MemoryStream())
            {
                info.EncodeTo(stream);
                stream.Position = 0;

                return sha1.ComputeHash(stream);
            }
        }
    }
}
