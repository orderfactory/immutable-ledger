using Faker;
using Microsoft.AspNetCore.Mvc;
using OrderFactory.GuidGenerator;
using OrderFactory.ImmutableLedger.Business;
using OrderFactory.ImmutableLedger.Repo;
using Phone = OrderFactory.ImmutableLedger.Business.Phone;

namespace OrderFactory.ImmutableLedger.FakesGeneratorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerGeneratorController(IGuidGenerator guidGenerator, IRepository repository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GenerateCustomer()
    {
        var middleName = RandomNumber.Next(3) > 1 ? Name.Middle() : null;
        var customer = new Customer(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            Name.First(),
            middleName,
            Name.Last()
        );
        await repository.SaveCustomer(customer);

        var customer2 = new Customer(
            customer.RootId,
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            customer.RecordId,
            Name.First(),
            customer.MiddleName,
            customer.LastName
        );
        await repository.SaveCustomer(customer2);

        var phoneNumber = Faker.Phone.Number();
        var phone = new Phone(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            phoneNumber[..Math.Min(15, phoneNumber.Length)]
        );
        await repository.SavePhone(phone);

        var phoneNumber2 = Faker.Phone.Number();
        var phone2 = new Phone(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            phoneNumber2[..Math.Min(15, phoneNumber2.Length)]
        );
        await repository.SavePhone(phone2);

        var customer3 = new Customer(
            customer2.RootId,
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            RandomNumber.Next(3) > 2,
            customer2.RecordId,
            customer2.FirstName,
            customer2.MiddleName,
            customer.LastName,
            phone2.RootId
        );
        var count = await repository.SaveCustomer(customer3);

        var customerPhone = new CustomerPhone(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            customer3.RootId,
            phone.RootId
        );
        await repository.SaveCustomerPhone(customerPhone);

        var customerPhone2 = new CustomerPhone(
            guidGenerator.NewGuid(),
            DateTime.UtcNow,
            guidGenerator.NewGuid(),
            false,
            null,
            customer3.RootId,
            phone2.RootId
        );
        await repository.SaveCustomerPhone(customerPhone2);

        if (count > 0) return Ok(customer3);

        return BadRequest();
    }
}