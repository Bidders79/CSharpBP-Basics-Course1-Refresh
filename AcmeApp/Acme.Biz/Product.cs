using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;
using static Acme.Common.LoggingService;

namespace Acme.Biz
{
    /// <summary>
    /// Manages Products carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructor
        public Product()
        {
            Console.WriteLine("Product instance created");
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }
        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }
            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        #endregion

        #region properties

        private string productName;
        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters long";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name must not be more than 20 characters long";
                }
                else
                {
                    productName = value;
                }
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        internal string Category { get; set; }

        public int SequenceNumber { get; set; } = 1;

        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";

        private DateTime? availabiltyDate;

        public DateTime? AvailabiltyDate
        {
            get { return availabiltyDate; }
            set { availabiltyDate = value; }
        }

        public decimal Cost { get; set; }

        public string ValidationMessage { get; private set; }


        #endregion

        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent"></param>
        /// <returns></returns>
        public decimal CalculateSuggestedPrice(decimal markupPercent) =>
            this.Cost + (this.Cost * markupPercent / 100);

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message: Vendor Email Send");

            var emailService = new EmailService();
            emailService.SendMessage("Mesage", "Message: Email Service send", "test@mail.com");

            var result = LogAction("Hello logging");

            return "Hello " +
                ProductName + "(" + ProductId + "): " +
                Description + " Available on: " +
                AvailabiltyDate?.ToShortDateString();
        }

        public override string ToString() => 
            this.ProductName + " (" + this.ProductId + ")" ;
                
    }
}
