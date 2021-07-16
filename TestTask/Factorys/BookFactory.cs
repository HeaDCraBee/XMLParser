using System.Xml;

namespace TestTask.Factorys
{
    class BookFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new Book(allData);
        }
    }
}