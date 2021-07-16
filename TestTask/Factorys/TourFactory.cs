using System.Xml;

namespace TestTask.Factorys
{
    class TourFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new Tour(allData);
        }
    }
}