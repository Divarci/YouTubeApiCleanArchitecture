using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;
public static class CustomerData
{
    public static readonly string[] TitleList =
    {
        "Electric Company Ltd",
        "Plumbing Company Ltd",
        "Carpenter", "Carpenter Company Ltd"
    };

    public static readonly string[] FirstLineAddressList =
    {
        "15 Rockstar street",
        "17 Oldcity street",
        "24 Skyscrapper street",
    };

    public static readonly string[] SecondLineAddressList =
    {
        "Tesla close",
        "Sister close",
        "Brother close",
    };

    public static readonly string[] CityList =
    {
        "Mancherter",
        "Liverpool",
        "London",
    };

    public static readonly string[] PostcodeList =
    {
        "ST175PL",
        "HJ147YJ",
        "KL789UJ"
    };

    public const string Country = "UK";

    public static Customer CreateCustomer(
        int titleIndex,
        int firstLineInex,
        int? secondLineIndex,
        int cityIndex,
        int PostcodeIndex,
        Guid CustomerId)
    {
        var customer = Customer.Create(
             new CreateCustomerDto
             {
                 Title = TitleList[titleIndex],
                 FirstLineAddress = FirstLineAddressList[firstLineInex],
                 SecondLineLineAddress = secondLineIndex is null 
                    ? null 
                    : SecondLineAddressList[secondLineIndex.Value],
                 City = CityList[cityIndex],
                 Postcode = PostcodeList[PostcodeIndex],
                 Country = "UK"
             },
             CustomerId);

        return customer;
    }

    public static Customer CreateQuickCustomer(Guid customerId) 
        => CreateCustomer(
            titleIndex: 0,
            firstLineInex: 1,
            secondLineIndex: null,
            cityIndex: 2,
            PostcodeIndex: 0,
            customerId);

}
