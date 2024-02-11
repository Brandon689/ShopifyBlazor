using System.Text.Json.Serialization;

namespace ShopifyPlaywrightSitemapScraping
{
    public class Image
    {
        public int ID { get; set; }

        [JsonPropertyName("id")]
        public long Id2 { get; set; }

        [JsonPropertyName("product_id")]
        public long ProductId2 { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("alt")]
        public string? Alt { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("src")]
        public string? Src { get; set; }

        [JsonPropertyName("variant_ids")]
        public List<long>? VariantIds { get; set; }
    }

    public class Option
    {
        public int ID { get; set; }

        [JsonPropertyName("id")]
        public long Id2 { get; set; }

        [JsonPropertyName("product_id")]
        public long ProductId2 { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("values")]
        public List<string>? Values { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }

        [JsonPropertyName("id")]
        public long? Id2 { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("body_html")]
        public string? BodyHtml { get; set; }

        [JsonPropertyName("vendor")]
        public string? Vendor { get; set; }

        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("handle")]
        public string Handle { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("template_suffix")]
        public string? TemplateSuffix { get; set; }

        [JsonPropertyName("published_scope")]
        public string? PublishedScope { get; set; }

        [JsonPropertyName("tags")]
        public string? Tags { get; set; }

        [JsonPropertyName("variants")]
        public List<Variant>? Variants { get; set; }

        [JsonPropertyName("options")]
        public List<Option>? Options { get; set; }

        [JsonPropertyName("images")]
        public List<Image>? Images { get; set; }

        [JsonPropertyName("image")]
        public Image? Image { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("product")]
        public Product Product { get; set; }
    }

    public class Variant
    {
        public int ID { get; set; }

        [JsonPropertyName("id")]
        public long Id2 { get; set; }

        [JsonPropertyName("product_id")]
        public long ProductId2 { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("compare_at_price")]
        public string? CompareAtPrice { get; set; }

        [JsonPropertyName("fulfillment_service")]
        public string? FulfillmentService { get; set; }

        [JsonPropertyName("inventory_management")]
        public string? InventoryManagement { get; set; }

        [JsonPropertyName("option1")]
        public string? Option1 { get; set; }

        [JsonPropertyName("option2")]
        public string? Option2 { get; set; }

        [JsonPropertyName("option3")]
        public string? Option3 { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("taxable")]
        public bool? Taxable { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("grams")]
        public int? Grams { get; set; }

        [JsonPropertyName("image_id")]
        public long? ImageId { get; set; }

        [JsonPropertyName("weight")]
        public double? Weight { get; set; }

        [JsonPropertyName("weight_unit")]
        public string? WeightUnit { get; set; }

        [JsonPropertyName("requires_shipping")]
        public bool? RequiresShipping { get; set; }
    }
}
