﻿namespace W49_AssetTracking2
{
    internal class Asset
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int OfficeId { get; set; }

        public Office Office { get; set; }
    }
}
