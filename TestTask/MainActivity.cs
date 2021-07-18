using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using TestTask.Factorys;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ArrayAdapter<string> _adapter;

        /*
         * Словарь фабрик, для пораждения экземпляров классов
         */
        private readonly Dictionary<string, IOffersFactory> _offerToFactory = new Dictionary<string, IOffersFactory>()
        {
            { "vendor.model", new VendorFactory() },
            { "book", new BookFactory() },
            { "audiobook", new AudioBookFactory() },
            { "artist.title", new ArtistTitleFactory() },
            { "tour", new TourFactory() },
            { "event-ticket", new EventTicketFactory() } 
        };

        /*
         * Ассоциативный массив Id -> offer.
         * По Id можно найти любой оффер и вытащить необходимые данные(при декларировании и реализации ссответствующих методов)
         */
        private Dictionary<int, IOfferInitialize> _IdstoOfferClasses = new Dictionary<int, IOfferInitialize>();

        /*
         * Список ID'шников
         */
        private List<string> _Ids = new List<string>();
        private ListView _list;

        [Obsolete]
        public override void OnBackPressed()
        {
            base.SetContentView(Resource.Layout.activity_main);
            _list = FindViewById<ListView>(Resource.Id.listView1);
            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _Ids);
            _list.SetAdapter(_adapter);
            _list.ItemClick += List_ItemClick;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var realizeDataTask = RealizeDataAsync();
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource           
            SetContentView(Resource.Layout.activity_main);
            _list = FindViewById<ListView>(Resource.Id.listView1);
            Task.WhenAll(realizeDataTask);
            _list.SetAdapter(_adapter);
            _list.ItemClick += List_ItemClick;
        }

        [Obsolete]
        private async Task RealizeDataAsync()
        {
            var parseTask = Task.Factory.StartNew(() => Parse(GetData()));
            Task.WaitAll(parseTask);
            var setDataTask = Task.Factory.StartNew(SetData);
            await Task.WhenAll(setDataTask);
        }

        private XmlDocument GetData()
        {
            string sURL;
            sURL = "http://partner.market.yandex.ru/pages/help/YML.xml";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            XmlDocument doc = new XmlDocument();
            doc.Load(objStream);
             
            return doc;
        }

        [System.Obsolete]
        private void Parse(XmlDocument doc)
        {
            XmlNode offers = doc.GetElementsByTagName("offers")[0];
            XmlNode offer;

            foreach (XmlNode xNode in offers)
            {
                if (xNode.Name == "offer")
                {
                    offer = xNode;
                    IOffersFactory offersFactory = null;

                    /*
                     * Добовляем offer в нужный класс
                     * Так же создаем новую пару ID - offer
                     */
                    if (_offerToFactory.ContainsKey(offer.Attributes.GetNamedItem("type").Value))
                    {
                        offersFactory = _offerToFactory[offer.Attributes.GetNamedItem("type").Value];
                    }

                    if (offersFactory != null)
                    {
                        var offerInitialize = offersFactory.CreateOffer(offer);
                        offerInitialize.Initialize();
                        _IdstoOfferClasses.Add(offerInitialize.ID(), offerInitialize);
                    }
                }

            }
        }

        /*
         * Запоняем список Id'шников и устонавливаем настройки адаптера
         */
        [System.Obsolete]
        private void SetData()
        {
            foreach (var ID in _IdstoOfferClasses.Keys)
            {
                _Ids.Add(ID.ToString());
            }

            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _Ids);
        }


        /*
         * Обработчик нажатия на строку ListView
         * Переключается на новое activity с TextView, куда и выводит объект offer
         * Реализуем Ассоциативный массив
         */
        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = _adapter.GetItem(e.Position);
            SetContentView(Resource.Layout.activity_message);
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            IOfferInitialize offer;

            if (_IdstoOfferClasses.TryGetValue(Int32.Parse(item), out offer))
            {
                textView.Text = JsonConvert.SerializeObject(offer);
            }
        }
    }
}