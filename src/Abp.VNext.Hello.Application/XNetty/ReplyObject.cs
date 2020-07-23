using Newtonsoft.Json;
using System;
using Volo.Abp.EventBus;

namespace Abp.VNext.Hello.XNetty
{


    public abstract class ReplyObject
    {
        public ReplyObject()
        {
        }
        /// <summary>
        /// I表示IOS, A表示Android, PC表示PC, H5表示移动端, D表示桌面程序
        /// </summary>
        public string TargetEndType { get; set; }// "A",
        public string ConnectionId { get; set; }
        /// <summary>
        /// 请求编号（36位的UUID字符串，或其他唯一的字符串）
        /// </summary>
        public string RequestNo { get; set; }//: "202db6c0-1ab1-4e4c-a638-4278bfb1d48b",

        /// <summary>
        /// 模块ID
        /// </summary>
        public string Scope { get; set; } //1,

        /// <summary>
        /// 指令ID
        /// </summary>
        [JsonProperty("cmd")]
        public string Cmd { get; set; }// 1,

        /// <summary>
        /// 状态码，200为正常，其他状态码为业务异常
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }// "200",

        /// <summary>
        /// 消息提示
        /// </summary>
        public string Message { get; set; }// "响应成功"

        public long ServerTime => DateTimeOffset.Now.ToUnixTimeMilliseconds();

        public long ClientTime { get; set; }
        public long GetTimeElapsed() => ClientTime - ServerTime;

        /// <summary>
        /// 耗时
        /// </summary>


        public override string ToString() => JsonConvert.SerializeObject(this);

    }
    /// <summary>
    /// tcp响应统一格式
    /// </summary>
    [EventName("Reply.Handler")]
    [Serializable]
    public class ReplyContent<T> : ReplyObject
    {
        public ReplyContent()
        {
        }
        public static ReplyContent<T> GetReplyContent(string json) => JsonConvert.DeserializeObject<ReplyContent<T>>(json);
        /// <summary>
        /// 响应实体对象
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }

        public static ReplyContent<T> GetModuleInfo(string json)
        {
            //ReplyObject result = JsonConvert.DeserializeObject<ReplyObject>(json);
            // return result;
            return new ReplyContent<T>() { Cmd = "100", RequestNo = "Hello", Message = json };
        }
    }
}
