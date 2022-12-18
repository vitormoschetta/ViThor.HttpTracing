using System.Text.Json.Serialization;
using ViThor.HttpTracing.Utils;

namespace ViThor.HttpTracing.Models
{
    public class LoggingModel
    {
        public string CorrelationId { get; set; }

        [JsonIgnore]
        public string Username { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string User => Username ?? "Anonymous";
    }


    public class LoggingRequestModel : LoggingModel
    {
        public string QueryParameters { get; set; }
        public string Arguments { get; set; }


        public override string ToString()
        {
            return $"[{CorrelationId}] Request from {User} to {Controller}/{Action} with query parameters: [{QueryParameters}] and arguments: {Arguments}";
        }

        public string ToJson()
        {
            return JsonManagerSerialize.Serialize(this);
        }
    }


    public class LoggingResponseModel : LoggingModel
    {
        public string Result { get; set; }


        public override string ToString()
        {
            return $"[{CorrelationId}] Response from {User} to {Controller}/{Action} with result: {Result}";
        }

        public string ToJson()
        {
            return JsonManagerSerialize.Serialize(this);
        }
    }
}