using System.Xml;

namespace TestTask.Factorys
{
    class ArtistTitleFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new ArtistTitle(allData);
        }
    }
}