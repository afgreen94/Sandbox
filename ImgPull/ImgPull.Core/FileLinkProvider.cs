//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ImgPull.Core
//{
//    public abstract class HtmlFileLinkProviderBase : IFileLinkProvider
//    {
//        public async IAsyncEnumerable<FileLinkResult> GetLinksAsync(FileLinkProviderArgs optionsOVerride = null)
//        {
//            throw new NotImplementedException();

//            var targetCollection = await this.RetrieveTargetCollection(optionsOVerride.Source);

//            foreach (var fileLink in targetCollection.FileLinks)
//                yield return new FileLinkResult();

//            //page links 
//        }

//        protected async Task<TargetCollection> RetrieveTargetCollection(string htmlSource)
//        {
//            var response = await HttpHandler.SendAsync(new HttpRequestMessage()
//            {
//                RequestUri = new Uri(htmlSource),
//                Method = HttpMethod.Get
//            }).ConfigureAwait(false);

//            var httpResponseValidationResult = await HttpResultValidator.ValidateHttpResponseMessageAsync(response).ConfigureAwait(false);

//            if (!httpResponseValidationResult.Success)
//                throw new Exception("-AG");

//            var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

//            return HtmlParser.Parse(html);
//        }
//    }

//    public class HtmlFileLinkProvider : HtmlFileLinkProviderBase, IFileLinkProvider
//    {
//    }
//}
