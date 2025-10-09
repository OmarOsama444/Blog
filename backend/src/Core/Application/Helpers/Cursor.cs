using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Application.Helpers
{
    public sealed record Cursor<T>(DateTime CreatedAt, T LastId)
    {
        public static string Encode(Cursor<T> cursor)
        {
            string json = JsonSerializer.Serialize(cursor);
            return Base64UrlTextEncoder.Encode(Encoding.UTF8.GetBytes(json));
        }
        public static Cursor<T>? Decode(string? cursor)
        {
            if (string.IsNullOrWhiteSpace(cursor))
            {
                return null;
            }

            try
            {
                string json = Encoding.UTF8.GetString(Base64UrlTextEncoder.Decode(cursor));
                return JsonSerializer.Deserialize<Cursor<T>>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}