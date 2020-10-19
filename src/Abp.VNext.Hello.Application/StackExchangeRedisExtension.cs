using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abp.VNext.Hello
{
    public static class StackExchangeRedisExtension
    {
        public static RedisValue ToRedisValue<T>(this T value)
        {
            return value is ValueType || value is string ? value.ToString() : JsonConvert.SerializeObject(value);
        }

        public static RedisValue[] ToRedisValues<T>(this IEnumerable<T> values)
        {
            return values.Select(v => v.ToRedisValue()).ToArray();
        }

        public static T ToObject<T>(this RedisValue value) where T : class
        {
            return typeof(T) == typeof(string)
? value.ToString() as T
: JsonConvert.DeserializeObject<T>(value.ToString());
        }

        public static IEnumerable<T> ToObjects<T>(this IEnumerable<RedisValue> values) where T : class
        {
            return values.Select(v => v.ToObject<T>());
        }

        public static HashEntry[] ToHashEntries(this Dictionary<string, string> entries)
        {
            HashEntry[] es = new HashEntry[entries.Count];
            for (var i = 0; i < entries.Count; i++)
            {
                string name = entries.Keys.ElementAt(i);
                string value = entries[name];
                es[i] = new HashEntry(name, value);
            }

            return es;
        }

        public static Dictionary<string, string> ToDictionary(this IEnumerable<HashEntry> entries)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var entry in entries)
            {
                dict[entry.Name] = entry.Value;
            }

            return dict;
        }

        public static Dictionary<string, string> ToDictionary(this RedisValue[] hashValues, IEnumerable<string> fields)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < fields.Count(); i++)
            {
                dict[fields.ElementAt(i)] = hashValues[i];
            }

            return dict;
        }

        public static Dictionary<string, double> ToDictionary(this IEnumerable<SortedSetEntry> entries)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            foreach (SortedSetEntry entry in entries)
            {
                dict[entry.Element] = entry.Score;
            }

            return dict;
        }


    }

}
