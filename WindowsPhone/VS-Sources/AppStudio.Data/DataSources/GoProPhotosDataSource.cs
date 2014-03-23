using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GoProPhotosDataSource : IDataSource<FlickrSchema>
    {
        private const string _url = @"http://api.flickr.com/services/feeds/photos_public.gne?tags=gopro";

        private IEnumerable<FlickrSchema> _data = null;

        public GoProPhotosDataSource()
        {
        }

        public async Task<IEnumerable<FlickrSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var rssDataProvider = new RssDataProvider(_url);
                    var syndicationItems = await rssDataProvider.Load();
                    _data = from r in syndicationItems
                            select new FlickrSchema()
                                {
                                    Title = r.Title,
                                    Summary = r.Summary,
                                    ImageUrl = r.ImageUrl,
                                    Published = r.PublishDate
                                };
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("GoProPhotosDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<FlickrSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
