using System.Xml;

namespace TestTask
{
    interface IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode AllData);
    }
}