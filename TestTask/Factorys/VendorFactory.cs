using System.Xml;

namespace TestTask
{
    class VendorFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new VenderModel(allData);
        }
    }
}