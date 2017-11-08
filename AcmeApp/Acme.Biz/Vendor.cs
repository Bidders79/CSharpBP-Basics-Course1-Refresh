using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {

        public enum IncludeAddress { Yes, No};
        public enum SendCopy { Yes, No };

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
                
        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product"></param>
        /// <param name="qauntity"></param>
        /// <param name="deliverBy"></param>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity,
                                            DateTimeOffset? deliverBy = null,
                                            string instructions = "Standard Delivery")
        {
            if (product == null) throw new ArgumentException(nameof(product));
            if (quantity <= 0) throw new ArgumentException(nameof(quantity));

            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc" + System.Environment.NewLine +
                            "Product: " + product.ProductCode + System.Environment.NewLine
                            + "Quantity: " + quantity);

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                    "Deliver By: " + deliverBy.Value.ToString("d"));
            }
            if (!string.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                    "Instructions: " + instructions);
            }
            var orderText = orderTextBuilder.ToString();
            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult(success, orderText);

            return operationResult;
        }

        /// <summary>
        /// Sends a product order to the vendor
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="includeAddress"></param>
        /// <param name="sendCopy"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity,
                                           IncludeAddress includeAddress, SendCopy sendCopy)
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " with Address";
            if (sendCopy== SendCopy.Yes) orderText += " with Copy";
            var operationResult = new OperationResult(true, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }

        public override string ToString()
        {
            string vendorInfo = "Vendor: " + this.CompanyName;
            string result;
        
            result = vendorInfo?.ToLower();
            result = vendorInfo?.ToUpper();
            result = vendorInfo?.Replace("Vendor", "Supplier");

            var length = vendorInfo?.Length;
            var index = vendorInfo?.IndexOf(":");
            var begins = vendorInfo?.StartsWith("Vendor");

            return vendorInfo;

        }
    }
}
