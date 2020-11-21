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
        private int cbid;
        private double localDeliveryCost;
        private string typePrefix;
        private string vendor;
        private string vendorCode;
        private string model;
        private bool delivery;
        private bool manufacturerWarranty;
        private string countryOfOrigin;

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
                    cbid = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "delivery")
                    delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "typePrefix")
                    typePrefix = childNode.InnerText;

                if (childNode.Name == "vendor")
                    vendor = childNode.InnerText;

                if (childNode.Name == "vendorCode")
                    vendorCode = childNode.InnerText;

                if (childNode.Name == "model")
                    model = childNode.InnerText;

                if (childNode.Name == "manufacturer_warranty")
                    manufacturerWarranty = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "country_of_origin")
                    countryOfOrigin = childNode.InnerText;
            }
        }
    }

    class Book : OffersType
    {
        private bool delivery;
        private double localDeliveryCost;
        private string author;
        private string name;
        private string publisher;
        private string series;
        private int year;
        private string ISBN;
        private int volume;
        private int part;
        private string language;
        private string binding;
        private int pageExtene;
        private bool downloadable;
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
                    delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "author")
                    author = childNode.InnerText;

                if (childNode.Name == "name")
                    name = childNode.InnerText;

                if (childNode.Name == " publisher")
                    publisher = childNode.InnerText;

                if (childNode.Name == "series")
                    series = childNode.InnerText;

                if (childNode.Name == "year")
                    year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "ISBN")
                    ISBN = childNode.InnerText;

                if (childNode.Name == "volume")
                    volume = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "part")
                    part = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "language")
                    language = childNode.InnerText;

                if (childNode.Name == "binding")
                    binding = childNode.InnerText;

                if (childNode.Name == "page_extent")
                    pageExtene = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "downloadable")
                    downloadable = Boolean.Parse(childNode.InnerText);

            }
        }
    }

    class AudioBook : OffersType
    {
        private string author;
        private string name;
        private string publisher;
        private int year;
        private string ISBN;
        private string language;
        private string performedBy;
        private string performanceType;
        private string storage;
        private string format;
        private string recordingLength;
        private bool downloadable;

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
                    author = childNode.InnerText;

                if (childNode.Name == "name")
                    name = childNode.InnerText;

                if (childNode.Name == " publisher")
                    publisher = childNode.InnerText;

                if (childNode.Name == "year")
                    year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "ISBN")
                    ISBN = childNode.InnerText;

                if (childNode.Name == "language")
                    language = childNode.InnerText;

                if (childNode.Name == "performed_by")
                    performedBy = childNode.InnerText;

                if (childNode.Name == "performance_type")
                    performanceType = childNode.InnerText;

                if (childNode.Name == "storage")
                    storage = childNode.InnerText;

                if (childNode.Name == "format")
                    format = childNode.InnerText;

                if (childNode.Name == "recording_length")
                    recordingLength = childNode.InnerText;

                if (childNode.Name == "downloadable")
                    downloadable = Boolean.Parse(childNode.InnerText);

            }
        }

    }

    class ArtistTitle : OffersType
    {
        private bool delivery;
        private string artist;
        private string title;
        private int year;
        private string media;
        private string starring;
        private string director;
        private string originalName;
        private string country;

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
                    delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "artist")
                    artist = childNode.InnerText;

                if (childNode.Name == "title")
                    title = childNode.InnerText;

                if (childNode.Name == "year")
                    year = Int32.Parse(childNode.InnerText);

                if (childNode.Name == " media")
                    media = childNode.InnerText;

                if (childNode.Name == "starring")
                    starring = childNode.InnerText;

                if (childNode.Name == "director")
                    director = childNode.InnerText;

                if (childNode.Name == "originalName")
                    originalName = childNode.InnerText;

                if (childNode.Name == "country")
                    country = childNode.InnerText;
            }
        }
    }

    class Tour : OffersType
    {
        private bool delivery;
        private double localDeliveryCost;
        private string worldRegion;
        private string country;
        private string region;
        private int days;
        private string dateTourStart;
        private string dateTourEnd;
        private string nextDateCheck = "9/9/9999";
        private string hotelStars;
        private string room;
        private string meal;
        private string included;
        private string transport;



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
                    delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "worldRegion")
                    worldRegion = childNode.InnerText;

                if (childNode.Name == " country")
                    country = childNode.InnerText;

                if (childNode.Name == "region")
                    region = childNode.InnerText;

                if (childNode.Name == "days")
                    days = Int32.Parse(childNode.InnerText);

                /*
                 * Для блоков :if (!CheckDate(childNode.InnerText, nextDateCheck)) - if (CheckDate(childNode.InnerText, nextDateCheck))
                 * Находит меньшую из дат и присваевает ее dateTourStart, большую -> dateTourEnd
                 */
                if (childNode.Name == "dataTour")
                    if (!CheckDate(childNode.InnerText, nextDateCheck)) {
                        dateTourStart = childNode.InnerText;
                        nextDateCheck = childNode.InnerText;
                    }

                if (childNode.Name == "dataTour")
                    if (CheckDate(childNode.InnerText, nextDateCheck))
                        dateTourEnd = childNode.InnerText;

                if (childNode.Name == "hotelStars")
                    hotelStars = childNode.InnerText;

                if (childNode.Name == "room")
                    room = childNode.InnerText;

                if (childNode.Name == "meal")
                    meal = childNode.InnerText;

                if (childNode.Name == "included")
                    included = childNode.InnerText;

                if (childNode.Name == "transport")
                    transport = childNode.InnerText;
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

        private bool delivery;
        private double localDeliveryCost;
        private string name;
        private string place;
        private string hallPlanLink;
        private string hallPlan;
        private string hallPart;
        private string date;
        private int isPremiere;
        private int isKids;

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
                    delivery = Boolean.Parse(childNode.InnerText);

                if (childNode.Name == "local_delivery_cost")
                    localDeliveryCost = Double.Parse(childNode.InnerText);

                if (childNode.Name == "name")
                    name = childNode.InnerText;

                if (childNode.Name == "place")
                    place = childNode.InnerText;

                if (childNode.Name == "hall")
                    hallPlanLink = childNode.Attributes.GetNamedItem("plan").Value;

                if (childNode.Name == "hall")
                    hallPlan = childNode.InnerText;

                if (childNode.Name == "hall_part")
                    hallPart = childNode.InnerText;

                if (childNode.Name == "date")
                    date = childNode.InnerText;

                if (childNode.Name == "is_premiere")
                    isPremiere = Int32.Parse(childNode.InnerText);

                if (childNode.Name == "isKids")
                    isKids = Int32.Parse(childNode.InnerText);
            }
        }
    }

}