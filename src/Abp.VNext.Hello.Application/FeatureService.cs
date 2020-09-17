using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class FeatureService : ApplicationService
    {
        public FeatureService()
        {

        }

        public IEnumerable<string> GetFeatures()
        {
            var httpContext = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            IFeatureCollection features = httpContext.HttpContext.Features;
            return features.Select(x => x.Key.FullName);
        }


        public object GetFeature(string name)
        {
            IHttpContextAccessor httpContext = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            IFeatureCollection features = httpContext.HttpContext.Features;//Get<IRoutingFeature>();
            KeyValuePair<Type, object> item = features.FirstOrDefault(x => x.Key.FullName == name);
            object feature = features[item.Key];
            return feature;
        }

        public IEnumerable<object> GetProperties()
        {
            IHttpContextAccessor httpContext = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            IFeatureCollection features = httpContext.HttpContext.Features;
            Type t = features.GetType();
            PropertyInfo[] properties = t.GetProperties();

            var groups = from prop in properties
                         group prop by new { prop.DeclaringType.FullName } into g
                         orderby g.Count() descending
                         select new
                         {
                             Name = g.Key,
                             Count = g.Count(),
                             Items = g.ToDictionary(x => x.Name, y => new { Value = y.GetValue(features)?.ToString() })
                         };
            return groups;
        }
    }
}
