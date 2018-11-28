using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace STH.BiometricIdentityService.Infrastructure.Utilities.ApiConfiguration.MediaFormatters
{

    public class ResponseMediaFormatter : MediaTypeFormatter
    {

        private readonly string json = "application/json";
        private readonly string textxml = "text/xml";
        private readonly string applicationxml = "application/xml";

        public ResponseMediaFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(json));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(textxml));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(applicationxml));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof (Uri);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof (Uri);
        }

        public override Task WriteToStreamAsync(Type type, object value,
            Stream writeStream, System.Net.Http.HttpContent content,
            System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                if (type == typeof (Uri))
                    BuildSyndicationFeed(value, writeStream, content.Headers.ContentType.MediaType);
            });
        }

        private void BuildSyndicationFeed(object uri, Stream stream, string contenttype)
        {
            var url = (uri as Uri).OriginalString;

            SyndicationFeedFormatter formatter;
            XmlTextReader reader = new XmlTextReader(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            if (contenttype == "application/atom+xml")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }

            using (var writer = XmlWriter.Create(stream))
            {
                formatter.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }

        }
    }

}