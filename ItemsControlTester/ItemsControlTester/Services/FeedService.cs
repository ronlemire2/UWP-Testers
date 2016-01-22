using ItemsControlTester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsControlTester.Services {
    public class FeedService {
        private List<Feed> GetFeeds() {
            List<Feed> feeds = new List<Feed>() {
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "CFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "DFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "EFeed3", Link = "AFeed3Link", LastUpdated = "AFeed3LastUpdated", Category = "Comics" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "GFeed1", Link = "BFeed1Link", LastUpdated = "BFeed1LastUpdated", Category = "Politics" },
                new Feed {FeedName = "HFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "JFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "KFeed2", Link = "CFeed2Link", LastUpdated = "CFeed2LastUpdated", Category = "Tech" },
                new Feed {FeedName = "LFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "DFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "EFeed3", Link = "AFeed3Link", LastUpdated = "AFeed3LastUpdated", Category = "Comics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "JFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "FFeed1", Link = "CFeed1Link", LastUpdated = "CFeed1LastUpdated", Category = "Tech" },
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "CFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "DFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "CFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "IFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "CFeed3", Link = "CFeed3Link", LastUpdated = "CFeed3LastUpdated", Category = "Tech" },
                new Feed {FeedName = "EFeed3", Link = "AFeed3Link", LastUpdated = "AFeed3LastUpdated", Category = "Comics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "BFeed2", Link = "BFeed2Link", LastUpdated = "BFeed2LastUpdated", Category = "Politics" },
                new Feed {FeedName = "AFeed1", Link = "AFeed1Link", LastUpdated = "AFeed1LastUpdated", Category = "Comics" },
                new Feed {FeedName = "AFeed2", Link = "AFeed2Link", LastUpdated = "AFeed2LastUpdated", Category = "Comics" }
            };

            return feeds;
        }


        public IEnumerable<object> GetFeedGroupsByLetter() {
            IEnumerable<object> feedsByLetter;
            List<Feed> feeds = GetFeeds();

            feedsByLetter = feeds
                .OrderBy(f => f.FeedName)
                .GroupBy(f => f.FeedName[0])
                .OrderBy(g => g.Key)
                .Select(g => g);

            return feedsByLetter;
        }

        public IEnumerable<object> GetFeedGroupsByCategory() {
            IEnumerable<object> feedsByCategory;
            List<Feed> feeds = GetFeeds();

            feedsByCategory = feeds
                .OrderBy(f => f.FeedName)
                .GroupBy(f => f.Category)
                .OrderBy(g => g.Key)
                .Select(g => g);

            return feedsByCategory;
        }
    }
}
