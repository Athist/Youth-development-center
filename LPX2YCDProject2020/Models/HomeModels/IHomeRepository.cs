using LPX2YCDProject2020.Models.ContactUs;

namespace LPX2YCDProject2020.Models.HomeModels
{
    public interface IHomeRepository
    {
        ContactUsModel GetSystemDetailsAsync();
    }
}