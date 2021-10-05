namespace Framework.Application.ZarinPal
{
    public interface IZarinPalFactory
    {
        string Prefix { get; set; }

        PaymentResponse CreatePaymentRequest(string amount, string mobile, string description,
            long orderId, string returnControllerUrl);

        VerificationResponse CreateVerificationRequest(string authority, string price);
    }
}