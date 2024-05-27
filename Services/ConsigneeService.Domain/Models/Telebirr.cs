using ConsigneeService.Domain.Common;
using System;

namespace ConsigneeService.Domain.Models
{
    public class Telebirr : BaseEntity
    {
        public string ToPayUrl { get; private set; } = string.Empty;
        public string OutTradeNumber { get; private set; } = string.Empty;
        public string Nonce { get; private set; } = string.Empty;
        public string Msisdn { get; private set; } = string.Empty;
        public string TradeStatus { get; private set; } = string.Empty;
        public string TransactionNo { get; private set; } = string.Empty;
        public double Amount { get; private set; }
        public string TradeNo { get; private set; } = string.Empty;
        public DateTime TradeDate { get; private set; }
        public string Uid { get; private set; } = string.Empty;


        public static Telebirr Create(
            string toPayUrl,
            string outTradeNumber,
            string nonce,
            string msisdn,
            string tradeStatus,
            string transactionNo,
            double amount,
            string tradeNo,
            DateTime tradeDate,
            string uid)
        {
            return new Telebirr
            {
                ToPayUrl = toPayUrl,
                OutTradeNumber = outTradeNumber,
                Nonce = nonce,
                Msisdn = msisdn,
                TradeStatus = tradeStatus,
                TransactionNo = transactionNo,
                Amount = amount,
                TradeNo = tradeNo,
                TradeDate = tradeDate,
                Uid = uid
            };
        }
    }
}
