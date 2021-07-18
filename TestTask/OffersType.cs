using System;
using System.Xml;


namespace TestTask
{
    /*
     * Класс OfferType описывает стандартные значения, присутствующее в любом типе offera
     * Реализует заполнение стандартных полей
     * Каждый дочерний класс использует методы этого для заполнения стандартных полей и свой метод ParseOther, который заполняет индивидуальные поля этого класса
     * Можно получить любой компонент offer, написав геттер. Написав сеттер можно их изменять, но нужно написать метод изменения AllData
     */
    abstract class OffersType : IOfferInitialize
    {
        protected int Id;
        protected string type;
        protected int bid;
        protected bool available;
        protected string URL;
        protected double price;
        protected string currencyID;
        protected int categoryID;
        protected string picture;
        protected string description;

        public XmlNode AllData { get; }

        public OffersType(XmlNode allData)
        {
            AllData = allData;
        }

        public virtual void Initialize()
        {
            ParseOfferLine();
            ParseBase();
        }

        public int ID()
        {
            return Id;
        }

        protected void ParseOfferLine()
        {
            Id = Int32.Parse(AllData.Attributes.GetNamedItem("id").Value);


            type = AllData.Attributes.GetNamedItem("type").Value;


            bid = Int32.Parse(AllData.Attributes.GetNamedItem("bid").Value);

            available = Boolean.Parse(AllData.Attributes.GetNamedItem("available").Value);
        }

        protected void ParseBase()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "url")
                    URL = childNode.InnerText;

                if (childNode.Name == "price")
                    price = Double.Parse(childNode.InnerText);

                if (childNode.Name == "currencyId")
                    currencyID = childNode.InnerText;

                if (childNode.Name == "categoryId")
                    categoryID = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "picture")
                    picture = childNode.InnerText;

                if (childNode.Name == "description")
                    description = childNode.InnerText; ;
            }
        }
    }

    class VenderModel : OffersType
    {
        private int _cbid;
        private double _localDeliveryCost;
        private string _typePrefix;
        private string _vendor;
        private string _vendorCode;
        private string _model;
        private bool _delivery;
        private bool _manufacturerWarranty;
        private string _countryOfOrigin;

        public VenderModel(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "cbid")
                    _cbid = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "delivery")
                    _delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    _localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "typePrefix")
                    _typePrefix = childNode.InnerText;

                if (childNode.Name == "vendor")
                    _vendor = childNode.InnerText;

                if (childNode.Name == "vendorCode")
                    _vendorCode = childNode.InnerText;

                if (childNode.Name == "model")
                    _model = childNode.InnerText;

                if (childNode.Name == "manufacturer_warranty")
                    _manufacturerWarranty = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "country_of_origin")
                    _countryOfOrigin = childNode.InnerText;
            }
        }
    }

    class Book : OffersType
    {
        private bool _delivery;
        private double _localDeliveryCost;
        private string _author;
        private string _name;
        private string _publisher;
        private string _series;
        private int _year;
        private string _ISBN;
        private int _volume;
        private int _part;
        private string _language;
        private string _binding;
        private int _pageExtene;
        private bool _downloadable;
        public Book(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "delivery")
                    _delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    _localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "author")
                    _author = childNode.InnerText;

                if (childNode.Name == "name")
                    _name = childNode.InnerText;

                if (childNode.Name == " publisher")
                    _publisher = childNode.InnerText;

                if (childNode.Name == "series")
                    _series = childNode.InnerText;

                if (childNode.Name == "year")
                    _year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "ISBN")
                    _ISBN = childNode.InnerText;

                if (childNode.Name == "volume")
                    _volume = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "part")
                    _part = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "language")
                    _language = childNode.InnerText;

                if (childNode.Name == "binding")
                    _binding = childNode.InnerText;

                if (childNode.Name == "page_extent")
                    _pageExtene = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "downloadable")
                    _downloadable = Boolean.Parse(childNode.InnerText);

            }
        }
    }

    class AudioBook : OffersType
    {
        private string _author;
        private string _name;
        private string _publisher;
        private int _year;
        private string _ISBN;
        private string _language;
        private string _performedBy;
        private string _performanceType;
        private string _storage;
        private string _format;
        private string _recordingLength;
        private bool _downloadable;

        public AudioBook(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "author")
                    _author = childNode.InnerText;

                if (childNode.Name == "name")
                    _name = childNode.InnerText;

                if (childNode.Name == " publisher")
                    _publisher = childNode.InnerText;

                if (childNode.Name == "year")
                    _year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "ISBN")
                    _ISBN = childNode.InnerText;

                if (childNode.Name == "language")
                    _language = childNode.InnerText;

                if (childNode.Name == "performed_by")
                    _performedBy = childNode.InnerText;

                if (childNode.Name == "performance_type")
                    _performanceType = childNode.InnerText;

                if (childNode.Name == "storage")
                    _storage = childNode.InnerText;

                if (childNode.Name == "format")
                    _format = childNode.InnerText;

                if (childNode.Name == "recording_length")
                    _recordingLength = childNode.InnerText;

                if (childNode.Name == "downloadable")
                    _downloadable = Boolean.Parse(childNode.InnerText);
            }
        }
    }

    class ArtistTitle : OffersType
    {
        private bool _delivery;
        private string _artist;
        private string _title;
        private int _year;
        private string _media;
        private string _starring;
        private string _director;
        private string _originalName;
        private string _country;

        public ArtistTitle(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "delivery")
                    _delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "artist")
                    _artist = childNode.InnerText;

                if (childNode.Name == "title")
                    _title = childNode.InnerText;

                if (childNode.Name == "year")
                    _year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == " media")
                    _media = childNode.InnerText;

                if (childNode.Name == "starring")
                    _starring = childNode.InnerText;

                if (childNode.Name == "director")
                    _director = childNode.InnerText;

                if (childNode.Name == "originalName")
                    _originalName = childNode.InnerText;

                if (childNode.Name == "country")
                    _country = childNode.InnerText;
            }
        }
    }

    class Tour : OffersType
    {
        private bool _delivery;
        private double _localDeliveryCost;
        private string _worldRegion;
        private string _country;
        private string _region;
        private int _days;
        private string _dateTourStart;
        private string _dateTourEnd;
        private string _nextDateCheck = "9/9/9999";
        private string _hotelStars;
        private string _room;
        private string _meal;
        private string _included;
        private string _transport;



        public Tour(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "delivery")
                    _delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    _localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "worldRegion")
                    _worldRegion = childNode.InnerText;

                if (childNode.Name == " country")
                    _country = childNode.InnerText;

                if (childNode.Name == "region")
                    _region = childNode.InnerText;

                if (childNode.Name == "days")
                    _days = Int32.Parse(childNode.InnerText);

                /*
                 * Для блоков :if (!CheckDate(childNode.InnerText, nextDateCheck)) - if (CheckDate(childNode.InnerText, nextDateCheck))
                 * Находит меньшую из дат и присваевает ее dateTourStart, большую -> dateTourEnd
                 */
                if (childNode.Name == "dataTour")
                    if (!CheckDate(childNode.InnerText, _nextDateCheck))
                    {
                        _dateTourStart = childNode.InnerText;
                        _nextDateCheck = childNode.InnerText;
                    }

                if (childNode.Name == "dataTour")
                    if (CheckDate(childNode.InnerText, _nextDateCheck))
                        _dateTourEnd = childNode.InnerText;

                if (childNode.Name == "hotelStars")
                    _hotelStars = childNode.InnerText;

                if (childNode.Name == "room")
                    _room = childNode.InnerText;

                if (childNode.Name == "meal")
                    _meal = childNode.InnerText;

                if (childNode.Name == "included")
                    _included = childNode.InnerText;

                if (childNode.Name == "transport")
                    _transport = childNode.InnerText;
            }

        }

        /*
         *Возвращает true, если data1 > data2
         *Проверка начинается с года, потом месяц, потом день
         */
        private bool CheckDate(string date1, string date2)
        {

            if (Int32.Parse(date1.Split("/")[2]) > Int32.Parse(date2.Split("/")[2]))
                return true;

            else if (Int32.Parse(date1.Split("/")[1]) > Int32.Parse(date2.Split("/")[1]))
                return true;

            else if (Int32.Parse(date1.Split("/")[0]) > Int32.Parse(date2.Split("/")[0]))
                return true;
            else
                return false;
        }
    }

    class EventTicket : OffersType
    {

        private bool _delivery;
        private double _localDeliveryCost;
        private string _name;
        private string _place;
        private string _hallPlanLink;
        private string _hallPlan;
        private string _hallPart;
        private string _date;
        private int _isPremiere;
        private int _isKids;

        public EventTicket(XmlNode allData) : base(allData) { }

        public override void Initialize()
        {
            ParseOfferLine();
            ParseBase();
            ParseOther();
        }

        private void ParseOther()
        {
            foreach (XmlNode childNode in AllData.ChildNodes)
            {
                if (childNode.Name == "delivery")
                    _delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    _localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "name")
                    _name = childNode.InnerText;

                if (childNode.Name == "place")
                    _place = childNode.InnerText;

                if (childNode.Name == "hall")
                    _hallPlanLink = childNode.Attributes.GetNamedItem("plan").Value;

                if (childNode.Name == "hall")
                    _hallPlan = childNode.InnerText;

                if (childNode.Name == "hall_part")
                    _hallPart = childNode.InnerText;

                if (childNode.Name == "date")
                    _date = childNode.InnerText;

                if (childNode.Name == "is_premiere")
                    _isPremiere = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "isKids")
                    _isKids = Int32.Parse(childNode.InnerText);
            }
        }
    }
}