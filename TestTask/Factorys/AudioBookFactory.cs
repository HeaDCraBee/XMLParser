using System.Xml;

namespace TestTask.Factorys
{
    class AudioBookFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new AudioBook(allData);
        }
    }
}