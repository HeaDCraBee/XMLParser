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

namespace TestTask
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ArrayAdapter<string> _adapter;
        /*
         * Ассоциативный массив Id -> offer.
         * По Id можно найти любой оффер и вытащить необходимые данные(реализовав предваритьно геттер)
         */
        private Dictionary<int, OffersType> _IdstoOfferClasses = new Dictionary<int, OffersType>();
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
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            GetData();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);          
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _list = FindViewById<ListView>(Resource.Id.listView1);
            _list.SetAdapter(_adapter);
            //_list.ItemClick += List_ItemClick;
        }

        /*
         * Получение данных
         */
        [System.Obsolete]
        private void GetData()
        {
            string sURL;
            sURL = "http://partner.market.yandex.ru/pages/help/YML.xml";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            XmlDocument doc = new XmlDocument();
            doc.Load(objStream);

            /*
             * Парсим файл
             */
            Parse(doc);
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
                    switch (offer.Attributes.GetNamedItem("type").Value)
                    {
                        case "vendor.model":
                            {
                                offersFactory = new VendorFactory();
                                break;
                            }
                        case "book":
                            {
                                offersFactory = new BookFactory();                             
                                break;
                            }
                        case "audiobook":
                            {
                                offersFactory = new AudioBookFactory();
                                break;
                            }
                        case "artist.title":
                            {
                                offersFactory = new ArtistTitleFactory();
                                break;
                            }
                        case "tour":
                            {
                                offersFactory = new TourFactory();
                                break;
                            }

                        case "event-ticket":
                            {
                                offersFactory = new EventTicketFactory();
                                break;
                            }
                    }

                    if (offersFactory != null)
                    {
                        IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                        offerInitialize.Initialize();
                        _IdstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                    }
                }

            }

            /*
            * Переходим в сетДата, в котором получаем список и устонавливаем значения адаптера
            * Переходим из этого метода, т.к он выполняется асинхронно, а если выполнить SetData в GetData есть шанс не заполнить массив IDtoOffer
            */
            SetData();
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
            OffersType offer;

            if (_IdstoOfferClasses.TryGetValue(Int32.Parse(item), out offer))
            {
                textView.Text = JsonConvert.SerializeObject(offer);
            }
        }
    }
}