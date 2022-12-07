/*  
 *  --------------------------------------------
 *  ---              Level 3                 ---
 *  --------------------------------------------
*/

namespace W49_AssetTracking2
{
    internal class Office
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string LocalCurrency { get; set; }
        public double CurrencyValue { get; set; }
        public List<Asset> Hardwares { get; set; }
    }
}
