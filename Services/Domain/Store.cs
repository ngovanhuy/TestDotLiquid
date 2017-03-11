using System;
using System.Collections.Generic;
using Jil;

namespace Services.Domain
{
    public class Store
    {
        [JilDirective(Name = "id")]
        public int Id { get; set; }

        [JilDirective(Name = "alias")]
        public string Alias { get; set; }

        [JilDirective(Name = "name")]
        public string Name { get; set; }

        [JilDirective(Name = "meta_title")]
        public string MetaTitle { get; set; }

        [JilDirective(Name = "meta_description")]
        public string MetaDescription { get; set; }

        [JilDirective(Name = "email")]
        public string Email { get; set; }

        [JilDirective(Name = "customer_email")]
        public string CustomerEmail { get; set; }

        [JilDirective(Name = "store_package_id")]
        public int StorePackageId { get; set; }

        [JilDirective(Name = "start_date")]
        public DateTime? StartDate { get; set; }

        [JilDirective(Name = "end_date")]
        public DateTime? EndDate { get; set; }

        [JilDirective(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JilDirective(Name = "modified_on")]
        public DateTime? ModifiedOn { get; set; }

        [JilDirective(Name = "active_theme_id")]
        public int ActiveThemeId { get; set; }

        [JilDirective(Name = "active_mobile_theme_id")]
        public int? ActiveMobileThemeId { get; set; }

        [JilDirective(Name = "trade_name")]
        public string TradeName { get; set; }

        [JilDirective(Name = "store_owner")]
        public string StoreOwner { get; set; }

        [JilDirective(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [JilDirective(Name = "address")]
        public string Address { get; set; }

        [JilDirective(Name = "province")]
        public string Province { get; set; }

        [JilDirective(Name = "province_id")]
        public int? ProvinceId { get; set; }

        [JilDirective(Name = "province_code")]
        public string ProvinceCode { get; set; }

        [JilDirective(Name = "country")]
        public string Country { get; set; }

        [JilDirective(Name = "country_code")]
        public string CountryCode { get; set; }

        [JilDirective(Name = "currency")]
        public string Currency { get; set; }

        [JilDirective(Name = "money_format")]
        public string MoneyFormat { get; set; }

        [JilDirective(Name = "money_with_currency_format")]
        public string MoneyWithCurrencyFormat { get; set; }

        [JilDirective(Name = "timezone")]
        public string Timezone { get; set; }

        [JilDirective(Name = "status")]
        public int Status { get; set; }

        [JilDirective(Name = "under_construction_mode")]
        public bool UnderConstructionMode { get; set; }

        [JilDirective(Name = "deleted")]
        public bool Deleted { get; set; }

        [JilDirective(Name = "order_code_format")]
        public string OrderCodeFormat { get; set; }

        [JilDirective(Name = "has_storefront")]
        public bool HasStorefront { get; set; }

        [JilDirective(Name = "domain")]
        public string Domain { get; set; }

        [JilDirective(Name = "force_domain")]
        public bool ForceDomain { get; set; }

        //[JilDirective(Name = "list_store_setting")]
        //public List<StoreSetting> ListStoreSetting { get; set; }

        //[JilDirective(Name = "channels")]
        //public List<StoreSaleChannel> Channels { get; set; }

        //[JilDirective(Name = "found_domain")]
        //public DomainEntity FoundDomain { get; set; }

        //public Store()
        //{
        //    ListStoreSetting = new List<StoreSetting>();
        //    Channels = new List<StoreSaleChannel>();
        //}
    }
}
