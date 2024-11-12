using FINAL_INTERN.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FINAL_INTERN.Business.VNPayService
{
    public class VNPayService
    {
        private readonly IConfiguration _config;

        private readonly finalInternDbContext _finalInternDbContext;

        public VNPayService(IConfiguration config,finalInternDbContext finalInternDbContext)
        {
            _config = config;
            _finalInternDbContext = finalInternDbContext;
        }

        public string CreatePaymentUrl(decimal amount, string orderInfo, string returnUrl, string transactionId)
        {
            var vnPayUrl = _config["VNPay:PaymentUrl"];
            var merchantId = _config["VNPay:MerchantId"];
            var secretKey = _config["VNPay:SecretKey"];

            var vnp_Params = new Dictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", merchantId },
            { "vnp_Amount", (amount * 100).ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", transactionId },
            { "vnp_OrderInfo", orderInfo },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", returnUrl },
            { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
        };

            // Tạo chữ ký (hash) cho dữ liệu
            var signData = string.Join("&", vnp_Params.OrderBy(k => k.Key).Select(kv => kv.Key + "=" + kv.Value));
            var hash = HmacSHA512(secretKey, signData);
            vnp_Params["vnp_SecureHash"] = hash;

            // Tạo URL với các tham số
            var paymentUrl = vnPayUrl + "?" + string.Join("&", vnp_Params.Select(kv => kv.Key + "=" + HttpUtility.UrlEncode(kv.Value)));

            return paymentUrl;
        }

        public bool ValidateSignature(Dictionary<string, string> responseParams)
        {
            var secretKey = _config["VNPay:SecretKey"];

            var receivedHash = responseParams["vnp_SecureHash"];
            responseParams.Remove("vnp_SecureHash");

            var rawData = string.Join("&", responseParams.OrderBy(k => k.Key).Select(kv => kv.Key + "=" + kv.Value));
            var expectedHash = HmacSHA512(secretKey, rawData);

            return receivedHash == expectedHash;
        }

        private string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

        public void SavePayment(Dictionary<string, string> responseParams)
        {
            var payment = new Payment
            {
                CarId = int.Parse(responseParams["CarId"]),
                AccountId = int.Parse(responseParams["AccountId"]),
                Amount = int.Parse(responseParams["vnp_Amount"]) / 100,
                OrderId = int.Parse(responseParams["orderId"]),
                VnpBankTranNo = responseParams["vnp_BankTranNo"],
                VnpCartType = responseParams["vnp_CardType"],
                VnpBankCode = responseParams["vnp_BankCode"],
                VnpAmount = int.Parse(responseParams["vnp_Amount"]),
                VnpTxnRef = responseParams["vnp_TxnRef"],
                VnpOrderInfo = responseParams["vnp_OrderInfo"],
                VnpResponseCode = responseParams["vnp_ResponseCode"],
                VnpTransactionNo = responseParams["vnp_TransactionNo"],
                VnpTransactionStatus = responseParams["vnp_TransactionStatus"],
                VnpPayDate = responseParams["vnp_PayDate"],
                VnpTmnCode = responseParams["vnp_TmnCode"],
                PaymentCreatedAt = DateTime.Now.ToString("yyyyMMddHHmmss"),
                PaymentsStatus= responseParams["vnp_ResponseCode"] == "00" ? "Success" : "Failed"
            };

            // Thêm vào cơ sở dữ liệu
            _finalInternDbContext.Payments.Add(payment);
            _finalInternDbContext.SaveChanges();
        }

        public void SaveTransaction(Dictionary<string, string> responseParams)
        {
            var transaction = new Transaction
            {
                DayOfTransaction = DateTime.Now.ToString("yyyyMMddHHmmss"),
                AccountId = int.Parse(responseParams["AccountId"]),
                OrderId = int.Parse(responseParams["orderId"]),
                Price = int.Parse(responseParams["vnp_Amount"]) / 100,
                Total = int.Parse(responseParams["vnp_Amount"]) / 100,
                Status = responseParams["vnp_ResponseCode"] == "00" ? 1 : 0
            };

            // Thêm vào cơ sở dữ liệu
            _finalInternDbContext.Transactions.Add(transaction);
            _finalInternDbContext.SaveChanges();
        }


    }
}
