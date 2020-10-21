using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

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
        public Task<byte[]> CreateImageAsync(CaptchaItem captchaItem)
        {
            //设置图片大小
            Image image = new Bitmap(100, 30);
            //设置画笔在哪一张图片上画图
            Graphics graph = Graphics.FromImage(image);
            //背景色
            graph.Clear(Color.White);
            //笔刷
            Pen pen = new Pen(Brushes.Red, 2);
            for (int i = 0; i < 4; i++)
            {
                int[] points = new int[6] { Thread.CurrentThread.ManagedThreadId, 5, 23, 8, 16, Thread.CurrentThread.ManagedThreadId };
                //画一条曲线
                graph.DrawCurve(pen, new Point[] {
                    new Point(points[0], points[1]),
                    new Point(points[2], points[3]),
                    new Point(points[4], points[5])
                });
            }
            //画一条直线
            graph.DrawLines(pen, new Point[] { new Point(10, 11), new Point(90, 40) });
            //画数字

            _distributedCache.SetAsync($"{captchaItem.SessionId}", captchaItem);
            graph.DrawString(captchaItem.Code, new Font(new FontFamily("Microsoft YaHei"), 16, FontStyle.Italic), Brushes.HotPink, new PointF(10, 0));
            //内存流
            MemoryStream ms = new MemoryStream();
            //把图片存进内存流
            image.Save(ms, ImageFormat.Png);
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
