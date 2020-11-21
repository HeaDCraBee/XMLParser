﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using TestTask.Factorys;



namespace TestTask
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ArrayAdapter<string> adapter;
        /*
         * Ассоциативный массив Id -> offer.
         * По Id можно найти любой оффер и вытащить необходимые данные(реализовав предваритьно геттер)
         */
        Dictionary<int, OffersType> IDstoOfferClasses = new Dictionary<int, OffersType>();
        /*
         * Список ID'шников
         */
        private List<string> IDs = new List<string>();
        private ListView list;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            GetData();
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            list = FindViewById<ListView>(Resource.Id.listView1);
            list.SetAdapter(adapter);
            list.ItemClick += List_ItemClick;
        }

        /*
         * Получение данных
         */
        [System.Obsolete]
        private async void GetData()
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
            await Task.Run(() => Parse(doc));

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
                    /*
                     * Добовляем offer в нужный класс
                     * Так же создаем новую пару ID - offer
                     */

                    switch (offer.Attributes.GetNamedItem("type").Value)
                    {
                        case "vendor.model":
                            {
                                IOffersFactory offersFactory = new VendorFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }
                        case "book":
                            {
                                IOffersFactory offersFactory = new BookFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }
                        case "audiobook":
                            {
                                IOffersFactory offersFactory = new AudioBookFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }
                        case "artist.title":
                            {
                                IOffersFactory offersFactory = new ArtistTitleFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }
                        case "tour":
                            {
                                IOffersFactory offersFactory = new TourFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }

                        case "event-ticket":
                            {
                                IOffersFactory offersFactory = new EventTicketFactory();
                                IOfferInitialize offerInitialize = offersFactory.CreateOffer(offer);
                                offerInitialize.Initialize();
                                IDstoOfferClasses.Add(offerInitialize.ID(), (OffersType)offersFactory.CreateOffer(offer));
                                break;
                            }
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
                                foreach (var ID in IDstoOfferClasses.Keys)
                                {
                                    IDs.Add(ID.ToString());
                                }
                                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, IDs);
                            }


                            /*
                             * Обработчик нажатия на строку ListView
                             * Переключается на новое activity с TextView, куда и выводит объект offer
                             * Реализуем Ассоциативный массив
                             */
                            private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
                            {
                                var item = adapter.GetItem(e.Position);
                                SetContentView(Resource.Layout.activity_message);
                                TextView textView = FindViewById<TextView>(Resource.Id.textView1);
                                OffersType offer;
                                if (IDstoOfferClasses.TryGetValue(Int32.Parse(item), out offer))
                                {
                                    textView.Text = JsonConvert.SerializeObject(offer);

                                }

                            }

                            [Obsolete]
                            public override void OnBackPressed()
        {
            base.SetContentView(Resource.Layout.activity_main);
            list = FindViewById<ListView>(Resource.Id.listView1);
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, IDs);
            list.SetAdapter(adapter);
            list.ItemClick += List_ItemClick;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}