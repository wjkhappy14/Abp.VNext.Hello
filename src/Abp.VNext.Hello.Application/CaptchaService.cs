using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;

namespace Abp.VNext.Hello
{
    [RemoteService(false)]
    public class CaptchaService : ApplicationService, ICaptchaService
    {
        protected new ILogger Logger { get; }

        protected IMemoryCache Cache { get; }

        public CaptchaOption Options { get; }

        IDistributedCache<CaptchaItem> _distributedCache;

        protected IOptionsMonitor<CaptchaOption> OptionsMonitor { get; }

        public CaptchaService(IOptionsMonitor<CaptchaOption> options, IMemoryCache cache, IDistributedCache<CaptchaItem> distributedCache)
        {
            OptionsMonitor = options;
            _distributedCache = distributedCache;
            Options = OptionsMonitor.Get("simple");
            Cache = cache;
        }

        /// <summary>
        /// https://sixlabors.com/products/imagesharp-drawing/
        /// </summary>
        /// <param name="captchaItem"></param>
        /// <returns></returns>
        public Task<byte[]> CreateImageAsync(CaptchaItem captchaItem)
        {
            //设置图片大小
            Image image = new Image<Rgba32>(100, 30); ;
            //设置画笔在哪一张图片上画图
            //背景色
            //笔刷
            Pen pen = new Pen(Brushes.Percent10(Color.Red), 2);
            Color[] colors =
                            {
                                Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple,
                                Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Purple
                            };
            var star = new Star(50, 50, 5, 20, 45);
            PointF[] points = new PointF[]
            {
                    new PointF(3, 5),
                    new PointF(3, 5),
                    new PointF(3, 5)
            };

            var brush = new PathGradientBrush(points, colors, Color.White);
            //画一条直线
            //画数字

            _distributedCache.SetAsync($"{captchaItem.SessionId}", captchaItem);
            //内存流
            MemoryStream ms = new MemoryStream();
            //把图片存进内存流
            image.Save("star.png");
            //获取内存流的byte数组
            byte[] buf = ms.GetBuffer();
            return Task.FromResult(buf);
        }

        public Task<CaptchaItem> GetItemAsync(string sessionKey)
        {
            return _distributedCache.GetAsync(sessionKey, false);
        }
        public Task<Guid> Code()
        {
            Guid guid = GuidGenerator.Create();
            Cache.CreateEntry(guid);
            return Task.FromResult(guid);
        }
    }
}
