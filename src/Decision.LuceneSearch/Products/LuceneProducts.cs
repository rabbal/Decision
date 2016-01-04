//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Web;
//using GoldenCitShop.Searching;
//using Lucene.Net.Analysis.Standard;
//using Lucene.Net.Documents;
//using Lucene.Net.Index;
//using Lucene.Net.QueryParsers;
//using Lucene.Net.Search;
//using Lucene.Net.Search.Similar;
//using Lucene.Net.Store;
//using ViewModel.Admin.Product;
//using Version = Lucene.Net.Util.Version;

//namespace Web.Searching
//{
//    public static class LuceneProducts
//    {
//        #region Fields
//        private static string _path = HttpRuntime.AppDomainAppPath + @"App_Data\Lucene_Index";
//        private static IndexSearcher _searcher;
//        private static readonly Version _version = Version.LUCENE_30;
//        #endregion

//        #region Properties

//        public static IndexSearcher Searcher
//        {
//            get
//            {
//                if (_searcher == null)
//                {
//                    _searcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(_path)), true);
//                }
//                return _searcher;
//            }
//        }
//        #endregion

//        #region Maping
//        static Document MapProductToDocument(ProductLuceneViewModel viewModel)
//        {
//            var productDocument = new Document();
//            productDocument.Add(new Field("Id", viewModel.Id.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.NOT_ANALYZED));
//            productDocument.Add(new Field("PrincipleImagePath", viewModel.ImagePath, Field.Store.YES, Field.Index.NOT_ANALYZED));
//            var nameField = new Field("Name", viewModel.Name, Field.Store.YES, Field.Index.ANALYZED,
//                Field.TermVector.WITH_POSITIONS_OFFSETS) {Boost = 3};
//            productDocument.Add(nameField);
//            productDocument.Add(new Field("Description", viewModel.Description, Field.Store.NO, Field.Index.ANALYZED,
//                Field.TermVector.WITH_POSITIONS_OFFSETS));
//            return productDocument;
//        }


//        #endregion

//        #region SearchResult

//        public static IList<ProductSearchResultViewModel> GetTermsScored(string inputText, int maxItems = 10)
//        {
//            var resultsList = new List<ProductSearchResultViewModel>();
//            inputText = inputText.ApplyCorrectYeKe();
//            if (string.IsNullOrEmpty(inputText.Replace("*", "").Replace("?", "")))
//                return resultsList;

//            inputText = inputText.ApplyCorrectYeKe();
//            var analyzer = new StandardAnalyzer(_version, GetStopWords());

//            var results = Searcher.Search(new PrefixQuery(new Term("Name", inputText)), null, maxItems);
//            if (results.TotalHits == 0)
//            {
//                results = Searcher.Search(new PrefixQuery(new Term("Description", inputText)), null, maxItems);
//            }
//            if (results.TotalHits == 0)
//            {
//                var parser = new MultiFieldQueryParser(_version, new[] { "Name", "Description" }, analyzer);
//                var searchQuery = ParseQuery(inputText, parser);
//                results = Searcher.Search(searchQuery, maxItems);
//                if (results.TotalHits == 0)
//                {
//                    inputText = SearchByPartialWords(inputText);
//                    searchQuery = ParseQuery(inputText, parser);
//                    results = Searcher.Search(searchQuery, maxItems);
//                }
//            }

//            resultsList.AddRange(results.ScoreDocs.Select(doc => new ProductSearchResultViewModel
//            {
//                Id = Guid.Parse(Searcher.Doc(doc.Doc).Get("Id")), Name = Searcher.Doc(doc.Doc).Get("Name"), ImagePath = Searcher.Doc(doc.Doc).Get("PrincipleImagePath")
//            }));
//            return resultsList;
//        }

//        #endregion

//        #region CreateIndexes
//        public static void CreateIndexes(IEnumerable<ProductLuceneViewModel> viewModel)
//        {
//            var directory = FSDirectory.Open(new DirectoryInfo(_path));
//            var analyzer = new StandardAnalyzer(_version, GetStopWords());
//            using (var writer = new IndexWriter(directory, analyzer, create: true, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
//            {
//                foreach (var product in viewModel)
//                {
//                    writer.AddDocument(MapProductToDocument(product));
//                }

//                writer.Optimize();
//                writer.Commit();
//                writer.Dispose();
//                directory.Dispose();
//            }
//        }

//        #endregion

//        #region add-delete-update

//        public static bool ClearLuceneIndex()
//        {
//            try
//            {
//                var directory = FSDirectory.Open(new DirectoryInfo(_path));
//                var analyzer = new StandardAnalyzer(_version);
//                using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
//                {
//                    writer.DeleteAll();
//                    writer.Commit();
//                    analyzer.Dispose();
//                    writer.Dispose();
//                    directory.Dispose();
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//            return true;
//        }

//        public static void UpdateIndex(ProductLuceneViewModel viewModel)
//        {
//            var directory = FSDirectory.Open(new DirectoryInfo(_path));
//            var analyzer = new StandardAnalyzer(_version, GetStopWords());
//            using (var indexWriter = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
//            {
//                var newDoc = MapProductToDocument(viewModel);

//                indexWriter.UpdateDocument(new Term("Id", viewModel.Id.ToString(CultureInfo.InvariantCulture)), newDoc);
//                indexWriter.Commit();
//                indexWriter.Dispose();
//                directory.Dispose();
//            }
//        }
//        public static void DeleteIndex(Guid id)
//        {
//            var directory = FSDirectory.Open(new DirectoryInfo(_path));
//            var analyzer = new StandardAnalyzer(_version);
//            using (var indexWriter = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
//            {
//                indexWriter.DeleteDocuments(new Term("Id", id.ToString(CultureInfo.InvariantCulture)));
//                indexWriter.Commit();
//                indexWriter.Dispose();
//                indexWriter.Optimize();
//                directory.Dispose();
//            }
//        }

//        public static void AddIndex(ProductLuceneViewModel viewModel)
//        {
//            var directory = FSDirectory.Open(new DirectoryInfo(_path));
//            var analyzer = new StandardAnalyzer(_version, GetStopWords());
//            using (var writer = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
//            {
//                writer.AddDocument(MapProductToDocument(viewModel));
//                writer.Optimize();
//                writer.Commit();
//                writer.Dispose();
//                directory.Dispose();
//            }
//        }

//        private static HashSet<string> GetStopWords()
//        {
//            var result = new HashSet<string>();
//            var stopWords = new[]
//            {
//                "به",
//                "با",
//                "از",
//                "تا",
//                "و",
//                "است",
//                "هست",
//                "هستم",
//                "هستیم",
//                "هستید",
//                "هستند",
//                "نیست",
//                "نیستم",
//                "نیستیم",
//                "نیستند",
//                "اما",
//                "یا",
//                "این",
//                "آن",
//                "اینجا",
//                "آنجا",
//                "بود",
//                "باد",
//                "برای",
//                "که",
//                "دارم",
//                "داری",
//                "دارد",
//                "داریم",
//                "دارید",
//                "دارند",
//                "چند",
//                "را",
//                "ها",
//                "های",
//                "می",
//                "هم",
//                "در",
//                "باشم",
//                "باشی",
//                "باشد",
//                "باشیم",
//                "باشید",
//                "باشند",
//                "اگر",
//                "مگر",
//                "بجز",
//                "جز",
//                "الا",
//                "اینکه",
//                "چرا",
//                "کی",
//                "چه",
//                "چطور",
//                "چی",
//                "چیست",
//                "آیا",
//                "چنین",
//                "اینچنین",
//                "نخست",
//                "اول",
//                "آخر",
//                "انتها",
//                "صد",
//                "هزار",
//                "میلیون",
//                "ملیون",
//                "میلیارد",
//                "ملیارد",
//                "یکهزار",
//                "تریلیون",
//                "تریلیارد",
//                "میان",
//                "بین",
//                "زیر",
//                "بیش",
//                "روی",
//                "ضمن",
//                "همانا",
//                "ای",
//                "بعد",
//                "پس",
//                "قبل",
//                "پیش",
//                "هیچ",
//                "همه",
//                "واما",
//                "شد",
//                "شده",
//                "شدم",
//                "شدی",
//                "شدیم",
//                "شدند",
//                "یک",
//                "یکی",
//                "نبود",
//                "میکند",
//                "میکنم",                
//                "میکنیم",
//                "میکنید",
//                "میکنند",
//                "میکنی",
//                "طور",
//                "اینطور",
//                "آنطور",
//                "هر",
//                "حال",
//                "مثل",
//                "خواهم",
//                "خواهی",
//                "خواهد",
//                "خواهیم",
//                "خواهید",
//                "خواهند",
//                "داشته",
//                "داشت",
//                "داشتی",
//                "داشتم",
//                "داشتیم",
//                "داشتید",
//                "داشتند",
//                "آنکه",
//                "مورد",
//                "کنید",
//                "کنم",
//                "کنی",
//                "کنند",
//                "کنیم",
//                "نکنم",
//                "نکنی",
//                "نکند",
//                "نکنیم",
//                "نکنید",
//                "نکنند",
//                "نکن",
//                "بگو",
//                "نگو",
//                "مگو",
//                "بنابراین",
//                "بدین",
//                "من",
//                "تو",
//                "او",
//                "ما",
//                "شما",
//                "ایشان",
//                "ی",
//                "ـ",
//                "هایی",
//                "خیلی",
//                "بسیار",
//                "1",
//                "بر",
//                "l",
//                "شود",
//                "کرد",
//                "کرده",
//                "نیز",
//                "خود",
//                "شوند",
//                "اند",
//                "داد",
//                "دهد",
//                "گشت",
//                "ز",
//                "گفت",
//                "آمد",
//                "اندر",
//                "چون",
//                "بد",
//                "چو",
//                "همی",
//                "پر",
//                "سوی",
//                "دو",
//                "گر",
//                "بی",
//                "گرد",
//                "زین",
//                "کس",
//                "زان",
//                "جای",
//                "آید"
//            };
//            foreach (var item in stopWords)
//                result.Add(item);
//            return result;
//        }


//        #endregion

//        #region Optimization
//        public static void Optimize()
//        {
//            var directory = FSDirectory.Open(new DirectoryInfo(_path));
//            var analyzer = new StandardAnalyzer(_version);
//            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
//            {
//                analyzer.Close();
//                writer.Optimize();
//                writer.Dispose();
//                directory.Dispose();
//            }
//        }


//        #endregion

//        #region QueryParser
//        private static Query ParseQuery(string searchQuery, QueryParser parser)
//        {
//            Query query;
//            try
//            {
//                query = parser.Parse(searchQuery.Trim());
//            }
//            catch (ParseException)
//            {
//                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
//            }
//            return query;
//        }
//        private static string SearchByPartialWords(string bodyTerm)
//        {
//            bodyTerm = bodyTerm.Replace("*", "").Replace("?", "");
//            var terms = bodyTerm.Trim().Replace("-", " ").Split(' ')
//                                     .Where(x => !string.IsNullOrEmpty(x))
//                                     .Select(x => x.Trim() + "*");
//            bodyTerm = string.Join(" ", terms);
//            return bodyTerm;
//        }

//        #endregion

//        #region MoreLike
//        private static int GetLuceneDocumentNumber(Guid productId)
//        {
//            var analyzer = new StandardAnalyzer(_version);
//            var parser = new QueryParser(_version, "Id", analyzer);
//            var query = ParseQuery(productId.ToString(CultureInfo.InvariantCulture), parser);
//            var doc = Searcher.Search(query, 1);
//            return doc.TotalHits == 0 ? 0 : doc.ScoreDocs[0].Doc;
//        }

//        private static Query CreateMoreLikeThisQuery(Guid prodcutId)
//        {
//            var docNum = GetLuceneDocumentNumber(prodcutId);
//            if (docNum == 0)
//                return null;

//            var analyzer = new StandardAnalyzer(_version);
//            var reader = Searcher.IndexReader;

//            var moreLikeThis = new MoreLikeThis(reader) {Analyzer = analyzer};
//            moreLikeThis.SetFieldNames(new[] { "Name", "Description" });
//            moreLikeThis.MinDocFreq = 1;
//            moreLikeThis.MinTermFreq = 1;
//            moreLikeThis.Boost = true;

//            return moreLikeThis.Like(docNum);
//        }

//        public static IList<ProductSearchResultViewModel> ShowMoreLikeThisPostItems(Guid productId)
//        {
//            var query = CreateMoreLikeThisQuery(productId);
//            if (query == null)
//                return null;
//            var hitResults = Searcher.Search(query, n: 7);
//            return hitResults.ScoreDocs.Select(item => Searcher.Doc(item.Doc)).Select(doc => new ProductSearchResultViewModel
//            {
//                Id = Guid.Parse(doc.Get("Id")), Name = doc.Get("Name"), ImagePath = doc.Get("PrincipleImagePath")
//            }).ToList();
//        }
//        #endregion

//    }
//}